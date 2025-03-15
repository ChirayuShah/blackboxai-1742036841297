namespace NotificationClient
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lstMessages;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btnSettings;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Initialize NotifyIcon
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon.Icon = System.Drawing.SystemIcons.Application;
            this.notifyIcon.Text = "Notification Client";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);

            // Initialize Message ListBox
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.lstMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.IntegralHeight = false;
            this.lstMessages.ItemHeight = 15;
            this.lstMessages.Name = "lstMessages";

            // Initialize Message TextBox
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.PlaceholderText = "Type your message here...";
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);

            // Initialize Send Button
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSend.Text = "Send";
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Name = "btnSend";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // Initialize Settings Button
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnSettings.Text = "Settings";
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            // Initialize Status Label
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatus.AutoSize = true;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Disconnected";
            this.lblStatus.ForeColor = System.Drawing.Color.Red;

            // Create bottom panel for input controls
            var bottomPanel = new System.Windows.Forms.Panel();
            bottomPanel.Height = 40;
            bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            bottomPanel.Padding = new System.Windows.Forms.Padding(5);

            // Create status panel
            var statusPanel = new System.Windows.Forms.Panel();
            statusPanel.Height = 30;
            statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            statusPanel.Padding = new System.Windows.Forms.Padding(5);

            // Add controls to panels
            bottomPanel.Controls.Add(this.btnSend);
            bottomPanel.Controls.Add(this.txtMessage);
            statusPanel.Controls.Add(this.lblStatus);
            statusPanel.Controls.Add(this.btnSettings);

            // Configure main form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(statusPanel);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.Text = "Notification Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);

            // Layout adjustments
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Width = 75;
            this.btnSettings.Width = 75;
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
        }
    }
}
