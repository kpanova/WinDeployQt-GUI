using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infrastructure.Shared.Commands;
using WinDeployQt_GUI.Datas;
using WinDeployQt_GUI.StaticClasses;

namespace WinDeployQt_GUI.Model
{
    public class Project : BaseProgram
    {
        public Project()
        {
            actionName = "Choose destination of your project:";
            getDestination = new RelayCommand(args => getExeDestination());
            try
            {
                fileLink = StaticEntity.AppConfigurationSettings.LoadSettings(this.GetType().ToString()); //StaticEntity.configurationDictionary[this.GetType().ToString()];

            }
            catch {
                fileLink = Environment.GetEnvironmentVariable("HOMEPATH");
            }
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
            RaisePropertyChanged("fileLink");
            try
            {
                StaticEntity.AppConfigurationSettings.Save(GetType().ToString(), fileLink.Replace(fileDialog.SafeFileName, ""));
            }
            catch { }
        }

    }
}