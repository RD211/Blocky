using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealLifeScratch
{
    public partial class CustomGameCreator : Form
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
        private int[,] GameLevel = new int[15,15];
        private Point SelectedBlock = new Point(-1,-1);
        private int SelectedValue = 0;
        private List<int[,]> listOfLevels = new List<int[,]>();
        private Color Purple = Color.FromArgb(114, 137, 218);
        private Color White = Color.FromArgb(255, 255, 255);
        private Color Grey = Color.FromArgb(153, 170, 181);
        private Color AlmostBlack = Color.FromArgb(44, 47, 51);
        private Color Black = Color.FromArgb(25, 26, 27);
        #endregion

        #region Constructor
        public CustomGameCreator()
        {
            InitializeComponent();
        }
        #endregion

        #region Draw Maze
        private void LoadMaze()
        {
            var maze = GameLevel;
            Bitmap bmp = new Bitmap(300, 300);
            Graphics g = Graphics.FromImage(bmp);
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int x = maze[i, j];
                    switch (x)
                    {
                        case 0:
                            g.FillRectangle(new SolidBrush(AlmostBlack), j * 20, i * 20, 20, 20);
                            break;
                        case 1:
                            g.FillRectangle(new SolidBrush(Black), j * 20, i * 20, 20, 20);
                            break;
                        case 2:
                            g.FillRectangle(new SolidBrush(Purple), j * 20, i * 20, 20, 20);
                            break;
                        case 3:
                            g.FillRectangle(new SolidBrush(Color.Orange), j * 20, i * 20, 20, 20);
                            break;
                        case 4:
                            g.FillRectangle(new SolidBrush(Color.Gray), j * 20, i * 20, 20, 20);
                            break;
                    }
                }
            }
            mazeBox.Image = bmp;
        }
        #endregion

        #region On Load
        private void CustomGameCreator_Load(object sender, EventArgs e)
        {
            this.Icon = RealLifeScratch.Properties.Resources.BlockyLogo;
            lbl_title.Font = new Font("Uni Sans Heavy", 10);
            btn_purpleblock.BackColor = Purple;
            btn_blackblock.BackColor = Black;
            btn_whiteblock.BackColor = AlmostBlack;

            ResultMazeDialog dialog = new ResultMazeDialog(4);
            var res = dialog.ShowDialog();
            if (res == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
            }
            else if (res == DialogResult.Abort)
                this.DialogResult = DialogResult.Abort;
            LoadMaze();
        }
        #endregion

        #region Click On Picture Box
        private void mazeBox_MouseClick(object sender, MouseEventArgs e)
        {
            if ((SelectedValue == 2 || SelectedValue == 3) && (!CheckIfLegalNumberOfElements(SelectedValue)))
            {
                //MessageBox.Show("To many of this block has been used!");
                return;
            }
            SelectedBlock = new Point((int)(e.X / (500 / 15)), (int)(e.Y / (500 / 15)));
            GameLevel[SelectedBlock.Y,SelectedBlock.X] = SelectedValue;
            LoadMaze();
        }
        #endregion

        #region Helpers
        private bool CheckIfLegalNumberOfElements(int Checker)
        {
            foreach (int i in GameLevel) {
                if (i == Checker)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Color Button Click
        private void buttonColors_MouseClick(object sender, EventArgs e)
        {
            SelectedValue = int.Parse(((Button)sender).Tag.ToString());
        }
        #endregion

        #region Save Maze
        private void SaveMaze()
        {
            String[] input = File.ReadAllLines(@"mazes.txt");

            int a = 321;
            for (int i = 0; i < 15; i++)
            {
                string Line = "";
                for (int j = 0; j < 15; j++) {
                    Line += GameLevel[i, j] + " ";
                }
                input[a + i] = Line;
            }
            File.WriteAllLines("mazes.txt", input);
        }
        #endregion

        #region Button Save
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!CheckIfLegalNumberOfElements(2) && !CheckIfLegalNumberOfElements(3))
            {
                SaveMaze();
                ResultMazeDialog result = new ResultMazeDialog(5);
                if (result.ShowDialog() == DialogResult.Cancel)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                ResultMazeDialog result = new ResultMazeDialog(6);
                result.ShowDialog();
            }
        }
        #endregion

        #region Button Exit Click Event
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Button Minimize Click Event
        private void btn_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Lbl Title Mouse Move
        private void lbl_title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion
    }
}
