namespace BoscoAFH.CommonService
{
    public static class CommonActionName
    {
        public static class Common_API
        {
            public const string getWeatherForecast = nameof(getWeatherForecast);
        }
    }

    public static class APIActionName
    {
        public static class API_Role
        {
            public const string saveRoleAsync = nameof(saveRoleAsync);
            public const string updateRoleAsync = nameof(updateRoleAsync);
            public const string getRoleAsync = nameof(getRoleAsync);
            public const string getRolesbyIdAsync = nameof(getRolesbyIdAsync);
            public const string deleteRoleAsync = nameof(deleteRoleAsync);
            public const string updateRoleStatus = nameof(updateRoleStatus);
            public const string getRoleDropdownAsync = nameof(getRoleDropdownAsync);
        }
    }
}
