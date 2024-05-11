using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("roles")]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("rolename")]
    [StringLength(255)]
    public string Rolename { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
