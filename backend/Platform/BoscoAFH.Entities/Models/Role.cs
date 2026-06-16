using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Models;

[Table("roles")]
public partial class Role
{
    [Key]
    [Column("role_id")]
    public long RoleId { get; set; }

    [Column("role_code")]
    [StringLength(50)]
    public string RoleCode { get; set; } = null!;

    [Column("role_name")]
    [StringLength(100)]
    public string RoleName { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<ModuleRight> ModuleRights { get; set; } = new List<ModuleRight>();
}
