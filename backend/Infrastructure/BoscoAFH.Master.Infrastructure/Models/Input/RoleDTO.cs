using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Models.Input
{
    public class RoleDTO
    {
        /// <summary>
        /// Unique identifier for the role
        /// </summary>

        /// <summary>
        /// Integer identifier for the role
        /// </summary>
        public long RoleId { get; set; }


        /// <summary>
        /// Name of the role
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string RoleName { get; set; }

        /// <summary>
        /// description of the role
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Status: active or not (TRUE = active, FALSE = inactive).
        /// </summary>
        public bool IsActive { get; set; }

        
        /// <summary>
        /// Status: deleted or not (TRUE = deleted, FALSE = not deleted).
        /// </summary>
        public bool IsDeleted { get; set; } = false;

         
    }

}
