using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WinDeployQT_GUI.Interfaces;
using WinDeployQT_GUI.StaticClasses;
using Infrastructure.Shared.Commands;

namespace WinDeployQT_GUI.Model
{
    public class WinDeployQT : BaseProgram, IDataGetter, IDataSetter
    {
        private Process _winDeployQT;
        public Process winDeployQT
        {
            get
            {
                return _winDeployQT;
            }
            set => _winDeployQT = value;
        }
        public Project userProject { get; set; }
        public ICommand Deploy { get; set; }

        private string _winDeployQtText;
        public string WinDeployQtText { get => _winDeployQtText; set => _winDeployQtText = value; }

        public WinDeployQT()
        {
            getDestination = new RelayCommand(args => getExeDestination());
            Deploy = new RelayCommand(args => Run());
        }
        

        public void Get()
        {
        }
        public void Run()
        {
            winDeployQT = new Process();
            winDeployQT.StartInfo = new ProcessStartInfo(@"C:\Windows\System32\cmd.exe", " /A /Q /K " + fileLink)
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            StreamWriter streamWriter;
            StreamReader streamReader;
            if (winDeployQT.Start())
            {
                streamWriter = winDeployQT.StandardInput;
                string inputText = "windeployqt " + StaticEntity.userProject.fileLink;
                streamWriter.WriteLine();
                streamWriter.WriteLine(inputText);
                streamWriter.Close();
                streamReader = winDeployQT.StandardOutput;
                WinDeployQtText += streamReader.ReadToEnd();
                RaisePropertyChanged("WinDeployQtText");
                //streamWriter.WriteLine(outputText);
            }
            WinDeployQtText += "\n Deploy is OK!";
            RaisePropertyChanged("WinDeployQtText");
            winDeployQT.WaitForExit();
            //winDeployQT.CloseMainWindow();

        }

        public void Set()
        {
        }
        public void getExeDestination()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\Qt\Qt5.8.0\5.8\mingw53_32\bin";
            fileDialog.Filter = "bat files (*.bat)|*.bat";
            fileDialog.FilterIndex = 2;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileLink = fileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            RaisePropertyChanged("fileLink");
        }
    }
}
