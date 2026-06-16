using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Models;

[Table("module_rights")]
[Index("FeatureId", Name = "idx_module_rights_feature_id")]
[Index("RoleId", Name = "idx_module_rights_role_id")]
[Index("UserId", Name = "idx_module_rights_user_id")]
public partial class ModuleRight
{
    [Key]
    [Column("rights_id")]
    public long RightsId { get; set; }

    [Column("feature_id")]
    public long FeatureId { get; set; }

    [Column("role_id")]
    public long? RoleId { get; set; }

    [Column("user_id")]
    public long? UserId { get; set; }

    [Column("access_right")]
    [StringLength(50)]
    public string AccessRight { get; set; } = null!;

    [Column("main_module_id")]
    public long? MainModuleId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("FeatureId")]
    [InverseProperty("ModuleRights")]
    public virtual ModulesAndFeature Feature { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("ModuleRights")]
    public virtual Role? Role { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ModuleRights")]
    public virtual User? User { get; set; }
}
