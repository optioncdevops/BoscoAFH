using System.ComponentModel;

namespace BoscoAFH.Common
{
    /// <summary>
    /// A static class containing various enumeration types used throughout the application.
    /// </summary>
    public static class EnumVariables
    {
        public enum AppointmentCancellationScope
        {
            [Description("Whole Day")]
            WholeDay = 1,

            [Description("Time Range")]
            TimeRange = 2,

            [Description("Slot Wise")]
            SlotWise = 3,

            [Description("Recurring")]
            Recurring = 4
        }
        public abstract class DefaultValues
        {
            public static string Zero = "0";
            public static string ONE = "1";
            public static string TWO = "2";
            public static string THREE = "3";
            public static string FOUR = "4";
            public static string FIVE = "5";
            public static string SIX = "6";
            public static string SEVEN = "7";
            public static string EIGHT = "8";
            public static string NINE = "9";
            public static string TEN = "10";
            public static string NINETYNINE = "-99";
            public static string NINETYEIGHT = "-98";

            public static string MAXLENGTH = "maxlength";
            public static string IMAGESIZE = "1500X400_";
            public static string TILE = @"~\";
            public static string REVIEWER = "1";
            public static string APPROVER = "2";
            public static string INPROGRESS = "1";
            public static string REVIEW = "2";
            public static string INDEXIMAGESIZE = "400X400_";

            public static string TRUE = "True";
            public static string FALSE = "False";
            public static int PAGEINDEX = 12;
            public static string PASSWORD = "";

            public static string ERROR = "ERROR";

            public static string CONTACTPROFILEIMAGESIZE = "120X120_";

            public static int Conflict = -99;
        }

       
    }
}
