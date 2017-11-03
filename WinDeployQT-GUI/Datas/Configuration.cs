using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WinDeployQT_GUI.Datas
{
    public class Configuration
    {
        public string Link { get; set; }
        public XmlDocument xmlDocument { get; set; }
        public int number { get; set; }

        public Configuration() { }
        public Configuration(int numder)
        {
            this.number = number;
        }

        public bool generateConfiguration(string ProjectLink, string WinDeployQTLink)
        {
            try
            {
                xmlDocument = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDocument.DocumentElement;
                xmlDocument.InsertBefore(xmlDeclaration, root);

                //(2) string.Empty makes cleaner code
                XmlElement configuration = xmlDocument.CreateElement(string.Empty, "Configuration", string.Empty);
                xmlDocument.AppendChild(configuration);

                XmlElement baseProgram = xmlDocument.CreateElement(string.Empty, "BaseProgram", string.Empty);
                configuration.AppendChild(baseProgram);

                XmlElement project = xmlDocument.CreateElement(string.Empty, "Project", string.Empty);
                baseProgram.AppendChild(project);

                XmlElement fileLinkProject = xmlDocument.CreateElement(string.Empty, "fileLink", string.Empty);
                XmlText projectLink = xmlDocument.CreateTextNode(ProjectLink);
                fileLinkProject.AppendChild(projectLink);
                project.AppendChild(fileLinkProject);


                XmlElement windeployqt = xmlDocument.CreateElement(string.Empty, "WinDeployQT", string.Empty);
                baseProgram.AppendChild(windeployqt);

                XmlElement fileLinkWinDeployQT = xmlDocument.CreateElement(string.Empty, "fileLink", string.Empty);
                XmlText winDeployQTLink = xmlDocument.CreateTextNode(WinDeployQTLink);
                fileLinkWinDeployQT.AppendChild(winDeployQTLink);
                windeployqt.AppendChild(fileLinkWinDeployQT);
                Link = @"C:\Users\Default\AppData\Local\config-" + number + ".xml";
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                xmlDocument.Save(Link);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Load()
        {
            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(Link);
                return true;
            }
            catch
            {

                return false;
            }
        }

    }
}
