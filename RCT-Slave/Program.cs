using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RCT_Slave
{
    internal static class Program
    {
        public static Form1 form;


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
            form = new Form1();
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
                        MasterIp = config.MasterIP;
                        if (silentMode == false)
                        {
                            form.AppendSuccess("config.xml loaded and applied!");
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
                    form.AppendWarning("config.xml not existing");
                    return null;
                }
            }
            catch (Exception ex)
            {
                form.AppendError("config.xml read error: " + ex.Message);
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
                form.AppendSuccess("config.xml saved!");
                await Task.Delay(200);
                LoadConfig("config.xml", true);
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
                using (TcpClient client = new TcpClient(MasterIp, MasterPort))
                {


                    NetworkStream stream = client.GetStream();
                    string message = $"{hashKey}-{token}-{content}";

                    string encryptedMessage = CryptoCore.Encrypt(message);


                    byte[] data = Encoding.UTF8.GetBytes(encryptedMessage);

                    // send
                    stream.Write(data, 0, data.Length);
                    form.AppendMessageText("Message Sent: ");
                    form.AppendInfoText(content);
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
                                    form.AppendReadbackText("[INBOUND]: ");
                                    form.AppendInfoText(parsedMessage);
                                }));

                                ActionHandler(parsedMessage);
                            }
                            else
                            {

                                parsedMessage = $"Invalid token received";
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


        public static void ActionHandler(string parsedMessage)
        {

            switch (parsedMessage)
            {
                case "[RCT]ConnectionTest[RCT]":
                    form.Invoke(new Action(() =>
                    {
                        form.AppendWarning(parsedMessage + "Link Established. Sending Response.");
                    }));
                   
                    break;

            }
        }

        

    }
}