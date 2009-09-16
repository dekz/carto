﻿using System;
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
            g_elecImporter.ParseXLS(_xlsData);
            g_elecImporter.ParseMID(_midData);
            g_elecImporter.ParseMIF(_mifData);
            m_Electorates = g_elecImporter.MergeData();
            Exporter m_exporter = new Exporter(m_Electorates);
            m_exporter.convertToKml();
            convertPB.Image = (Image)pic.ResourceManager.GetObject("Tick");
            MessageBox.Show("Created KML File from XLS and MID/MIF Data");

        }

        private void exit_Click(object sender, EventArgs e)
        {
           
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



    }
}
