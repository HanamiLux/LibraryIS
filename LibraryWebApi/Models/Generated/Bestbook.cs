using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("bestbooks")]
public partial class Bestbook
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("bookid")]
    public int Bookid { get; set; }

    [Column("ranking")]
    public int Ranking { get; set; }

    [Column("year")]
    public short Year { get; set; }

    [ForeignKey("Bookid")]
    [InverseProperty("Bestbooks")]
    public virtual Book? Book { get; set; }
}
