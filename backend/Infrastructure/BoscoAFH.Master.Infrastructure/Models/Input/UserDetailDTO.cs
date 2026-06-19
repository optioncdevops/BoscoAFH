using BoscoAFH.Common;
using System.ComponentModel.DataAnnotations;

namespace BoscoAFH.MasterInfrastructure.Models.Input
{
    /// <summary>
    /// Represents a data transfer object for user details.
    /// </summary>
    
    public class UserDetailResult
    {
        public int StatusCode { get; set; } = 200;
       
        public UserDetailDTO? UserDetails { get; set; }
    }


    /// <summary>
    /// Represents a data transfer object for user credentials.
    /// </summary>
    public class UserCredential
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required]
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        // [MinLength(1)]
        public string? Password { get; set; }
    }


}
