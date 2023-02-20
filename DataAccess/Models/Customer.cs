using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public record Customer(int Id, string FirstName, string LastName, 
        string Country, string PostalCode, 
        string Phonenumber, string Email);
}
