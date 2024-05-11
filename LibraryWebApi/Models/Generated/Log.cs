using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Models;

[Table("logs")]
public partial class Log
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("useraction")]
    [StringLength(255)]
    public string Useraction { get; set; } = null!;

    [Column("logdatetime", TypeName = "timestamp without time zone")]
    public DateTime? Logdatetime { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Logs")]
    public virtual User? User { get; set; }
}
