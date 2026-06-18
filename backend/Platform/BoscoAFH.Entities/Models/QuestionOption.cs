using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoscoAFH.Entities.Models;

[Table("question_options")]
public partial class QuestionOption
{
    [Key]
    [Column("option_id")]
    public int OptionId { get; set; }

    [Column("question_id")]
    public int QuestionId { get; set; }

    [Column("option_text")]
    [StringLength(500)]
    public string? OptionText { get; set; }

    [Column("is_correct")]
    public bool? IsCorrect { get; set; }

    [Column("created_on", TypeName = "timestamp without time zone")]
    public DateTime? CreatedOn { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("QuestionOptions")]
    public virtual QuestionBank Question { get; set; } = null!;
}
