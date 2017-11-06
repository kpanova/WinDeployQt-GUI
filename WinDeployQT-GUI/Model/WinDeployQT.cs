using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WinDeployQt_GUI.StaticClasses;
using Infrastructure.Shared.Commands;
using WinDeployQt_GUI.Datas;

namespace WinDeployQt_GUI.Model
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

        private string _winDeployQtText="Results... \n";
        public string WinDeployQtText { get => _winDeployQtText; set => _winDeployQtText = value; }
        private string qtenv2Link = "qtenv2.bat";

        public WinDeployQT()
        {
            actionName = "Choose destination of windeployqt.exe:";
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
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            winDeployQT = new Process();
            winDeployQT.StartInfo = new ProcessStartInfo(Environment.GetEnvironmentVariable("ComSpec"), " /A /Q /K " + qtenv2Link + "qtenv2.bat")
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
                WinDeployQtText += "winDeployQT run\n";
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
            }
            WinDeployQtText += "\nDeploy is OK!";
            RaisePropertyChanged("WinDeployQtText");
            winDeployQT.WaitForExit();
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        public void getExeDestination()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = fileLink;
            fileDialog.Filter = "exe files (*.exe)|*.exe";
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
            try
            {
                qtenv2Link = fileLink.Replace(fileDialog.SafeFileName, "");
                StaticEntity.AppConfigurationSettings.Save(GetType().ToString(), qtenv2Link);
            }
            catch { }
            RaisePropertyChanged("fileLink");
        }
    }
}
