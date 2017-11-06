using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WinDeployQt_GUI.Model;
using WinDeployQt_GUI.Datas;

namespace WinDeployQt_GUI.StaticClasses
{
    public static class StaticEntity
    {
        public static BaseProgram userProject { get; set; }
        public static AppConfiguration AppConfigurationSettings { get; set; }
    }
}
