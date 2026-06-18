using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Models;

[Table("question_bank")]
public partial class QuestionBank
{
    [Key]
    [Column("question_id")]
    public int QuestionId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("question_text")]
    public string QuestionText { get; set; } = null!;

    [Column("question_type")]
    [StringLength(30)]
    public string QuestionType { get; set; } = null!;

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("created_on", TypeName = "timestamp without time zone")]
    public DateTime? CreatedOn { get; set; }

    [Column("modified_by")]
    public int? ModifiedBy { get; set; }

    [Column("modified_on", TypeName = "timestamp without time zone")]
    public DateTime? ModifiedOn { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("QuestionBanks")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Question")]
    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
}
