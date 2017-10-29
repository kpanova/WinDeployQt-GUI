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
            actionName = "Chose destination of your project:";
            getDestination = new RelayCommand(args => getExeDestination());
        }
        public void Get()
        {
        }

        public void Set()
        {
        }
        
    }
}
