
using DataAccess.Models;

public interface ICustomerRepository
{
    public ICollection<Customer> GetAll();
    public ICollection<Customer> GetRange(int offset, int limit);
    public Customer Get(int id);    
    public ICollection<Customer> Get(string name);    
    public bool Add(Customer customer);
    public bool Update(Customer update);

    public ICollection<CustomerCountry> GetCustomerCountries();
    public ICollection<CustomerSpender> GetHighestSpenders();
    public CustomerGenre MostPopularGenre(int customerId);
}