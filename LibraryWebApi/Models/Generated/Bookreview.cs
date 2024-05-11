using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("bookreviews")]
public partial class Bookreview
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("bookid")]
    public int Bookid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("reviewcontent")]
    public string Reviewcontent { get; set; } = null!;

    [Column("rate")]
    public short Rate { get; set; }

    [Column("reviewdate", TypeName = "timestamp without time zone")]
    public DateTime? Reviewdate { get; set; }

    [ForeignKey("Bookid")]
    [InverseProperty("Bookreviews")]
    public virtual Book? Book { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Bookreviews")]
    public virtual User? User { get; set; }
}
