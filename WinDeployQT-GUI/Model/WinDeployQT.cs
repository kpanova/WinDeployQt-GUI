using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WinDeployQT_GUI.StaticClasses;
using Infrastructure.Shared.Commands;
using WinDeployQT_GUI.Datas;

namespace WinDeployQT_GUI.Model
{
    public class WinDeployQT : BaseProgram
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
            actionName = "Choose destination of qtenv2.bat:";
            getDestination = new RelayCommand(args => getExeDestination());
            Deploy = new RelayCommand(args => Run());
            try
            {
                fileLink = StaticEntity.AppConfigurationSettings.LoadSettings(this.GetType().ToString()); //StaticEntity.configurationDictionary[this.GetType().ToString()];

            }
            catch
            {
                fileLink = Environment.GetEnvironmentVariable("HOMEPATH");
            }
        }        

        public void Run()
        {
            winDeployQT = new Process();
            winDeployQT.StartInfo = new ProcessStartInfo(Environment.GetEnvironmentVariable("ComSpec"), " /A /Q /K " + fileLink) //#TODO применить переменную окружения ComSpec
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };
            StreamWriter streamWriter;
            StreamReader streamReader;
            if (winDeployQT.Start())
            {
                WinDeployQtText += "winDeployQT run";
                RaisePropertyChanged("WinDeployQtText");
                streamWriter = winDeployQT.StandardInput;
                string inputText = "windeployqt " + StaticEntity.userProject.fileLink;
                streamWriter.WriteLine();
                streamWriter.WriteLine(inputText);
                streamWriter.Close();
                streamReader = winDeployQT.StandardOutput;
                while (!streamReader.EndOfStream)
                {
                    WinDeployQtText += "\n" + streamReader.ReadLine();
                    RaisePropertyChanged("WinDeployQtText");
                }
                //streamWriter.WriteLine(outputText);
            }
            WinDeployQtText += "\nDeploy is OK!";
            RaisePropertyChanged("WinDeployQtText");
            winDeployQT.WaitForExit();
        }

        public void getExeDestination()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = fileLink;
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
            try
            {
                StaticEntity.AppConfigurationSettings.Save(GetType().ToString(), fileLink.Replace(fileDialog.SafeFileName, ""));
            }
            catch { }
        }
    }
}
