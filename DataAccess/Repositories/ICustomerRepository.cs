
using DataAccess.Models;

public interface ICustomerRepository
{
    public Task<IList<Customer>> GetAll();
    public IList<Customer> GetRange(int offset, int limit);
    public Customer Get(int id);
    public Customer Get(string name);    
    public bool Add(Customer customer);
    public bool Update(int id, Customer updatedCustomer);

}