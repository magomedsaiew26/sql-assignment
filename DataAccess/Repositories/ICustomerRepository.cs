
using DataAccess.Models;

public interface ICustomerRepository
{
    public Task<IList<Customer>> GetAll();
    public Task<IList<Customer>> GetRange(int offset, int limit);
    public Task<Customer> Get(int id);    
    public Task<IList<Customer>> Get(string name);    
    public bool Add(Customer customer);
    public bool Update(int id, Customer updatedCustomer);

}