using System;
using System.ComponentModel.DataAnnotations;

namespace BoscoAFH.MasterInfrastructure.Models.Input
{
    public class UserDTO
    {
        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Unique username of the user
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Plain text password of the user (used for registration/saving/updating)
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Full name of the user
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; } = null!;

        /// <summary>
        /// Unique email address of the user
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Mobile number of the user
        /// </summary>
        public string? MobileNo { get; set; }

        /// <summary>
        /// Status: active or not (TRUE = active, FALSE = inactive)
        /// </summary>
        public bool IsActive { get; set; }

         
    }
}
