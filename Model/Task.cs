using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.Model;

public partial class Task
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTask { get; set;}

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly Term { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
