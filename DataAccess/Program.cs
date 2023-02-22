

using DataAccess.Repositories;

async void dod()
{
    CustomerRepository repo = new CustomerRepository();
    var all = repo.GetAll();

    foreach (var item in all)
        Console.WriteLine(item);
}

dod();
