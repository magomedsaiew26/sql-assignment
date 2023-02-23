
using DataAccess.Repositories;

Console.WriteLine("Appendix B");

var repo = new CustomerRepository();
var all = repo.GetAll();

foreach (var item in all)
    Console.WriteLine(item);
