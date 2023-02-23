using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public record struct CustomerGenre(string firstname, string lastname, ICollection<string> genre, int count);
}
