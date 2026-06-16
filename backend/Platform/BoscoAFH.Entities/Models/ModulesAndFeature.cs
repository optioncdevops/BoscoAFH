using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Models;

[Table("modules_and_features")]
[Index("ParentId", Name = "idx_modules_parent_id")]
public partial class ModulesAndFeature
{
    [Key]
    [Column("module_id")]
    public long ModuleId { get; set; }

    [Column("i_module_id")]
    public long? IModuleId { get; set; }

    [Column("module")]
    [StringLength(150)]
    public string Module { get; set; } = null!;

    [Column("parent_id")]
    public long? ParentId { get; set; }

    [Column("submodule")]
    [StringLength(150)]
    public string? Submodule { get; set; }

    [Column("menu_code")]
    [StringLength(100)]
    public string? MenuCode { get; set; }

    [Column("routing_url")]
    [StringLength(250)]
    public string? RoutingUrl { get; set; }

    [Column("display_order")]
    public int DisplayOrder { get; set; }

    [Column("is_have_submodule")]
    public bool IsHaveSubmodule { get; set; }

    [Column("item_description")]
    [StringLength(500)]
    public string? ItemDescription { get; set; }

    [Column("access_level")]
    [StringLength(50)]
    public string? AccessLevel { get; set; }

    [Column("is_show_in_rights")]
    public bool IsShowInRights { get; set; }

    [Column("activity")]
    [StringLength(100)]
    public string? Activity { get; set; }

    [Column("is_landingurl")]
    public bool IsLandingurl { get; set; }

    [Column("is_landing_card")]
    public bool IsLandingCard { get; set; }

    [Column("icon")]
    [StringLength(100)]
    public string? Icon { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("display_name")]
    [StringLength(150)]
    public string? DisplayName { get; set; }

    [Column("main_module_id")]
    public long? MainModuleId { get; set; }

    [Column("submodule2")]
    [StringLength(150)]
    public string? Submodule2 { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<ModulesAndFeature> InverseParent { get; set; } = new List<ModulesAndFeature>();

    [InverseProperty("Feature")]
    public virtual ICollection<ModuleRight> ModuleRights { get; set; } = new List<ModuleRight>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual ModulesAndFeature? Parent { get; set; }
}
