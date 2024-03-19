using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRelationships.Models;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;
}
