using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace RealLifeScratch
{
    #region Form Class
    public partial class GameScreen : Form
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
        //Necessary lists
        private List<FunctionPointersHelper> listOfLinesFunctions = new List<FunctionPointersHelper>();
        private List<FunctionPointersHelper> listOfFunctions = new List<FunctionPointersHelper>();
        private string[] ArrayOfParameters;
        //Level and variables for it
        private int[,] CurrentLevel = new int[15, 15];
        private int MazeID { get; set; }
        public int returnCode { get; set; }
        private int Direction = 2;
        private int MaxBlocks = 16;
        private enum Orientation {Left=-1,Ahead,Right};
        //Colors
        private Color Purple = Color.FromArgb(114, 137, 218);
        private Color White = Color.FromArgb(255, 255, 255);
        private Color Grey = Color.FromArgb(153, 170, 181);
        //private Color  Black = Color.FromArgb(25, 26, 27);
        private Color Black = Color.FromArgb(255, 255, 255);

        private Color AlmostBlack = Color.FromArgb(35, 39, 42);
        //Arduino
        SerialPort currentPort;
        bool portFound;
        //Helper Variables
        bool isAllowed = true;
        bool stopExecution = false;
        Thread SolveThread = null;
        bool isBreak = false;

        #region Block Values

        #region Basic Blocks
        const int vMoveForward = 840;
        const int vTurnLeft = 300;
        const int vTurnRight = 660;
        #endregion

        #region Logic Blocks
        const int vRepeatUntilDone = 730;
        const int vIfPathAhead = 600;
        const int vIfPathLeft = 370;
        const int vIfPathRight = 680;
        const int vWhilePathAhead = 500;
        const int vWhilePathLeft = 320;
        const int vWhilePathRight = 550;
        const int vBreak = 800;
        const int vEND = 700;
        const int vElse = 400;
        #endregion

        #region Variation of Logic Blocks
        const int vIfElsePathAhead = -vIfPathAhead;
        const int vIfElsePathLeft = -vIfPathLeft;
        const int vIfElsePathRight = -vIfPathRight;
        const int vWhileElsePathAhead = -vWhilePathAhead;
        const int vWhileElsePathLeft = -vWhilePathLeft;
        const int vWhileElsePathRight = -vWhilePathRight;
        #endregion

        #endregion

        #endregion

        #region Helpers

        private int GetIndexOfEnd(int pos)
        {
            int num = 0;
            for (int i = pos+1;i<listOfLinesFunctions.Count; i++)
            {
                    if (listOfLinesFunctions[i].vResistor==vEND && num == 0)
                    {
                        return i;
                    }
                    if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vEND,vBreak)))
                    {
                        num++;
                    }
                    if (listOfLinesFunctions[i].vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                    {
                        num++;
                    }
                    if (listOfLinesFunctions[i].vResistor==vEND)
                    {
                        num--;
                    }
            }
            return -1;
        }

        private int[] NormalizeCodeElse(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == vElse)
                {
                    int counter = 0;
                    for (int j = i; j >= 0; j--)
                    {
                        if (counter == 0 && arr[j].In(vIfPathAhead, vIfPathLeft, vIfPathRight, vWhilePathAhead, vWhilePathLeft, vWhilePathRight))
                        {
                            arr[j] = -arr[j];
                            arr[i] = vEND;
                            break;
                        }

                        if (arr[j] == vEND)
                            counter--;
                        else if (arr[j].In(vIfPathAhead, vIfPathLeft, vIfPathRight, vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight, vWhilePathAhead, vWhilePathLeft, vWhilePathRight, vWhileElsePathAhead, vWhileElsePathLeft, vWhileElsePathRight))
                            counter++;
                    }
                }
            }
            string a = "";
            int b = 0;
            foreach(int i in arr)
            {
                if (i != 0)
                    b++;
            }
            int[] arr2 = new int[b];
            for (int i = 0; i < b; i++)
                arr2[i] = arr[i];
            foreach (int i in arr2)
                a += i;
            return arr2;
        }

        private Point GetCoordinatesOfPlayer()
        {
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    if ((CurrentLevel)[i, j] == 2)
                        return new Point(j, i);
                }
            }
            return new Point(0,0);
        }

        private Point GetCoordinatesOfGoal()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if ((CurrentLevel)[i, j] == 3)
                        return new Point(j, i);
                }
            }
            return new Point(0, 0);
        }

        private bool isLegalMove(Orientation offset)
        {
            int tDirection = Direction;
            tDirection += (int)offset;
            if (tDirection == 5)
                tDirection = 1;
            if (tDirection == 0)
                tDirection = 4;

            Point player = GetCoordinatesOfPlayer();
            switch (tDirection)
            {
                case 1:
                    if (player.Y != 0 && (CurrentLevel)[player.Y - 1, player.X].In(1, 3))
                        return true;
                    break;
                case 2:
                    if (player.X != 14 && (CurrentLevel)[player.Y, player.X + 1].In(1, 3))
                        return true;
                    break;
                case 3:
                    if (player.Y!=14 &&(CurrentLevel)[player.Y + 1, player.X].In(1, 3))
                        return true;
                    break;
                case 4:
                    if (player.X!=0 && (CurrentLevel)[player.Y, player.X - 1].In(1, 3))
                        return true;
                    break;
            }

            return false;
        }
        private bool isLegalCode()
        {
            int a=0;
            string msg = "";
            foreach(var i in listOfLinesFunctions)
            {
                if (i.vResistor == vEND)
                    a--;
                else if (i.vResistor.In(vIfPathAhead, vIfPathLeft, vIfPathRight,vWhilePathAhead,vWhilePathLeft,vWhilePathRight,vRepeatUntilDone))
                    a++;
                else if (i.vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                    a += 2;

                msg += i.vResistor.ToString();
            }
            if (a == 0)
                return true;
            else
                return false;
        }
        private bool isInLineWithParameters()
        {
            if (listOfLinesFunctions.Count > MaxBlocks)
                return false;
            else
                return true;
        }
        #endregion

        #region Board

        #region Find Port
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
                        lbl_connected.Text = "Status: Connected";
                        lbl_connected.ForeColor = Purple;
                        if (isAllowed)
                        {
                            btn_solve.Enabled = true;
                            btn_connect.Enabled = true;
                            btn_retry.Enabled = true;
                            btn_exit.Enabled = true;

                        }
                        portFound = true;
                        return;
                    }
                }
                catch
                { }
                }
            lbl_connected.Text = "Status: Not Connected";
            lbl_connected.ForeColor = Purple;
            this.Enabled = false;
            ResultMazeDialog result = new ResultMazeDialog(2);
            if (result.ShowDialog() == DialogResult.OK)
            {
                this.Enabled = true;
                btn_solve.Enabled = true;
                this.Activate();
                SetComPort();
                return;
            }
            this.Enabled = true;
            this.Activate();
            btn_connect.Enabled = true;
            btn_retry.Enabled = true;
            btn_solve.Enabled = false;
            btn_exit.Enabled = true;

        }
        #endregion

        #region Detect Arduino
        private bool DetectArduino()
        {
            try
            {
                currentPort.Open();
                currentPort.Write("!");
                System.Threading.Thread.Sleep(500);
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

        #region Get Lines From Arduino

        private void GetLinesOfCodeFromArduino()
        {
            try
            {
                if (portFound)
                {
                    currentPort.Open();
                    currentPort.Write("*");
                    System.Threading.Thread.Sleep(800);
                    string a = currentPort.ReadExisting();
                    currentPort.Close();
                    btn_retry.Enabled = true;
                    string[] arr = a.Split('.');
                    int[] arr2 = new int[arr.Length];
                    for(int i = 0; i < arr.Length -1; i++)
                    {
                        arr2[i] = int.Parse(arr[i]);
                    }
                    arr2=NormalizeCodeElse(arr2);
                    foreach (int i in arr2)
                    {
                        int ii = i - (i % 10);
                        foreach (var functionHelper in listOfFunctions)
                        {
                            if (functionHelper.vResistor == ii)
                                listOfLinesFunctions.Add(functionHelper);
                        }
                    }
                    
                    TrySolution();
                    return;
                }
            }
            catch(Exception ex) { SetComPort();MessageBox.Show("Failed to read data from board :(."+ex.ToString()); }   
        }

        #endregion

        #endregion

        #region Try Solution
        private void TrySolution()
        {
            if (isLegalCode())
            {
                if (isInLineWithParameters())
                {
                    Point goal = GetCoordinatesOfGoal();
                    string a = "";
                    foreach (var i in listOfLinesFunctions)
                        a += i.vResistor + " ";
                    //MessageBox.Show(a);
                    for (int i = 0; i < listOfLinesFunctions.Count; i++)
                    {
                        listOfLinesFunctions[i].FunctionToRun(i);

                        if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight,vBreak)))
                        {
                            if (listOfLinesFunctions[i].vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                            {
                                i = GetIndexOfEnd(i);
                            }
                            i = GetIndexOfEnd(i);
                        }

                        if (stopExecution)
                            return;
                        if (isBreak)
                        {
                            isBreak = false;
                            break;
                        }
                    }

                    if (goal == GetCoordinatesOfPlayer())
                    {
                        btn_next.Enabled = true;
                        ResultMazeDialog dialog = new ResultMazeDialog(0);
                        this.Enabled = false;
                        DialogResult result = dialog.ShowDialog();
                        if (result == DialogResult.Yes)
                        {
                            this.Enabled = true;
                            btn_next.PerformClick();
                        }
                        else if (result == DialogResult.Retry)
                        {
                            returnCode = 0;
                            this.DialogResult = DialogResult.OK;
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            returnCode = 21;
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.Enabled = true;
                            btn_next.Enabled = true;
                        }
                    }
                    else
                    {
                        ResultMazeDialog dialog = new ResultMazeDialog(1);
                        this.Enabled = false;
                        DialogResult result = dialog.ShowDialog();
                        if (result == DialogResult.Retry)
                        {
                            returnCode = 0;
                            this.DialogResult = DialogResult.OK;
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            returnCode = 21;
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            dialog.Dispose();
                            this.Enabled = true;
                            this.Focus();
                            this.WindowState = FormWindowState.Normal;
                        }
                        this.Activate();
                    }
                }
                else
                {
                    ResultMazeDialog dialog = new ResultMazeDialog(7);
                    this.Enabled = false;
                    btn_solve.Enabled = true;
                    dialog.ShowDialog();
                    this.Activate();
                }
            }
            else
            {
                
                ResultMazeDialog dialog = new ResultMazeDialog(3);
                this.Enabled = false;
                btn_solve.Enabled = true;
                dialog.ShowDialog();
            }
            btn_connect.Enabled = true;
            this.Enabled=true;
            isAllowed = false;
            this.Activate();
        }
        #endregion

        #region Get/Draw Maze
        private void LoadMaze()
        {
            var maze = CurrentLevel;
            Bitmap bmp = new Bitmap(300, 300);
            Graphics g = Graphics.FromImage(bmp);
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int x = maze[i,j];
                    switch (x)
                    {
                        case 0:
                            g.FillRectangle(new SolidBrush(AlmostBlack), j * 10 * 2, i * 10 * 2, 20, 20);
                            break;
                        case 1:
                            g.FillRectangle(new SolidBrush(Black),j * 10 * 2, i * 10 * 2, 20, 20);
                            break;
                        case 2:
                            g.FillRectangle(new SolidBrush(Purple), j * 10 * 2, i * 10 * 2, 20, 20);
                            switch (Direction)
                            {
                                case 1:
                                    g.DrawPolygon(new Pen(Color.Black), new Point[] { new Point(j * 10 * 2 + 10, i * 10 * 2), new Point(j * 10 * 2 + 5, i * 10 * 2 + 5), new Point(j * 10 * 2 + 15, i * 10 * 2 + 5) });
                                    break;
                                case 2:
                                    g.DrawPolygon(new Pen(Color.Black), new Point[] { new Point(j * 10 * 2 + 20, i * 10 * 2 + 10), new Point(j * 10 * 2 + 15, i * 10 * 2 + 5), new Point(j * 10 * 2 + 15, i * 10 * 2 + 15) });
                                    break;
                                case 3:
                                    g.DrawPolygon(new Pen(Color.Black), new Point[] { new Point(j * 10 * 2 + 10, i * 10 * 2 + 20), new Point(j * 10 * 2 + 5, i * 10 * 2 + 15), new Point(j * 10 * 2 + 15, i * 10 * 2 + 15) });
                                    break;
                                case 4:
                                    g.DrawPolygon(new Pen(Color.Black), new Point[] { new Point(j * 10 * 2, i * 10 * 2 + 10), new Point(j * 10 * 2 + 5, i * 10 * 2 + 5), new Point(j * 10 * 2 + 5, i * 10 * 2 + 15) });
                                    break;
                            }
                            break;
                        case 3:
                            g.FillRectangle(new SolidBrush(Color.Orange), j * 10 * 2, i * 10 * 2, 20, 20);
                            break;
                    }
                }
            }
            mazeBox.Image = bmp;
            System.Threading.Thread.Sleep(100);
        }
        #endregion

        #region Constructor
        public GameScreen(int mazeID, int[,] maze,string [] parameters)
        {
            ArrayOfParameters = parameters;
            m_aeroEnabled = false;
            this.MazeID = mazeID;
            int[,] MazeCopy = maze;
            CurrentLevel = MazeCopy;
            returnCode = 0;
            InitializeComponent();
        }
        #endregion

        #region Form load
        private void GameScreen_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Enabled = true;
            ///
            MaxBlocks = int.Parse(ArrayOfParameters[1]);
            Direction = int.Parse(ArrayOfParameters[2]);
            LoadMaze();
            lbl_title.Font = new Font("Uni Sans Heavy", 10);
            lbl_title.Text = "Level " + (MazeID + 1)+"-"+ArrayOfParameters[0];
            lbl_max.Text = "Max Blocks: " + ArrayOfParameters[1];
            lbl_max.Font = new Font("Uni Sans Heavy", 10);
            this.Icon = RealLifeScratch.Properties.Resources.BlockyLogo;
            btn_exit.Enabled = false;
            btn_connect.Enabled = false;
            btn_retry.Enabled = false;
            Thread T1 = new Thread(SetComPort);
            T1.Start();
            int[] ArrOfValues = new int[] { vMoveForward, vTurnLeft, vTurnRight, vRepeatUntilDone, vIfPathAhead, vIfPathLeft, vIfPathRight, vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhilePathAhead,vWhilePathLeft,vWhilePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight,vElse,vBreak, vEND };
            Action<int>[] ArrOfFunctions = new Action<int>[] { MoveForward, TurnLeft, TurnRight, RepeatUntilDone, IfPathAhead, IfPathLeft, IfPathRight, IfElsePathAhead, IfElsePathLeft, IfElsePathRight, WhilePathAhead,WhilePathLeft,WhilePathRight,WhileElsePathAhead,WhileElsePathLeft,WhileElsePathRight,empty, Break, empty };
            for (int ii = 0;ii < 19; ii++)
            {
                listOfFunctions.Add(new FunctionPointersHelper(ArrOfValues[ii], ArrOfFunctions[ii]));
            }
            this.Activate();
        }
        #endregion

        #region Solve Button Click Event
        private void btn_solve_Click(object sender, EventArgs e)
        {
            //TestSes("130\n170\n100\n200\n190\n120\n200\n110\n200\n200\n200");
            try
            {
                currentPort.Open();
                currentPort.Close();
            }
            catch
            {
                SetComPort();
                return;
            }
            btn_solve.Enabled = false;
            btn_connect.Enabled = false;
            btn_retry.Enabled = false;
            SolveThread = new Thread(GetLinesOfCodeFromArduino);
            SolveThread.Start();
        }
        #endregion

        #region Next Button Click Event
        private void btn_next_Click(object sender, EventArgs e)
        {
            returnCode = MazeID + 1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Controls
        private void empty(int pos)
        {
            
        }
        private void Break(int pos)
        {
            isBreak = true;
        }
        #region Move Forward
        private void MoveForward(int pos)
        {
            Point player = GetCoordinatesOfPlayer();
            if(isLegalMove(Orientation.Ahead))
            {
                CurrentLevel[player.Y, player.X] = 1;
                int i = player.Y, j = player.X;
                i = Direction % 2 == 1 ? Direction == 3 ? i += 1 : i -= 1 : i;
                j = Direction % 2 == 0 ? Direction == 2 ? j += 1 : j -= 1 : j;
                CurrentLevel[i, j] = 2;
            }
            LoadMaze();
        }
        #endregion

        #region Turn
        private void TurnLeft(int pos)
        {
            if (Direction == 1) { Direction = 4; }
            else { Direction--; }
            LoadMaze();
            
        }
        private void TurnRight(int pos)
        {
            if (Direction == 4) { Direction = 1; }
            else { Direction++; }
            LoadMaze();

        }
        #endregion

        #region Repeat Until Done
        private void RepeatUntilDone(int pos)
        {
            Point Player = GetCoordinatesOfPlayer(), Goal = GetCoordinatesOfGoal();
            int counter = 0;
            while (Player != Goal)
            {
                for (int i = pos + 1; i < GetIndexOfEnd(pos); i++)
                {
                    listOfLinesFunctions[i].FunctionToRun(i);
                    if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vBreak)))
                    {
                        if (listOfLinesFunctions[i].vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                        {
                            i = GetIndexOfEnd(i);
                        }
                        i = GetIndexOfEnd(i);
                    }
                }
                Player = GetCoordinatesOfPlayer();
                if (++counter == 300)
                    break;
                if (stopExecution)
                    return;
                if (isBreak)
                {
                    isBreak = false;
                    break;
                }
            }
        }
        #endregion

        #region If
        private void IfBase(int pos,Orientation orientation)
        {
            if (isLegalMove(orientation))
            {
                int a = GetIndexOfEnd(pos);
                for (int i = pos + 1; i < a; i++)
                {
                    listOfLinesFunctions[i].FunctionToRun(i);
                    if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vBreak)))
                    {
                        if (listOfLinesFunctions[i].vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                        {
                            i = GetIndexOfEnd(i);
                        }
                        i = GetIndexOfEnd(i);
                        if (isBreak)
                            return;
                    }
                }
            }
        }
        private void IfPathAhead(int pos)
        {
            IfBase(pos, Orientation.Ahead);
        }
        private void IfPathLeft(int pos)
        {
            IfBase(pos, Orientation.Left);
        }
        private void IfPathRight(int pos)
        {
            IfBase(pos, Orientation.Right);
        }

        #endregion

        #region If else
        private void IfElseBase(int pos,Orientation orientation)
        {

            if (isLegalMove(orientation))
            {
                for (int i = pos + 1; i < GetIndexOfEnd(pos); i++)
                {
                    listOfLinesFunctions[i].FunctionToRun(i);
                    if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vBreak)))
                        i = GetIndexOfEnd(i);
                    if (listOfLinesFunctions[i].In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                    {
                        i = GetIndexOfEnd(i);
                    }
                    if (isBreak)
                        break ;
                }
            }
            else
            {
                

                int b = GetIndexOfEnd(pos);
                int a = GetIndexOfEnd(b);
                for (int i = GetIndexOfEnd(pos) + 1; i < a; i++)
                {
                    listOfLinesFunctions[i].FunctionToRun(i);
                    if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vBreak)))
                    {
                        if (listOfLinesFunctions[i].vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                        {
                            i = GetIndexOfEnd(i);
                        }
                        i = GetIndexOfEnd(i);
                    }
                    if (isBreak)
                        break;
                }
            }
        }
        private void IfElsePathAhead(int pos)
        {
            IfElseBase(pos, Orientation.Ahead);
        }
        private void IfElsePathLeft(int pos)
        {
            IfElseBase(pos, Orientation.Left);
        }
        private void IfElsePathRight(int pos)
        {
            IfElseBase(pos, Orientation.Right);
        }
        #endregion

        #region While
        private void WhileBase(int pos,Orientation orientation)
        {
            Point Player = GetCoordinatesOfPlayer(), Goal = GetCoordinatesOfGoal();
            int counter = 0;
            while (isLegalMove(orientation) && Player != Goal)
            {
                for (int i = pos + 1; i < GetIndexOfEnd(pos); i++)
                {
                    listOfLinesFunctions[i].FunctionToRun(i);
                    if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vBreak)))
                    {
                        if (listOfLinesFunctions[i].vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                        {
                            i = GetIndexOfEnd(i);
                        }
                        i = GetIndexOfEnd(i);
                    }
                }
                Player = GetCoordinatesOfPlayer();
                if (++counter == 300)
                    break;
                if (stopExecution)
                    return;
                if (isBreak)
                {
                    isBreak = false;
                    break;
                }
            }
        }
        private void WhilePathAhead(int pos)
        {
            WhileBase(pos, Orientation.Ahead);
        }
        private void WhilePathLeft(int pos)
        {
            WhileBase(pos, Orientation.Left);
        }
        private void WhilePathRight(int pos)
        {
            WhileBase(pos, Orientation.Right);
        }
        #endregion

        #region While else
        private void WhileElseBase(int pos,Orientation orientation)
        {
            Point Player = GetCoordinatesOfPlayer(), Goal = GetCoordinatesOfGoal();
            int counter = 0;
            if (isLegalMove(orientation))
            {
                while (isLegalMove(orientation) && Player != Goal)
                {
                    for (int i = pos + 1; i < GetIndexOfEnd(pos); i++)
                    {
                        listOfLinesFunctions[i].FunctionToRun(i);
                        if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vBreak)))
                            i = GetIndexOfEnd(i);
                        if (listOfLinesFunctions[i].In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                        {
                            i = GetIndexOfEnd(i);
                        }
                    }
                    Player = GetCoordinatesOfPlayer();
                    if (++counter == 300)
                        break;
                    if (stopExecution)
                        return;
                    if (isBreak)
                    {
                        isBreak = false;
                        break;
                    }
                }
            }
            else
            {
                while (!isLegalMove(orientation) && Player != Goal)
                {
                    int b = GetIndexOfEnd(pos);
                    int a = GetIndexOfEnd(b);
                    for (int i = GetIndexOfEnd(pos) + 1; i < a; i++)
                    {
                        listOfLinesFunctions[i].FunctionToRun(i);
                        if (!(listOfLinesFunctions[i].vResistor.In(vMoveForward, vTurnLeft, vTurnRight, vBreak)))
                        {
                            if (listOfLinesFunctions[i].vResistor.In(vIfElsePathAhead, vIfElsePathLeft, vIfElsePathRight,vWhileElsePathAhead,vWhileElsePathLeft,vWhileElsePathRight))
                            {
                                i = GetIndexOfEnd(i);
                            }
                            i = GetIndexOfEnd(i);
                        }
                    }
                    Player = GetCoordinatesOfPlayer();
                    if (++counter == 300)
                        break;
                    if (stopExecution)
                        return;
                    if (isBreak)
                    {
                        isBreak = false;
                        break;
                    }
                }
            }
        }
        private void WhileElsePathAhead(int pos)
        {
            WhileElseBase(pos, Orientation.Ahead);
        }
        private void WhileElsePathLeft(int pos)
        {
            WhileElseBase(pos, Orientation.Left);
        }
        private void WhileElsePathRight(int pos)
        {
            WhileElseBase(pos, Orientation.Right);
        }
        #endregion

        #endregion

        #region Button Exit Click Event
        private void btn_exit_Click(object sender, EventArgs e)
        {
            stopExecution = true;
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Button Minimize Click Event
        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Button Connect Click Event
        private void btn_connect_Click(object sender, EventArgs e)
        {
            SetComPort();
        }
        #endregion

        #region Button Retry Click Event
        private void btn_retry_Click(object sender, EventArgs e)
        {
            returnCode = 0;
            stopExecution = true;
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region Lbl Title Mouse Move Event
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
    #endregion

    #region Extensions
    public static class Ext
    {
        public static bool In<T>(this T t, params int[] values)
        {
            foreach (var value in values)
            {
                if (t.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
    #endregion
}