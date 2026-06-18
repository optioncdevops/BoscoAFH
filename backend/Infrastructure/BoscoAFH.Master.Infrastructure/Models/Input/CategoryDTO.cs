using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Models.Input
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; } = null!;

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }


    }

}
