using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("books")]
public partial class Book
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("authorid")]
    public int Authorid { get; set; }

    [Column("genreid")]
    public int Genreid { get; set; }

    [Column("isbn")]
    [StringLength(255)]
    public string Isbn { get; set; } = null!;

    [Column("yearpublished")]
    public short Yearpublished { get; set; }

    [Column("availablecopies")]
    public int Availablecopies { get; set; }

    [Column("publisherid")]
    public int Publisherid { get; set; }

    [Column("isavailable")]
    public bool? Isavailable { get; set; }

    [ForeignKey("Authorid")]
    [InverseProperty("Books")]
    public virtual Author? Author { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<Bestbook> Bestbooks { get; set; } = new List<Bestbook>();

    [InverseProperty("Book")]
    public virtual ICollection<Bookinstance> Bookinstances { get; set; } = new List<Bookinstance>();

    [InverseProperty("Book")]
    public virtual ICollection<Bookreview> Bookreviews { get; set; } = new List<Bookreview>();

    [ForeignKey("Genreid")]
    [InverseProperty("Books")]
    public virtual Genre? Genre { get; set; }

    [ForeignKey("Publisherid")]
    [InverseProperty("Books")]
    public virtual Publisher? Publisher { get; set; }
}
