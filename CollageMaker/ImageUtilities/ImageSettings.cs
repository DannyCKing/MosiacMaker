using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.ImageUtilities
{
    public class ImageSettings
    {
        public const string TARGET_IMAGE_SETTING = "TargetImage";

        public const string SOURCE_FOLDER_SETTING = "SourceFolder";

        public const string X_COUNT_SETTING = "XCount";

        public const string Y_COUNT_SETTING = "YCount";

        private const string SETTINGS_FILE_NAME = "image_settings.txt";

        private const char DELIMETER = '=';
        private static string SETTINGS_FOLDER_NAME
        {
            get
            {
                var settingsFolder = Path.Combine(FileUtils.AppDataPath, "Settings");
                if(!Directory.Exists(settingsFolder))
                {
                    Directory.CreateDirectory(settingsFolder);
                }
                return settingsFolder;
            }
        }


        private static string SETTINGS_FILE_FULL_PATH
        {
            get
            {
                var settingsFileFullPath = Path.Combine(SETTINGS_FOLDER_NAME, SETTINGS_FILE_NAME);
                return settingsFileFullPath;
            }
        }

        public Dictionary<string, ImageSetting> SettingValues = new Dictionary<string, ImageSetting>();

        public ImageSettings() 
        {
        }

        public void LoadFromFile()
        {
            if(File.Exists(SETTINGS_FILE_FULL_PATH))
            {
                var lines = File.ReadAllLines(SETTINGS_FILE_FULL_PATH);
                foreach(var line in lines)
                {
                    try
                    {
                        var splitItems = line.Split(DELIMETER);
                        ImageSetting imageSetting = new ImageSetting(splitItems[0], splitItems[1]);
                        SettingValues[imageSetting.SettingName] = imageSetting;
                    }
                    catch 
                    {
                        // ignore for now
                    }
                }
            }
            else
            {
                // write defaults
                SettingValues = new Dictionary<string, ImageSetting>();
                SettingValues[TARGET_IMAGE_SETTING] = new ImageSetting(TARGET_IMAGE_SETTING, "");
                SettingValues[SOURCE_FOLDER_SETTING] = new ImageSetting(SOURCE_FOLDER_SETTING, "");
                SettingValues[X_COUNT_SETTING] = new ImageSetting(X_COUNT_SETTING, 20);
                SettingValues[Y_COUNT_SETTING] = new ImageSetting(Y_COUNT_SETTING, 20);

                SaveToFile();
            }
        }

        public void SaveToFile()
        {
            var outputFileText = "";

            var outputSettingsKeys = SettingValues.Keys.Order().ToList();

            foreach(var settingKey in outputSettingsKeys)
            {
                var setting = SettingValues[settingKey];
                outputFileText += setting.SettingName +  DELIMETER.ToString() + setting.SettingStringValue + Environment.NewLine;
            }

            File.WriteAllText(SETTINGS_FILE_FULL_PATH, outputFileText);
        }

        public string GetStringSetting(string settingName)
        {
            if(SettingValues.ContainsKey(settingName))
            {
                return SettingValues[settingName].SettingStringValue;
            }
            else
            {
                return null;
            }
        }

        public int GetintSetting(string settingName)
        {
            if (SettingValues.ContainsKey(settingName))
            {
                return SettingValues[settingName].IntValue;
            }
            else
            {
                return 0;
            }
        }

        internal void Set(string settingName, string text)
        {
            SettingValues[settingName] = new ImageSetting(settingName, text);
            SaveToFile();
        }
    }

    public class ImageSetting
    {
        public string SettingName { get; set; }
        public string SettingStringValue { get; set; }

        public bool BoolValue
        {
            get
            {
                if (bool.TryParse(SettingStringValue, out bool boolValue))
                {
                    return boolValue;
                }

                else if (SettingStringValue == "1")
                {
                    return true;
                }

                else if (SettingStringValue == "0")
                {
                    return false;
                }

                throw new InvalidOperationException($"Unable to parse bool value of {SettingStringValue}");
            }
        }

        public int IntValue
        {
            get
            {
                if (int.TryParse(SettingStringValue, out int intValue))
                {
                    return intValue;
                }

                throw new InvalidOperationException($"Unable to parse int value of {SettingStringValue}");
            }
        }

        public ImageSetting(string settingName, string settingValue)
        {
            SettingName = settingName;
            SettingStringValue = settingValue;
        }

        public ImageSetting(string settingName, int settingValue)
        {
            SettingName = settingName;
            SettingStringValue = settingValue.ToString();
        }

        public ImageSetting(string settingName, bool settingValue)
        {
            SettingName = settingName;
            SettingStringValue = settingValue.ToString();
        }
    }
}
