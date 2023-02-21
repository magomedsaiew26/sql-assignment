
using DataAccess.Models;

public interface ICustomerRepository
{
    public IList<Customer> GetAll();
    public IList<Customer> GetRange(int offset, int limit);
    public Customer Get(int id);
    public Customer Get(string name);    
    public bool Add(Customer customer);
    public bool Update(int id, Customer updatedCustomer);
    public IList<String, int> CustomersByCountry();
    public IList<Customer> HighestSpenders(int n);
    public String MostPopularGenre(int id);




}

