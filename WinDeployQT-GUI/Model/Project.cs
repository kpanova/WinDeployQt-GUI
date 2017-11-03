using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinDeployQT_GUI.Interfaces;
using Infrastructure.Shared.Commands;

namespace WinDeployQT_GUI.Model
{
    public class Project : BaseProgram, IDataGetter, IDataSetter
    {
        public Project()
        {
            actionName = "Choose destination of your project:";
            getDestination = new RelayCommand(args => getExeDestination());
            fileLink = @"C:\Users";
        }
        public void Get()
        {
        }

        public void Set()
        {
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
        }

    }
}
