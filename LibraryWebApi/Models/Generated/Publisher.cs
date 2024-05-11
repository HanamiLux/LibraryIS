using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("publishers")]
public partial class Publisher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("publishername")]
    [StringLength(255)]
    public string Publishername { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    [Column("contactphone")]
    [StringLength(255)]
    public string Contactphone { get; set; } = null!;

    [InverseProperty("Publisher")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
