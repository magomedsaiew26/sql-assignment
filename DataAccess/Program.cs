﻿

using DataAccess.Repositories;

async void dod()
{
    CustomerRepository repo = new CustomerRepository();
    var all = await repo.GetAll();

    foreach (var item in all)
        Console.WriteLine(item);
}

dod();
