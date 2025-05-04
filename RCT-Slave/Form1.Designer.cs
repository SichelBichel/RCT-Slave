namespace RCT_Slave
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            ConsoleOutput = new RichTextBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            checkBoxWANMode = new CheckBox();
            buttonReset = new Button();
            buttonReload = new Button();
            buttonApply = new Button();
            label6 = new Label();
            richTextToken = new RichTextBox();
            label5 = new Label();
            richTextMasterPort = new RichTextBox();
            label4 = new Label();
            label3 = new Label();
            richTextMasterIP = new RichTextBox();
            panel2 = new Panel();
            buttonLog = new Button();
            buttonConfig = new Button();
            buttonClear = new Button();
            label7 = new Label();
            buttonUpdate = new Button();
            buttonHelp = new Button();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ReHoGaBanner3_0_Transparent;
            pictureBox1.Location = new Point(3, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(247, 102);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // ConsoleOutput
            // 
            ConsoleOutput.BackColor = Color.Black;
            ConsoleOutput.ForeColor = SystemColors.Window;
            ConsoleOutput.Location = new Point(363, 12);
            ConsoleOutput.Name = "ConsoleOutput";
            ConsoleOutput.ReadOnly = true;
            ConsoleOutput.Size = new Size(425, 593);
            ConsoleOutput.TabIndex = 1;
            ConsoleOutput.Text = "";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.BannerRCT;
            pictureBox2.Location = new Point(256, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 93);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(256, 108);
            label1.Name = "label1";
            label1.Size = new Size(45, 20);
            label1.TabIndex = 3;
            label1.Text = "Slave";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 113);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 4;
            label2.Text = "Version: 1.0.0";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(checkBoxWANMode);
            panel1.Controls.Add(buttonReset);
            panel1.Controls.Add(buttonReload);
            panel1.Controls.Add(buttonApply);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(richTextToken);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(richTextMasterPort);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(richTextMasterIP);
            panel1.Location = new Point(12, 134);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 449);
            panel1.TabIndex = 5;
            // 
            // checkBoxWANMode
            // 
            checkBoxWANMode.AutoSize = true;
            checkBoxWANMode.Location = new Point(45, 267);
            checkBoxWANMode.Name = "checkBoxWANMode";
            checkBoxWANMode.Size = new Size(88, 19);
            checkBoxWANMode.TabIndex = 10;
            checkBoxWANMode.Text = "WAN Mode";
            checkBoxWANMode.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            buttonReset.BackColor = Color.White;
            buttonReset.Font = new Font("Arial", 12F, FontStyle.Bold);
            buttonReset.Location = new Point(27, 394);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(130, 40);
            buttonReset.TabIndex = 9;
            buttonReset.Text = "RESET";
            buttonReset.UseVisualStyleBackColor = false;
            // 
            // buttonReload
            // 
            buttonReload.BackColor = Color.White;
            buttonReload.Font = new Font("Arial", 12F, FontStyle.Bold);
            buttonReload.Location = new Point(27, 348);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(130, 40);
            buttonReload.TabIndex = 8;
            buttonReload.Text = "RELOAD";
            buttonReload.UseVisualStyleBackColor = false;
            buttonReload.Click += reload_CFG;
            // 
            // buttonApply
            // 
            buttonApply.BackColor = Color.White;
            buttonApply.Font = new Font("Arial", 12F, FontStyle.Bold);
            buttonApply.Location = new Point(27, 302);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(130, 40);
            buttonApply.TabIndex = 7;
            buttonApply.Text = "APPLY";
            buttonApply.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(19, 193);
            label6.Name = "label6";
            label6.Size = new Size(41, 15);
            label6.TabIndex = 6;
            label6.Text = "Token:";
            label6.Click += label6_Click;
            // 
            // richTextToken
            // 
            richTextToken.BackColor = Color.Black;
            richTextToken.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextToken.ForeColor = Color.Yellow;
            richTextToken.Location = new Point(18, 210);
            richTextToken.Name = "richTextToken";
            richTextToken.Size = new Size(149, 33);
            richTextToken.TabIndex = 5;
            richTextToken.Text = "";
            richTextToken.TextChanged += richTextToken_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 126);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 4;
            label5.Text = "Master Port:";
            // 
            // richTextMasterPort
            // 
            richTextMasterPort.BackColor = Color.Black;
            richTextMasterPort.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextMasterPort.ForeColor = Color.Yellow;
            richTextMasterPort.Location = new Point(18, 144);
            richTextMasterPort.Name = "richTextMasterPort";
            richTextMasterPort.Size = new Size(149, 33);
            richTextMasterPort.TabIndex = 3;
            richTextMasterPort.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 61);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 2;
            label4.Text = "Master IP:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Impact", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(19, 16);
            label3.Name = "label3";
            label3.Size = new Size(148, 26);
            label3.TabIndex = 1;
            label3.Text = "Master Settings";
            // 
            // richTextMasterIP
            // 
            richTextMasterIP.BackColor = Color.Black;
            richTextMasterIP.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextMasterIP.ForeColor = Color.Yellow;
            richTextMasterIP.Location = new Point(18, 79);
            richTextMasterIP.Name = "richTextMasterIP";
            richTextMasterIP.Size = new Size(149, 33);
            richTextMasterIP.TabIndex = 0;
            richTextMasterIP.Text = "";
            richTextMasterIP.TextChanged += richTextBox1_TextChanged;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(buttonLog);
            panel2.Controls.Add(buttonConfig);
            panel2.Controls.Add(buttonClear);
            panel2.Controls.Add(label7);
            panel2.Location = new Point(205, 134);
            panel2.Name = "panel2";
            panel2.Size = new Size(151, 222);
            panel2.TabIndex = 6;
            panel2.Paint += panel2_Paint;
            // 
            // buttonLog
            // 
            buttonLog.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            buttonLog.Location = new Point(9, 170);
            buttonLog.Name = "buttonLog";
            buttonLog.Size = new Size(130, 40);
            buttonLog.TabIndex = 5;
            buttonLog.Text = "Open Log";
            buttonLog.UseVisualStyleBackColor = true;
            buttonLog.Click += inputOpenLog;
            // 
            // buttonConfig
            // 
            buttonConfig.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            buttonConfig.Location = new Point(9, 114);
            buttonConfig.Name = "buttonConfig";
            buttonConfig.Size = new Size(130, 40);
            buttonConfig.TabIndex = 4;
            buttonConfig.Text = "Open Config";
            buttonConfig.UseVisualStyleBackColor = true;
            buttonConfig.Click += inputOpenConfig;
            // 
            // buttonClear
            // 
            buttonClear.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            buttonClear.Location = new Point(9, 61);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(130, 40);
            buttonClear.TabIndex = 3;
            buttonClear.Text = "Clear Console";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += inputClearConsole;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Impact", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(32, 17);
            label7.Name = "label7";
            label7.Size = new Size(81, 26);
            label7.TabIndex = 2;
            label7.Text = "General";
            // 
            // buttonUpdate
            // 
            buttonUpdate.Font = new Font("Arial Narrow", 9.75F, FontStyle.Bold);
            buttonUpdate.Location = new Point(93, 109);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(53, 23);
            buttonUpdate.TabIndex = 7;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += inputUpdate;
            // 
            // buttonHelp
            // 
            buttonHelp.Font = new Font("Arial Narrow", 9.75F, FontStyle.Bold);
            buttonHelp.Location = new Point(146, 109);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(53, 23);
            buttonHelp.TabIndex = 8;
            buttonHelp.Text = "Help";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += inputHelp;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(18, 590);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(176, 15);
            linkLabel1.TabIndex = 9;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://rehoga-interactive.com/";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 617);
            Controls.Add(linkLabel1);
            Controls.Add(buttonHelp);
            Controls.Add(buttonUpdate);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(ConsoleOutput);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "RCT-Slave";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private RichTextBox ConsoleOutput;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private RichTextBox richTextMasterIP;
        private Label label3;
        private Label label4;
        private Label label5;
        private RichTextBox richTextMasterPort;
        private Label label6;
        private RichTextBox richTextToken;
        private CheckBox checkBoxWANMode;
        private Button buttonReset;
        private Button buttonReload;
        private Button buttonApply;
        private Panel panel2;
        private Label label7;
        private Button buttonLog;
        private Button buttonConfig;
        private Button buttonClear;
        private Button buttonUpdate;
        private Button buttonHelp;
        private LinkLabel linkLabel1;
    }
}
