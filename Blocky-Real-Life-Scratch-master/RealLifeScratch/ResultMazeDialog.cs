using System;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RealLifeScratch
{
    public partial class ResultMazeDialog : Form
    {
        #region Constants and OVERRIDES
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
   (
       int nLeftRect, // x-coordinate of upper-left corner
       int nTopRect, // y-coordinate of upper-left corner
       int nRightRect, // x-coordinate of lower-right corner
       int nBottomRect, // y-coordinate of lower-right corner
       int nWidthEllipse, // height of ellipse
       int nHeightEllipse // width of ellipse
    );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;
                //const int CS_DROPSHADOW2 = 0x20000;
                //cp.ClassStyle |= CS_DROPSHADOW2;

                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 1, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Variables
        private bool portFound = false;
        private SerialPort currentPort;
        private int Request;
        #endregion

        #region Board
        private void SetComPort()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                try
                {
                    currentPort = new SerialPort(port, 9600);
                    if (DetectArduino())
                    {
                        portFound = true;
                        return;
                    }
                }
                catch
                { }
            }
        }

        #region Detect Arduino
        private bool DetectArduino()
        {
            try
            {
                currentPort.Open();
                currentPort.Write("!");
                System.Threading.Thread.Sleep(1000);
                string a = currentPort.ReadExisting();
                currentPort.Close();
                if (a.Contains("Detected"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #endregion
        
        #region Constructor
        public ResultMazeDialog(int request)
        {
            Request = request;
            InitializeComponent();
        }
        #endregion

        #region On Load
        private void FinishedMazeDialog_Load(object sender, EventArgs e)
        {
            lbl_title.Font = new Font("Uni Sans Heavy", 10);
            btn_next.Enabled = false;
            btn_retry.Enabled = false;
            btn_home.Enabled = false;
            btn_creator.Enabled = false;
            btn_playLevel.Enabled = false;
            btn_connect.Enabled = false;
            btn_creator.Hide();
            btn_playLevel.Hide();
            btn_next.Hide();
            btn_retry.Hide();
            btn_home.Hide();
            btn_connect.Hide();
            this.Icon = RealLifeScratch.Properties.Resources.BlockyLogo;
            if (Request == 0)
            {
                btn_home.Enabled = true;
                btn_home.Show();
                btn_retry.Enabled = true;
                btn_retry.Show();
                btn_next.Enabled = true;
                btn_next.Show();
                GIFBox.Image = RealLifeScratch.Properties.Resources.Won;
                
                lbl_title.Text = "Winner";
            }
            else if (Request == 1)
            {
                btn_home.Enabled = true;
                btn_home.Show();
                btn_retry.Enabled = true;
                btn_retry.Show();
                GIFBox.Image = RealLifeScratch.Properties.Resources.Failed;
                lbl_title.Text = "Loser";
            }
            else if(Request==2)
            {
                lbl_title.Text = "Connection";
                btn_connect.Show();
                btn_connect.Enabled = true;
                GIFBox.Image = RealLifeScratch.Properties.Resources.connect;
            }
            else if (Request == 3) {
                btn_retry.Show();
                btn_retry.Enabled = true;
                btn_retry.Width = 375;
                lbl_title.Text = "Invalid Code";
                GIFBox.Image = RealLifeScratch.Properties.Resources.InvalidCode;
            }
            else if(Request == 4)
            {
                btn_creator.Enabled = true;
                btn_playLevel.Enabled = true;
                btn_creator.Show();
                btn_playLevel.Show();
                lbl_title.Text = "Choose";
                GIFBox.Image = RealLifeScratch.Properties.Resources.Choose;
            }
            else if(Request == 5)
            {
                btn_home.Show();
                btn_home.Enabled = true;
                btn_home.Width = 375;
                lbl_title.Text = "Saved";
                btn_home.Location = new Point(13, btn_home.Location.Y);
                GIFBox.Image = RealLifeScratch.Properties.Resources.Saved;
            }
            else if(Request == 6)
            {
                btn_retry.Show();
                btn_retry.Enabled = true;
                btn_retry.Width = 375;
                lbl_title.Text = "Failed";
                GIFBox.Image = RealLifeScratch.Properties.Resources.FailedSave;
            }
            else if (Request == 7)
            {
                btn_retry.Show();
                btn_retry.Enabled = true;
                btn_retry.Width = 375;
                lbl_title.Text = "Too many lines";
                GIFBox.Image = RealLifeScratch.Properties.Resources.longCode;
            }
            else if (Request == 8)
            {

            }
        }
        #endregion

        #region Button Click Events
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
        
        private void btn_home_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            SetComPort();
            if (portFound)
                this.DialogResult = DialogResult.OK;
        }

        private void btn_playLevel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btn_creator_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        
        private void btn_retry_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
        }
        #endregion
    }
}
