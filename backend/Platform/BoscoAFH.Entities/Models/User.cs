using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Models;

[Table("users")]
[Index("Email", Name = "uq_users_email", IsUnique = true)]
[Index("UserName", Name = "uq_users_user_name", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("user_name")]
    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [Column("full_name")]
    [StringLength(150)]
    public string FullName { get; set; } = null!;

    [Column("email")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("mobile_no")]
    [StringLength(20)]
    public string? MobileNo { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("role_id")]
    public long? RoleId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ModuleRight> ModuleRights { get; set; } = new List<ModuleRight>();
}
