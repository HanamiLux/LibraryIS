using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("conditions")]
public partial class Condition
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [InverseProperty("Condition")]
    public virtual ICollection<Bookinstance> Bookinstances { get; set; } = new List<Bookinstance>();
}
