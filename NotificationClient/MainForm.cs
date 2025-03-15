using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace NotificationClient
{
    public partial class MainForm : Form
    {
        private readonly NotificationService _notificationService;
        private string _nickname;

        public MainForm()
        {
            InitializeComponent();
            _notificationService = new NotificationService();
            _nickname = Environment.UserName;
            InitializeNotificationService();
        }

        private async void InitializeNotificationService()
        {
            try
            {
                _notificationService.OnMessageReceived += NotificationService_OnMessageReceived;
                _notificationService.OnConnectionStatusChanged += NotificationService_OnConnectionStatusChanged;
                await _notificationService.StartConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to server: {ex.Message}", "Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotificationService_OnMessageReceived(string sender, string message, DateTime timestamp)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => NotificationService_OnMessageReceived(sender, message, timestamp)));
                return;
            }

            string formattedMessage = $"[{timestamp:HH:mm:ss}] {sender}: {message}";
            lstMessages.Items.Insert(0, formattedMessage);

            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.ShowBalloonTip(3000, "New Message",
                    $"From {sender}: {message}", ToolTipIcon.Info);
            }
        }

        private void NotificationService_OnConnectionStatusChanged(bool isConnected)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => NotificationService_OnConnectionStatusChanged(isConnected)));
                return;
            }

            lblStatus.Text = isConnected ? "Connected" : "Disconnected";
            lblStatus.ForeColor = isConnected ? Color.Green : Color.Red;
            btnSend.Enabled = isConnected;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
                return;

            try
            {
                await _notificationService.SendMessage(_nickname, txtMessage.Text);
                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send message: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !e.Handled)
            {
                e.Handled = true;
                btnSend_Click(sender, e);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _notificationService.Dispose();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using var dialog = new InputDialog("Enter Nickname", "Nickname:", _nickname);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _nickname = dialog.InputText;
            }
        }
    }
}
