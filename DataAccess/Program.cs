

using DataAccess.Repositories;

async void dod()
{
    CustomerRepository repo = new CustomerRepository();
    var all = repo.MostPopularGenre(12);

    Console.WriteLine(all);
    foreach(var genre in all.genre)
        Console.WriteLine(genre);
}

dod();
