namespace BoscoAFH.Common
{
    public static class Constant
    {
        public static class InputType
        {
            public const string ApplicationJson = "application/json";
            public const string Text = "Text";
        }
        public static class Property
        {
            //public const string APIController = "api/[controller]/[action]";
            public const string APIController1 = "api/v{version:apiVersion}/[controller]/[action]";
            public const string APIController = "api/v1/[controller]/[action]";
            public const string AllowOrigin = "AllowOrigin";
            public const string VersionV1 = "1";
            public const string VersionV2 = "2";
        }

        public static class DBConectionName
        {
            public const string ConnString = "ConnString";
            public const string AuditLogDB = "AuditLogDB";
        }

        public static class JWTTypes
        {
            public const string CanSeeAll = "CanSeeAll";
            public const string DepartmentId = "DepartmentId";
            public const string LocationId = "LocationId";
        }

        public static class AuditTableName
        {
            public const string Feedback = "feedback";
            public const string Master = "master";
            public const string Auth = "auth";
        }

        public static class ExcelSettings
        {
            public const string ProtectionPassword = "PMbyLNTHCIAPApps#2026";
            public const string DateTimeFormat = "yyyyMMdd_HHmmss";
        }



        public static class ResultDataKeys
        {
            public const string Records = "records";
            public const string PreviewRecords = "PreviewRecords";
            public const string Dropdowns = "Dropdowns";
        }

        public static class ErrorMessage
        {
            public const string NoFileProvided = "No file provided.";
            public const string NoImportDataProvided = "No import data provided.";
        }

        public static class CommonValues
        {
            public const string CopySuffix = "_Copy";
        }

        public static class SwaggerModuleDoc
        {
            public const string LNTHCIAPAppsBGateway = "LNTHCIAPApps.Gateway";

            /// <summary>
            /// RebarPro Potal
            /// </summary>
            public const string RebarPro = "RebarPro";
            public const string RebarProDocs = "RebarPro.Administration,RebarPro.Master,RebarPro.Reports,RebarPro.Projects";
            public const string RebarProAdministration = "RebarPro.Administration";
            public const string RebarProMaster = "RebarPro.Master";
            public const string RebarProReports = "RebarPro.Reports";
            public const string RebarProProjects = "RebarPro.Projects";

            /// <summary>
            /// AccessHub Potal
            /// </summary>
            public const string BoscoAFHAuthDocs = "BoscoAFH.Auth";
            public const string BoscoAFHAuth = "BoscoAFH.Auth";

            /// <summary>
            /// AccessHub Potal
            /// </summary>
            public const string BoscoAFHFeedbackDocs = "BoscoAFH.Feedback";
            public const string BoscoAFHFeedback = "BoscoAFH.Feedback";

            /// <summary>
            /// AccessHub Potal
            /// </summary>
            public const string BoscoAFHMasterDocs = "BoscoAFH.Master";
            public const string BoscoAFHMaster = "BoscoAFH.Master";



        }
        public static class SMTPText
        {
            public const string ViperChangePassword = "This is your new password for Viper LogIn";
        }
        public static class NotificationText
        {
            public const string NotificationT = "";
        }

        public static class NotificationTitle
        {
            public const string NotificationT = "";
        }
        public static class SwaggerDocs
        {
            public const string TestDescription = @"The {0} API enable applications to read and write  data stored in an {0} through a secure REST interface. <hr> Note: Consumers of {0} API information should sanitize all data for display and storage. The {0} provides reasonable safeguards against cross-site scripting attacks and other malicious content, but the platform does not and cannot guarantee that the data it contains is free of all potentially harmful content. <hr>";
            public const string Description = @"The {0} API enables applications to securely read and write data through a RESTful interface. <hr> Note: Consumers of the {0} API should ensure that all data is properly sanitized before display and storage. While the {0} API includes safeguards against cross-site scripting (XSS) and other malicious content, it cannot guarantee that all data is free from potentially harmful content. Therefore, it is crucial to implement additional security measures in your application. <hr>";
            public const string Contact_Us = "us";
            public const string Version = "v1";
            public const string Contact_Email = "dev@boscosofttech.com";
            public const string SwaggerDefault = "string";
        }

        public static class JWTDocs
        {
            public const string Description = "JWT Authorization header using the Bearer scheme";
            public const string Bearer = "Bearer";
            public const string Authorization = "Authorization";
            public const string JWT = "JWT";
            public const string Scheme = "Bearer";
        }

        public static class Common
        {
            public const string asc = nameof(asc);
            public const short Five = 5;
            public const short Four = 4;
            public const short Two = 2;
            public const short One = 1;
            public const short Six = 6;
            public const short Three = 3;
            public const short Eight = 8;
            public const short Seven = 7;
            public const short Nine = 9;
            public const short Zero = 0;
            public const short Ten = 10;
            public const short Eleven = 11;
            public const short Twelve = 12;
            public const short Twenty = 20;
            public const string ReportSettings = nameof(ReportSettings);
            public const string StringOne = "01";
            public const string StringTwo = "02";
            public const string StringThree = "03";
        }
    }
}
