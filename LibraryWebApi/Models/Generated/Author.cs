using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("authors")]
public partial class Author
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("firstname")]
    [StringLength(255)]
    public string Firstname { get; set; } = null!;

    [Column("lastname")]
    [StringLength(255)]
    public string Lastname { get; set; } = null!;

    [Column("birthdate")]
    public DateOnly Birthdate { get; set; }

    [Column("country")]
    [StringLength(255)]
    public string Country { get; set; } = null!;

    [InverseProperty("Author")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
