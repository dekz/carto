using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO;

using EARTHLib;

namespace cartographer
{
    public partial class Cartographer : Form
    {
        public EARTHLib.ApplicationGE ge = null; //new ApplicationGEClass();
        public List<Electorate> m_Electorates;
        public DataGrabber g_Grabber;
        private string colourMode = "Party";
        public string ColourMode { get; set; }

        private string _midData;
        private string _mifData;
        private string _xlsData;
        #region COM Hacks

        [DllImport("user32.dll")]
        private static extern int SetParent(
        int hWndChild,
        int hWndParent);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(
        int hWnd,
        int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(
        int hWnd,
        uint Msg,
        int wParam,
        int lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern bool SetWindowPos(
        int hWnd,
        int hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        uint uFlags);

        [DllImport("user32.dll")]
        private static extern int SendMessage(
        int hWnd,
        uint Msg,
        int wParam,
        int lParam);

        private const int HWND_TOP = 0x0;
        private const int WM_COMMAND = 0x0112;
        private const int WM_QT_PAINT = 0xC2DC;
        private const int WM_PAINT = 0x000F;
        private const int WM_SIZE = 0x0005;
        private const int SWP_FRAMECHANGED = 0x0020;



        #endregion

        public Cartographer()
        {
            m_Electorates = new List<Electorate>();
            ge = new ApplicationGEClass();
            ShowWindowAsync(ge.GetMainHwnd(), 0);
            InitializeComponent();
            SetParent(ge.GetRenderHwnd(), this.Handle.ToInt32());
            ResizeGoogleControl();
        }

        private void ResizeGoogleControl()
        {
            SendMessage(ge.GetMainHwnd(), WM_COMMAND, WM_PAINT, 0);
            PostMessage(ge.GetMainHwnd(), WM_QT_PAINT, 0, 0);

            SetWindowPos(
            ge.GetMainHwnd(),
            HWND_TOP,
            0,
            0,
            (int)this.Width,
            (int)this.Height,
            SWP_FRAMECHANGED);

            SendMessage(ge.GetRenderHwnd(), WM_COMMAND, WM_SIZE, 0);
        }

        private void Cartographer_Resize(object sender, EventArgs e)
        {
            ResizeGoogleControl();
        }

        private void loadKML_Click(object sender, EventArgs e)
        {
                      
            OpenFileDialog _fDialog = new OpenFileDialog();
            _fDialog.Title = "Select KML Data File";
            _fDialog.Filter = "KML Files|*.kml";
            _fDialog.InitialDirectory = "data/";


            string _textFile = "data/kml.kml";

            if (_fDialog.ShowDialog() == DialogResult.OK)
            {
                _fDialog.CheckFileExists = true;
                _textFile = _fDialog.FileName.ToString();
            }

            ge.OpenKmlFile(_textFile, 1);
            //ge.

        }

        [DllImport("user32")]
        public static extern int GetWindowThreadProcessId(int hwnd, ref int lpdwProcessId);

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            int PID = 0;

            int handle = ge.GetMainHwnd();

            GetWindowThreadProcessId(handle, ref PID);

            Process process = null;
            try
            {
                process = Process.GetProcessById(PID);
            }
            catch
            {
                //process not found
            }
            if (process != null)
            {
                process.Kill();
            }

        }

        private void convertData_Click(object sender, EventArgs e)
        {
            ElectorateImporter g_elecImporter = new ElectorateImporter();
            try { g_elecImporter.ParseXLS(_xlsData); }
            catch { }
            try { g_elecImporter.ParseMID(_midData); }
            catch { }
            try { g_elecImporter.ParseMIF(_mifData); }
            catch { }
            m_Electorates = g_elecImporter.MergeData();
            g_elecImporter.MergeDataPhaseTwo(m_Electorates, "data/Qld_Federal-State Electorate Mapping.xls", "data/Federal Election Results-Qld-2004.xls", "data/Qld_State Results by Electorate-2006.xls"); //!TESTING
            convertPB.Image = (Image)pic.ResourceManager.GetObject("Tick");

           // m_Electorates[0].Name;
            for (int i =0; i < m_Electorates.Count; i++)
                pointBox.Items.Add(m_Electorates[i].Name, CheckState.Checked);
            g_Grabber = new DataGrabber();
            g_Grabber.importData();


            Exporter m_exporter = new Exporter(m_Electorates);
            m_exporter.convertToKml(colourMode);
            MessageBox.Show("Saved KML File from XLS and MID/MIF Data");

        }


        private void loadSav()
        {

            if (File.Exists("data/last.sav"))
            {
                Console.Out.WriteLine("Sav file exists");
                StreamReader tr = new StreamReader("data/last.sav");
                if ((_mifData = tr.ReadLine()) != null)
                {
                    Console.Out.WriteLine("Mif not null");
                    if (File.Exists(_mifData))
                    {
                        mifPB.Image = (Image)pic.ResourceManager.GetObject("Tick");
                    }
                    else
                    {
                        MessageBox.Show("Error: Empty Save file");
                        return;
                    }
                }
                else
                {
                    return;
                }

                if ((_midData = tr.ReadLine()) != null)
                {
                    Console.Out.WriteLine("mid not null");
                    if (File.Exists(_midData))
                    {
                        midPB.Image = (Image)pic.ResourceManager.GetObject("Tick");
                    }
                    else
                    {
                        MessageBox.Show("Error: Empty Save file");
                        return;
                    }

                }
                else
                {
                    return;
                }

                if ((_xlsData = tr.ReadLine()) != null)
                {
                    Console.Out.WriteLine("Mif not null");
                    if (File.Exists(_xlsData))
                    {
                        xlsPB.Image = (Image)pic.ResourceManager.GetObject("Tick");
                    }
                    else
                    {
                        MessageBox.Show("Error: Empty Save file");
                        return;
                    }


                }
                else
                {
                    return;
                }
                tr.Close();

                convertData_Click(this, null);
            }
            else
            {
                MessageBox.Show("No Last save file");
            }


        }


        private void exit_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("data/last.sav", FileMode.Create);
            fs.Close();
            StreamWriter tw = new StreamWriter("data/last.sav");
            tw.WriteLine(_mifData);
            tw.WriteLine(_midData);
            tw.WriteLine(_xlsData);
            tw.Close();

            Application.Exit();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void mifBut_Click(object sender, EventArgs e)
        {

            OpenFileDialog _fDialog = new OpenFileDialog();
            _fDialog.Title = "Select MIF Data File";
            _fDialog.Filter = "MIF Files|*.mif";
            _fDialog.InitialDirectory = "data/";


            _mifData = "";

            if (_fDialog.ShowDialog() == DialogResult.OK)
            {
                _fDialog.CheckFileExists = true;
                _mifData = _fDialog.FileName.ToString();
                mifLab.Text = "MIF Loaded";
               mifPB.Image = (Image)pic.ResourceManager.GetObject("Tick");
            }

        }

        private void midBut_Click(object sender, EventArgs e)
        {

            OpenFileDialog _fDialog = new OpenFileDialog();
            _fDialog.Title = "Select MID Data File";
            _fDialog.Filter = "MID Files|*.mid";
            _fDialog.InitialDirectory = "data/";


            _midData = "";

            if (_fDialog.ShowDialog() == DialogResult.OK)
            {
                _fDialog.CheckFileExists = true;
                _midData = _fDialog.FileName.ToString();
                midLab.Text = "MID Loaded";
                midPB.Image = (Image)pic.ResourceManager.GetObject("Tick");
            }

        }

        private void xlsBut_Click(object sender, EventArgs e)
        {

            OpenFileDialog _fDialog = new OpenFileDialog();
            _fDialog.Title = "Select XLS Data File";
            _fDialog.Filter = "XLS Files|*.xls";
            _fDialog.InitialDirectory = "data/";


            _xlsData = "";

            if (_fDialog.ShowDialog() == DialogResult.OK)
            {
                _fDialog.CheckFileExists = true;
                _xlsData = _fDialog.FileName.ToString();
                xlsLab.Text = "XLS Loaded";
                xlsPB.Image = (Image)pic.ResourceManager.GetObject("Tick");
            }

        }

        private void generateBut_Click(object sender, EventArgs e)
        {
            List<String> _checkList = new List<string>();
            foreach (object itemChecked in pointBox.CheckedItems)
            {
                _checkList.Add(itemChecked.ToString());
            }

            List<Electorate> _electorates =new List<Electorate>();

            for (int i = 0; i < _checkList.Count; i++)
            {
                if (_checkList.Contains(m_Electorates[i].Name))
                {
                    _electorates.Add(m_Electorates[i]);
                    FederalElectorate fed = g_Grabber.FederalElectorates.Find(delegate(FederalElectorate f) { return f.Name == m_Electorates[i].Name; });
                    m_Electorates[i].SeatSafety = fed.SeatSafety;
                    m_Electorates[i].WinningParty = fed.WinningParty;

                    // SomeObject desiredObject = myObjects.Find(delegate(SomeObject o) { return o.Id == desiredId; });
                }
            }

            Exporter _exporter = new Exporter(_electorates);
            _exporter.convertToKml(colourMode);
            string _textFile = "";
            if (File.Exists("data/kml.kml"))
            {
                _textFile = Directory.GetCurrentDirectory() + "\\data\\kml.kml";
                Console.Out.WriteLine("file exists");
            }

            Console.Out.WriteLine("Saved KML File " + _textFile);
            ge.OpenKmlFile(_textFile, 1);

        }

        private void loadBut_Click(object sender, EventArgs e)
        {
            loadSav();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                colourMode = "Party";
            }
            else if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                colourMode = "Safety";
            }

        }

        //north
        private void button1_Click(object sender, EventArgs e)
        {
            CameraInfoGEClass cam = new CameraInfoGEClass();
            cam.FocusPointLatitude = -15.5f;
            cam.FocusPointLongitude = 143.3f;
            cam.FocusPointAltitude = 600000.0f;
            ge.SetCamera(cam, 1.0f);
        }
        //west
        private void button2_Click(object sender, EventArgs e)
        {
            CameraInfoGEClass cam = new CameraInfoGEClass();
            cam.FocusPointLatitude = -23.3f;
            cam.FocusPointLongitude = 143.3f;
            cam.FocusPointAltitude = 700000.0f;
            ge.SetCamera(cam, 1.0f);
        }
        //southeast
        private void button3_Click(object sender, EventArgs e)
        {
            CameraInfoGEClass cam = new CameraInfoGEClass();
            cam.FocusPointLatitude = -27.23;
            cam.FocusPointLongitude = 152.3f;
            cam.FocusPointAltitude = 291000.0f;
            ge.SetCamera(cam, 1.0f);
        }

        //sunshine
        private void button4_Click(object sender, EventArgs e)
        {
            CameraInfoGEClass cam = new CameraInfoGEClass();
            cam.FocusPointLatitude = -26.55;
            cam.FocusPointLongitude = 153f;
            cam.FocusPointAltitude = 80000.0f;
            ge.SetCamera(cam, 1.0f);
        }
        //gold
        private void button5_Click(object sender, EventArgs e)
        {
            CameraInfoGEClass cam = new CameraInfoGEClass();
            cam.FocusPointLatitude = -28.0f;
            cam.FocusPointLongitude = 153.4f;
            cam.FocusPointAltitude = 41000.0f;
            ge.SetCamera(cam, 1.0f);
        }
        //bris
        private void button6_Click(object sender, EventArgs e)
        {
            CameraInfoGEClass cam = new CameraInfoGEClass();
            cam.FocusPointLatitude = -27.5;
            cam.FocusPointLongitude = 153;
            cam.FocusPointAltitude = 60000.0f;
            ge.SetCamera(cam, 1.0f);
        }

    }
}
