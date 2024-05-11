using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("bookrentals")]
public partial class Bookrental
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("instanceid")]
    public int Instanceid { get; set; }

    [Column("daterented")]
    public DateOnly Daterented { get; set; }

    [Column("datereturned")]
    public DateOnly Datereturned { get; set; }

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    [ForeignKey("Instanceid")]
    [InverseProperty("Bookrentals")]
    public virtual Bookinstance? Instance { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Bookrentals")]
    public virtual User? User { get; set; }
}
