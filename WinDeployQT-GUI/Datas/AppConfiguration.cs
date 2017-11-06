using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WinDeployQt_GUI.Datas
{
    public class AppConfiguration
    {
        private string SectionName { get; set; }
        private Configuration configFile { get; set; }
        private AppSettingsSection Section { get; set; }

        public AppConfiguration(string sectionName)
        {
            LoadConfiguration(sectionName);
        }

        public Dictionary<string,string> LoadAllSettings()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            try
            {
                var section = (AppSettingsSection)configFile.Sections[SectionName];
                var appSettings = section.Settings;

                if (appSettings.Count == 0)
                {
                    MessageBox.Show("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        settings.Add(key, appSettings[key].Value);
                    }
                }
                return settings;
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error reading app settings");
                return null;
            }
        }

        public string LoadSettings(string key)
        {
            try
            {
                var section = (AppSettingsSection)configFile.Sections[SectionName];
                var appSettings = section.Settings;
                string result = appSettings[key]?.Value ?? null;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error reading app settings");
                return null;
            }

        }

        public void LoadConfiguration(string sectionName)
        {
            this.SectionName = sectionName;
            this.configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            if (configFile.Sections[SectionName] == null)
            {
                Section = new AppSettingsSection();
                Section.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
                configFile.Sections.Add(SectionName, Section);
            }
        }
        public void Save(string key, string value)
        {
            try
            {
                var section = (AppSettingsSection)configFile.Sections[SectionName];
                var settings = section.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(section.SectionInformation.SectionName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message+"\n"+e?.InnerException?.ToString());
            }

        }
        public void Clear()
        {
            configFile = null;
        }
    }
}
