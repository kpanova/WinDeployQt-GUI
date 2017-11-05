using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinDeployQT_GUI.Model;

namespace WinDeployQT_GUI
{
    public class ViewModel : BaseViewModel
    {
        public BaseProgram userProject { get; set; }
        public BaseProgram winDeployQt { get; set; }

        public ViewModel()
        {
            userProject = new Project();
            winDeployQt = new WinDeployQT();
            StaticClasses.StaticEntity.userProject = userProject;
        }
    }
}
