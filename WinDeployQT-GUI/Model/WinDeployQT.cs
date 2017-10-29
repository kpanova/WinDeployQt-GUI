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
            winDeployQT.StartInfo = new ProcessStartInfo(fileLink, StaticEntity.userProject.fileLink);
            winDeployQT.Start();
            winDeployQT.OutputDataReceived += (s, e) =>  //здесь подписываемся на событие "что-то появилось в выводе консоли"
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    WinDeployQtText += e.Data;
                    RaisePropertyChanged("WinDeployQtText");
                }
            };
            winDeployQT.Close();
        }

        public void Set()
        {
        }
    }
}
