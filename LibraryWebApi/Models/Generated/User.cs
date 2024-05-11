using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("login")]
    [StringLength(255)]
    public string Login { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;
  
    [Column("roleid")]
    public int Roleid { get; set; }

    [Column("isavailable")]
    public bool? Isavailable { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Bookrental> Bookrentals { get; set; } = new List<Bookrental>();

    [InverseProperty("User")]
    public virtual ICollection<Bookreview> Bookreviews { get; set; } = new List<Bookreview>();

    [InverseProperty("User")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [ForeignKey("Roleid")]
    [InverseProperty("Users")]
    public virtual Role? Role { get; set; }
}
