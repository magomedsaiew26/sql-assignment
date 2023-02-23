using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public record struct CustomerSpender(int customerId, string firstname, string lastname, double amount);
}
