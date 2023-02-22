
namespace DataAccess.Models
{
    public record struct Customer(int Id, string FirstName, string LastName, 
        string Country, string PostalCode, 
        string Phonenumber, string Email);

    public record struct CustomerCountry(string country, int count);
 
    public record struct CustomerSpender(Customer customer, Decimal amount);

    public record struct CustomerGenre(Customer customer, string genre);

}
