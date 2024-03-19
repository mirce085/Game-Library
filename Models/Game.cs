using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EFRelationships.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public string Developer { get; set; } = null!;
    public string Publisher { get; set; } = null!;
    public int Rating { get; set; }
    public string Category { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = null!;
}
