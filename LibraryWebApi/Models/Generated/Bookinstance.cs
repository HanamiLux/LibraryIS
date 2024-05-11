using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("bookinstances")]
public partial class Bookinstance
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("bookid")]
    public int Bookid { get; set; }

    [Column("conditionid")]
    public int Conditionid { get; set; }

    [Column("acquireddate")]
    public DateOnly Acquireddate { get; set; }

    [Column("isavailable")]
    public bool? Isavailable { get; set; }

    [ForeignKey("Bookid")]
    [InverseProperty("Bookinstances")]
    public virtual Book? Book { get; set; }

    [InverseProperty("Instance")]
    public virtual ICollection<Bookrental> Bookrentals { get; set; } = new List<Bookrental>();

    [ForeignKey("Conditionid")]
    [InverseProperty("Bookinstances")]
    public virtual Condition? Condition { get; set; }
}
