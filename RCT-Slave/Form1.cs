using System.Diagnostics;

namespace RCT_Slave
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                if (File.Exists("config.xml"))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "config.xml",
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
            AppendInfoText("Reloading config.xml ...");
            Config config = Program.LoadConfig("config.xml", false);

            if (config != null)
            {
                richTextMasterIP.Text = config.MasterIP;
                richTextMasterPort.Text = config.ListenerPort.ToString();
                richTextToken.Text = config.Token;
                checkBoxWANMode.Checked = config.WanMode;
            }
            else
            {
                await Task.Delay(50);
                AppendInfoText("Generating new config.xml ...");
                await Task.Delay(100);
                AppendSuccess("config.xml Generated!");
                save_CFG(null, null);
            }
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

    }
}
