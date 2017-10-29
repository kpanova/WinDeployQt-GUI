using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WinDeployQT_GUI.Model
{
    public class BaseProgram : BaseViewModel
    {
        public string actionName { get; set; }
        public string fileLink { get; set; }
        public ICommand getDestination { get; set; }
        public event UI RunDeploy;
        
    }
}
