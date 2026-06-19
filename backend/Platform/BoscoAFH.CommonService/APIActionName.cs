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
        public static class API_Login
        {
            public const string userAuthenticateAsync = nameof(userAuthenticateAsync);
        }
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
        public static class API_Category
        {
            public const string getCategoryAsync = nameof(getCategoryAsync);
            public const string getCategoryListAsync = nameof(getCategoryListAsync);
        }

        public static class API_User
        {
            public const string saveUserAsync = nameof(saveUserAsync);
            public const string updateUserAsync = nameof(updateUserAsync);
            public const string getUserAsync = nameof(getUserAsync);
            public const string getUserbyIdAsync = nameof(getUserbyIdAsync);
            public const string deleteUserAsync = nameof(deleteUserAsync);
        }
    }
}
