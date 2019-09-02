using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Media;

namespace RealLifeScratch
{
    public partial class DashBoard : Form
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
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 0);
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
        private List<int[,]> listOfLevels = new List<int[,]>();
        private List<string> listOfParameters = new List<string>();
        private Color Purple = Color.FromArgb(114, 137, 218);
        private Color White = Color.FromArgb(255, 255, 255);
        private Color Grey = Color.FromArgb(153, 170, 181);
        private Color AlmostBlack = Color.FromArgb(44, 47, 51);
        private Color Black = Color.FromArgb(35, 39, 42);
        #endregion

        #region Music Player
        SoundPlayer audio;
        bool Playing = false;
        private void PlayMusic()
        {
            audio = new SoundPlayer(RealLifeScratch.Properties.Resources.BlockySong);
            audio.PlayLooping();
            Playing = true;
        }
        private void StopMusic()
        {
            audio.Stop();
            Playing = false;
        }
        #endregion

        #region Constructor
        public DashBoard()
        {
            InitializeComponent();
        }
        #endregion

        #region Helpers
        private void ReadMazes()
        {
            listOfLevels.Clear();
            String[] input = File.ReadAllLines(@"mazes.txt");
            int ii = 0, jj = 0;
            for (int xx = 0; xx < 21; xx++)
            {
                int[,] result = new int[15, 15];
                for (int aa = xx * 15 + 1 + xx; aa < (xx + 1) * 15 + xx + 1; aa++)
                {
                    jj = 0;
                    foreach (var col in input[aa].Trim().Split(' '))
                    {
                        result[ii, jj] = int.Parse(col.Trim());
                        jj++;
                    }
                    ii++;
                }
                ii = 0;
                jj = 0;
                listOfLevels.Add(result);
                string a = (input[xx * 15 + xx].Split('*'))[1] + "*" + (input[xx * 15 + xx].Split('*'))[2] + "*" + (input[xx * 15 + xx].Split('*'))[3];
                listOfParameters.Add(a);
            }
        }
        #endregion

        #region On Load
        private void DashBoard_Load(object sender, EventArgs e)
        {
            PlayMusic();
            this.Icon = RealLifeScratch.Properties.Resources.BlockyLogo;
            if (!File.Exists("mazes.txt"))
                File.WriteAllText("mazes.txt", Properties.Resources.mazes);
            this.BackColor = AlmostBlack;
            List<Button> buttons = new List<Button>();   
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Button buttonLevel = new Button { Text = "" + (i*7+j+1),Tag = (i*7+j), Dock = DockStyle.Fill,ForeColor = White,Cursor=Cursors.Hand,BackColor= AlmostBlack,FlatStyle = FlatStyle.Flat,Font = new Font("Uni Sans Heavy",20), AutoSize = true };
                    buttonLevel.FlatAppearance.MouseDownBackColor = Purple;
                    buttonLevel.FlatAppearance.MouseOverBackColor = Grey;
                    buttons.Add(buttonLevel);
                    tableLayoutPanel1.Controls.Add(buttonLevel, j, i);
                }
            }
            buttons[20].Tag = "custom";
            buttons[20].Text = "Creator";
            foreach (Button button in buttons)
            {
                if(!(button.Tag.ToString()=="custom"))
                    button.Click += Button_Click;
            }
            
            buttons[20].Click += CreatorButton_Click;
            ReadMazes();
        }
        #endregion

        #region Start Custom Level Event
        private void CreatorButton_Click(object sender, EventArgs e)
        {
            CustomGameCreator frm = new CustomGameCreator();
            this.Hide();
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                StartLevel(20);
            }
            this.Show();
            this.Activate();
            ReadMazes();
        }
        #endregion

        #region Start normal level
        private void StartLevel(int ID)
        {
            GameScreen gameScreen = new GameScreen(ID,listOfLevels[ID],listOfParameters[ID].Split('*'));
            if (gameScreen.ShowDialog() == DialogResult.OK)
            {
                if (gameScreen.returnCode == 0)
                {
                    ReadMazes();
                    gameScreen.Dispose();
                    StartLevel(ID);
                    return;
                }
                else if (gameScreen.returnCode == 21)
                {
                    this.Show();
                    ReadMazes();
                    gameScreen.Dispose();
                    return;
                }
                else
                {
                    ReadMazes();
                    gameScreen.Dispose();
                    StartLevel(ID+1);
                    return;
                }
            }
            this.Activate();
            this.Show();
            ReadMazes();
        }
        #endregion

        #region Button Start Level Click Event
        private void Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartLevel(int.Parse(((Button)sender).Tag.ToString()));
        }
        #endregion

        #region Button Exit Click Event
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Button Maximize Click Event
        private void button2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;
        }

        #endregion

        #region Button Minimize Click Event
        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Move Panel Mouse Move Event
        private void ControlPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Audio Button Click Event
        private void btn_audio_Click(object sender, EventArgs e)
        {
            if (Playing)
            {
                StopMusic();
                btn_audio.BackgroundImage = RealLifeScratch.Properties.Resources.AudioOff;
            }
            else
            {
                PlayMusic();
                btn_audio.BackgroundImage = RealLifeScratch.Properties.Resources.AudioOn;
            }
        }
        #endregion
    }
}
