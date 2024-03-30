using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ContentTeil.DomainModel.Model;

public class Content
{
    public int Id { get; init; }
    public Guid guid { get; init; } = Guid.NewGuid();
    public string text { get; set; }
    
    public Content()
    {
    }

    public Content(int id, Guid guid,  string text)
    {
        if (id < 0)
        {
            throw new ArgumentException("ID cannot be negative", nameof(id));
        }

        Id = id;
        this.guid = guid;
        this.text = text;
    }
}