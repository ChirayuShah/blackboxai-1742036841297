using System.Windows.Forms;

namespace NotificationClient
{
    public class InputDialog : Form
    {
        private TextBox txtInput;
        private Button btnOK;
        private Button btnCancel;
        private Label lblPrompt;

        public string InputText
        {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public InputDialog(string title, string prompt, string defaultText = "")
        {
            InitializeComponent();
            this.Text = title;
            this.lblPrompt.Text = prompt;
            this.txtInput.Text = defaultText;
        }

        private void InitializeComponent()
        {
            this.txtInput = new TextBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.lblPrompt = new Label();

            // Label
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new System.Drawing.Point(12, 9);
            this.lblPrompt.Size = new System.Drawing.Size(200, 15);

            // TextBox
            this.txtInput.Location = new System.Drawing.Point(12, 27);
            this.txtInput.Size = new System.Drawing.Size(260, 23);

            // OK Button
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(116, 56);
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Text = "OK";
            this.btnOK.Click += (s, e) => 
            {
                if (string.IsNullOrWhiteSpace(txtInput.Text))
                {
                    MessageBox.Show("Please enter a valid value.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
            };

            // Cancel Button
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 56);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Cancel";

            // Form
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 91);
            this.Controls.AddRange(new Control[] 
            { 
                this.lblPrompt,
                this.txtInput,
                this.btnOK,
                this.btnCancel
            });
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
