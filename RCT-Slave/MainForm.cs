using System.Diagnostics;

namespace RCT_Slave
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            LogToFile("\n\n ---------- NEW SESSION ---------- \n\n");
            InitializeComponent();
            PostInit();
        }

        private async void PostInit()
        {
            AppendInfoText("Waiting for PostInit...");
            await Task.Delay(1000);
            Program.coreInit();
            reload_CFG(null, null);
            AppendSuccess("Post Init successful!");
            postPostInit();

        }

        private async void postPostInit()
        {
            await Task.Delay(1000);
            AppendInfoText("\n--[========|========|==RCT Online==|========|========]--\n");

        }

        //##############################
        //         RichText Append
        //##############################
        public void AppendError(string errorText)
        {
            ConsoleOutput.SelectionColor = System.Drawing.Color.Red;
            ConsoleOutput.AppendText(errorText + "\n");
            LogToFile("[Error] " + errorText);
        }
        public void AppendWarning(string warningText)
        {
            ConsoleOutput.SelectionColor = System.Drawing.Color.Yellow;
            ConsoleOutput.AppendText(warningText + "\n");
            LogToFile($"[Warning] " + warningText);
        }
        public void AppendSuccess(string successText)
        {
            ConsoleOutput.SelectionColor = System.Drawing.Color.LightGreen;
            ConsoleOutput.AppendText(successText + "\n");
            LogToFile($"[Success] " + successText);
        }
        public void AppendInfoText(string infoText)
        {
            ConsoleOutput.SelectionColor = System.Drawing.Color.White;
            ConsoleOutput.AppendText(infoText + "\n");
            LogToFile($"[Info] " + infoText);
        }

        //##############################
        //         WriteToLog
        //##############################

        public void LogToFile(string message)
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            string timeStampedMessage = $"[{DateTime.Now:yyy-MM-DD HH:mm:ss}] {message}";
            File.AppendAllText(logFilePath, timeStampedMessage + Environment.NewLine);
        }



        //only for output message - single use
        public void AppendMessageText(string messageText)
        {
            ConsoleOutput.SelectionColor = System.Drawing.Color.LightGreen;
            ConsoleOutput.AppendText(messageText);
        }
        public void AppendReadbackText(string readbackText)
        {
            ConsoleOutput.SelectionColor = System.Drawing.Color.Cyan;
            ConsoleOutput.AppendText(readbackText);

        }


        //##############################
        //         InfoButtons
        //##############################

        private void inputHelp(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("help.txt"))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "help.txt",
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        AppendError(ex.ToString());
                    }
                }
                else
                {
                    AppendWarning("info file not found!");
                }
            }
            catch (Exception ex)
            {
                AppendError(ex.ToString());
            }
        }

        private void inputUpdate(object sender, EventArgs e)
        {
            AppendWarning("\nAutomatic Updates are not available yet.\n Please visit:\n https://rehoga-interactive.com/remote-control-terminal \n and crosscheck the version number\n");
            AppendError("ALWAYS UPDATE MASTER TOO!! DIFFERENT VERSIONS CAN CAUSE ISSUES");
        }



        //##############################
        //         General
        //##############################
        private void inputClearConsole(object sender, EventArgs e)
        {
            ConsoleOutput.Clear();
            AppendInfoText("Console cleared...");
        }

        private void inputOpenLog(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "log.txt",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                AppendError(ex.ToString());
            }
        }

        private void inputOpenConfig(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("SlaveConfig.xml"))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "SlaveConfig.xml",
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        AppendError(ex.ToString());
                    }
                }
                else
                {
                    reload_CFG(null, null);
                }
            }
            catch (Exception ex)
            {
                AppendError(ex.ToString());
            }
        }

        private async void reload_CFG(object sender, EventArgs e)
        {
            AppendInfoText("Reloading SlaveConfig.xml ...");
            Config config = Program.LoadConfig("SlaveConfig.xml", false);

            if (config != null)
            {
                richTextMasterIP.Text = config.MasterIP;
                richTextMasterPort.Text = config.MasterPort.ToString();
                richTextToken.Text = config.Token;
                checkBoxWANMode.Checked = config.WanMode;
            }
            else
            {
                await Task.Delay(50);
                AppendInfoText("Generating new SlaveConfig.xml ...");
                Config defaultConfig = CreateDefaultConfig();
                Program.SaveConfigFile(defaultConfig, "SlaveConfig.xml");
                await Task.Delay(100);
                AppendSuccess("SlaveConfig.xml Generated!");
            }
        }

        private void save_CFG(object sender, EventArgs e)
        {
            AppendInfoText("Saving SlaveConfig.xml ...");

            Config config = Program.LoadConfig("SlaveConfig.xml", true) ?? new Config();

            config.MasterIP = richTextMasterIP.Text;
            config.MasterPort = int.TryParse(richTextMasterPort.Text, out int port) ? port : 65000;
            config.Token = richTextToken.Text;
            config.WanMode = checkBoxWANMode.Checked;

            Program.SaveConfigFile(config, "SlaveConfig.xml");
        }

        private async void delCFG(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
                "Are you sure you want to reset the configuration?",
                "Reset Config",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
            {
                AppendInfoText("Reset cancelled by user.");
                return;
            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SlaveConfig.xml");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                AppendInfoText("Config deleted.");
            }
            else
            {
                AppendWarning("SlaveConfig.xml does not exist.");
            }

            await Task.Delay(250);

            Config defaultConfig = CreateDefaultConfig();
            Program.SaveConfigFile(defaultConfig, "SlaveConfig.xml");
            await Task.Delay(200);
            reload_CFG(null, null);
        }


        private Config CreateDefaultConfig()
        {
            return new Config
            {
                MasterIP = "127.0.0.1",
                MasterPort = 65000,
                Token = "12345",
                WanMode = false,

                Event1Name = "none",
                Event1Path = "empty",
                Event2Name = "none",
                Event2Path = "empty",
                Event3Name = "none",
                Event3Path = "empty",
                Event4Name = "none",
                Event4Path = "empty",
                Event5Name = "none",
                Event5Path = "empty",
                Event6Name = "none",
                Event6Path = "empty",
                Event7Name = "none",
                Event7Path = "empty",
                Event8Name = "none",
                Event8Path = "empty",
                Event9Name = "none",
                Event9Path = "empty",
                Event10Name = "none",
                Event10Path = "empty",
                Event11Name = "none",
                Event11Path = "empty",
                Event12Name = "none",
                Event12Path = "empty",
                Event13Name = "none",
                Event13Path = "empty",
                Event14Name = "none",
                Event14Path = "empty",
                Event15Name = "none",
                Event15Path = "empty",
                Event16Name = "none",
                Event16Path = "empty",
                Event17Name = "none",
                Event17Path = "empty",
                Event18Name = "none",
                Event18Path = "empty",
                Event19Name = "none",
                Event19Path = "empty",
                Event20Name = "none",
                Event20Path = "empty",
                Event21Name = "none",
                Event21Path = "empty",
                Event22Name = "none",
                Event22Path = "empty",
                Event23Name = "none",
                Event23Path = "empty",
                Event24Name = "none",
                Event24Path = "empty",
                Event25Name = "none",
                Event25Path = "empty",
                Event26Name = "none",
                Event26Path = "empty",
                Event27Name = "none",
                Event27Path = "empty",
                Event28Name = "none",
                Event28Path = "empty",
                Event29Name = "none",
                Event29Path = "empty",
                Event30Name = "none",
                Event30Path = "empty",
                Event31Name = "none",
                Event31Path = "empty",
                Event32Name = "none",
                Event32Path = "empty"
            };
        }





        private void wanBoxClicked(object sender, EventArgs e)
        {
            MessageBox.Show("WAN Mode: \n\nOn: Invalid Responses are filtered \nOff: Invalid Responses are displayed ", "RCT", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }










































        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextToken_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
