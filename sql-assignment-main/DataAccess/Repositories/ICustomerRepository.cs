
using DataAccess.Models;

public interface ICustomerRepository
{
    public Task<IList<Customer>> GetAll();
    public Task<IList<Customer>> GetRange(int offset, int limit);
    public Task<Customer> Get(int id);
    // public Customer Get(string name);    
    public bool Add(Customer customer);
    public bool Update(int id, Customer updatedCustomer);
    public IList<CustomerCountry> CustomersByCountry();
    public IList<CustomerSpender> HighestSpenders(int n);
    public IList<CustomerGenre> MostPopularGenre(int id);

}

