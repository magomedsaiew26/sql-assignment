
namespace DataAccess.Models
{
    public record struct Customer(int Id, string FirstName, string LastName, 
        string Country, string PostalCode, 
        string Phone, string Email);
}
