using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RCT_Slave
{
    internal static class Program
    {
        public static MainForm form;

        public static Config LoadedConfig { get; private set; }
        public static string MasterIp = "127.0.0.1";
        public static int MasterPort = 1;
        public static int responsePort = 65534;
        public static string token = "12345";
        public static string hashKey = "c71ee8230724cc1eef15740fba8506a2";
        public static bool WanMode = false;
        private static TcpListener readbackListener;
        private static Thread listenerThread;
        private static bool listenerRunning = false;
        private static CancellationTokenSource listenerCancelToken;


        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            form = new MainForm();
            Application.Run(form);
        }

        public static async void coreInit()
        {
            await Task.Delay(500);
            Program.InitReadback();
        }

        //##############################
        //         Read/Write
        //##############################
        //CFG Loader
        public static Config LoadConfig(string filePath, bool silentMode)
        {
            try
            {
                if (File.Exists(filePath))
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(Config));

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        Config config = (Config)serializer.Deserialize(reader);
                        LoadedConfig = config;
                        MasterIp = config.MasterIP;
                        if (silentMode == false)
                        {
                            form.AppendSuccess("SlaveConfig.xml loaded and applied!");
                        }
                        MasterPort = config.MasterPort;
                        token = config.Token;
                        responsePort = config.MasterPort + 1;
                        WanMode = config.WanMode;
                        form.LogToFile("[LOG READ CONTENT] " + MasterIp + ":" + MasterPort + ":" + token + ":" + "[LOG READ CONTENT]");
                        RestartReadback();

                        return config;
                    }
                }
                else
                {
                    form.AppendWarning("SlaveConfig.xml not existing");
                    return null;
                }
            }
            catch (Exception ex)
            {
                form.AppendError("SlaveConfig.xml read error: " + ex.Message);
                return null;
            }
        }

        //CFG Writer
        public static async void SaveConfigFile(Config config, string fileName)
        {
            try
            {
                string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(rootPath, fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(Config));

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, config);
                    MasterIp = config.MasterIP;
                    MasterPort = config.MasterPort;
                    token = config.Token;
                    WanMode = config.WanMode;
                    form.LogToFile("[CFG WRITE CONTENT] " + MasterIp + ":" + MasterPort + ":" + token + ":" + "[CFG WRITE CONTENT]");
                }
                form.AppendSuccess("SlaveConfig.xml saved!");
                await Task.Delay(200);
                LoadConfig("SlaveConfig.xml", true);
            }
            catch (Exception ex)
            {
                form.AppendError("CFG save error: " + ex.Message);
            }
        }



        //##############################
        //         Sender
        //##############################

        public static void SendMessage(string content)
        {
            try
            {
                using (TcpClient client = new TcpClient(MasterIp, responsePort))
                {


                    NetworkStream stream = client.GetStream();
                    string message = $"{hashKey}-{token}-{content}";

                    string encryptedMessage = CryptoCore.Encrypt(message);


                    byte[] data = Encoding.UTF8.GetBytes(encryptedMessage);

                    // send
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                form.AppendError($"Error: {ex.Message}");
            }
        }



        //##############################
        //         Readback Listener
        //##############################

        public static async Task InitReadback()
        {
            if (listenerRunning) return;

            listenerRunning = true;
            listenerCancelToken = new CancellationTokenSource();

            await Task.Run(() => ReadbackListener(MasterIp, MasterPort, listenerCancelToken.Token));
        }

        public static async Task ReadbackListener(string ipAddress, int port, CancellationToken cancellationToken)
        {
            try
            {
                readbackListener = new TcpListener(IPAddress.Any, port);
                readbackListener.Start();

                form.Invoke(new Action(() =>
                {
                    form.AppendSuccess($"Listener online at {ipAddress}:{port}");
                }));

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (!readbackListener.Pending())
                    {
                        await Task.Delay(100, cancellationToken);
                        continue;
                    }

                    using (TcpClient client = await readbackListener.AcceptTcpClientAsync())
                    using (NetworkStream stream = client.GetStream())
                    {
                        var remoteIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

                        if (remoteIp != Program.MasterIp)
                        {
                            continue;  
                        }

                        byte[] buffer = new byte[1024];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                        if (bytesRead == 0) continue;

                        string encryptedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        string decryptedMessage = CryptoCore.Decrypt(encryptedMessage);

                        string[] parts = decryptedMessage.Split('-');
                        string parsedMessage;
                        string instructionMessage;

                        if (parts.Length == 3)
                        {
                            string hash = parts[0];
                            string incomingToken = parts[1];
                            string content = parts[2];

                            if (incomingToken == Program.token)
                            {
                                parsedMessage = content;
                                instructionMessage = CryptoCore.Encrypt($"ACK-{content}");

                                form.Invoke(new Action(() =>
                                {
                                    form.AppendReadbackText(MasterIp + ": ");
                                    form.AppendInfoText(parsedMessage);
                                }));

                                ActionHandler(parsedMessage);
                            }
                            else
                            {

                                parsedMessage = $"Invalid token received from {MasterIp}";
                                instructionMessage = CryptoCore.Encrypt("ERR-InvalidToken");


                                if (WanMode == false)
                                {
                                    form.Invoke(new Action(() =>
                                    {
                                        form.AppendWarning(parsedMessage);
                                    }));
                                }

                            }
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //LogInfo("Listener canceled.");
            }
            catch (Exception ex)
            {
                LogError($"Listener error: {ex.Message}");
            }
            finally
            {
                try { readbackListener?.Stop(); } catch { }

                listenerRunning = false;
                readbackListener = null;

                LogInfo("Listener stopped.");
            }
        }


        public static async Task RestartReadback()
        {
            try
            {
                listenerCancelToken?.Cancel();
                await Task.Delay(500);

                await InitReadback();

                form.Invoke(new Action(() =>
                {
                    //form.AppendSuccess("Listener restarted successfully.");
                }));
            }
            catch (Exception ex)
            {
                form.Invoke(new Action(() =>
                {
                    form.AppendError($"Error restarting listener: {ex.Message}");
                }));
            }
        }

        private static void LogError(string message)
        {
            form.Invoke(new Action(() => form.AppendError(message)));
        }

        private static void LogInfo(string message)
        {
            form.Invoke(new Action(() => form.AppendInfoText(message)));
        }


        //##############################
        //         Action Handler
        //##############################


        private static (string Name, string Path)? GetMatchedEvent(Config config, string message)
        {
            for (int i = 1; i <= 32; i++)
            {
                var nameProp = typeof(Config).GetProperty($"Event{i}Name");
                var pathProp = typeof(Config).GetProperty($"Event{i}Path");

                if (nameProp != null && pathProp != null)
                {
                    string eventName = nameProp.GetValue(config)?.ToString();
                    string eventPath = pathProp.GetValue(config)?.ToString();

                    if (!string.IsNullOrEmpty(eventName) && message == eventName)
                    {
                        return (Name: eventName, Path: eventPath);
                    }
                }
            }

            return null;
        }

        public static void ActionHandler(string parsedMessage)
        {
            var config = LoadedConfig;
            if (config == null) return;

            if (parsedMessage == "[RCT]ConnectionTest[RCT]")
            {
                form.Invoke(new Action(() =>
                {
                    form.AppendSuccess($"Link Established. Sending Response to {MasterIp}");
                }));
                SendMessage("Connection Established. RCT Link online!");
                return;
            }

            var matchedEvent = GetMatchedEvent(config, parsedMessage);
            if (matchedEvent.HasValue)
            {
                var eventName = matchedEvent.Value.Name;
                var eventPath = matchedEvent.Value.Path;

                form.Invoke(new Action(() =>
                {
                    form.AppendSuccess($"Event triggered: {eventName} - {eventPath}");
                }));

                if (!string.IsNullOrWhiteSpace(eventPath) && eventPath != "empty")
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = eventPath,
                            UseShellExecute = true
                        });

                        SendMessage(eventName + " Executed Successfully!");

                        form.Invoke(new Action(() =>
                        {
                            form.AppendInfoText(eventPath + " Executed by: " + MasterIp);
                        }));
                    }
                    catch (Exception ex)
                    {
                        form.Invoke(new Action(() =>
                        {
                            form.AppendError($"Error executing: {ex.Message}");
                        }));

                        SendMessage(eventName + ": " + ex.Message);
                    }
                }
            }
            else
            {
                form.Invoke(new Action(() =>
                {
                    form.AppendWarning($"Function not configured {parsedMessage}");
                }));

                SendMessage($"Function not configured {parsedMessage}");
            }          
        }
    }
}