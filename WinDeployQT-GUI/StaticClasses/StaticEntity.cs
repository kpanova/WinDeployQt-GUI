using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WinDeployQT_GUI.Model;
using WinDeployQT_GUI.Datas;

namespace WinDeployQT_GUI.StaticClasses
{
    public static class StaticEntity
    {
        public static BaseProgram userProject { get; set; }
        public static AppConfiguration AppConfigurationSettings { get; set; }
    }
}
