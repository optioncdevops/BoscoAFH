namespace BoscoAFH.Common
{
    
    public class UserDetailDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public long UserId { get; set; } = 0;

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Fullname
        /// </summary>
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the role identifier of the user.
        /// </summary>
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }

        public string Token { get; set; }
    }
}
