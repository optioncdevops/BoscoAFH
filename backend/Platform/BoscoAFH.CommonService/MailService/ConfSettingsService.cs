using Newtonsoft.Json;

namespace BoscoAFH.CommonService.MailService
{
    public class ConfSettings
    {
        public SMTPMailConfig? SMTPMailConfig { get; set; }
    }

    public class SettingConfiguration
    {
        public SMTPMailConfig SMTPMailConfig { get; set; }
        public ApplicationFilePath ApplicationFilePath { get; set; }
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettings
    {
        public string DocBaseURL { get; set; }
        public string ExpTrainingSchedule { get; set; }

    }
    public class ApplicationFilePath
    {
        public string Doc_BasePath { get; set; }
        public string OldDocBaseURL { get; set; }
        public string OldDocTicket { get; set; }
        public string OldSaintImagePath { get; set; }
        public string ExistingStaticFilePath { get; set; }
        public string StaticFilePath { get; set; }
        public string NextGenBaseURL { get; set; }
        public string ComponentBaseURL { get; set; }
        public string eFormBaseURL { get; set; }
        public string ReportManagerBaseURL { get; set; }
    }

    public class SMTPMailConfig
    {
        public string SendMailFlag { get; set; }
        public string SMTPServer { get; set; }
        public string SMTPPort { get; set; }
        public string DisplayName { get; set; }
        public string MUserName { get; set; }
        public string MPassword { get; set; }
        public string IsSSLEnabled { get; set; }
        public string CCMailId { get; set; }
        public string LoginURL { get; set; }
        public string ContactUsMailId { get; set; }
        public string LogoUrl { get; set; }
    }

    public class ConfSettingsService
    {
        public ConfSettings Settings = new();

        public ConfSettingsService()
        {
            Settings = LoadData();
        }

        /// <summary>
        /// To get the Value from json files
        /// </summary>
        /// <returns></returns>
        public ConfSettings LoadData()
        {
            var confSettings = new ConfSettings();
            try
            {
                var sBaseDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net10.0", "");
                var sCSSFileName = Path.Combine(sBaseDirectory, "_configurationSettings.json");

                if (File.Exists(sCSSFileName))
                {
                    var jsonString = File.ReadAllText(sCSSFileName);
                    confSettings = JsonConvert.DeserializeObject<ConfSettings>(jsonString) ?? new ConfSettings(); // Ensure non-null value
                }
            }
            catch (Exception)
            {
                // Optionally log the exception
            }
            return confSettings;
        }
    }
}
