using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("genres")]
public partial class Genre
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("genrename")]
    [StringLength(255)]
    public string Genrename { get; set; } = null!;

    [InverseProperty("Genre")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
