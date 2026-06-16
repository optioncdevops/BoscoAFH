using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Models;

[Table("departments")]
[Index("DepartmentName", Name = "departments_department_name_key", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Column("department_name")]
    [StringLength(100)]
    public string DepartmentName { get; set; } = null!;

    [Column("created_on", TypeName = "timestamp without time zone")]
    public DateTime? CreatedOn { get; set; }
}
