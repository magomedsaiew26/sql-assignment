using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //TODO: GetAll, Get, GetLimited, Delete, Update...

        private static string ServerName => "N-NO-01-01-1467\\SQLEXPRESS02";
        private static string DatabaseName = "Chinook";
        private static string ConncectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = ServerName;
                builder.InitialCatalog = DatabaseName;
                builder.IntegratedSecurity = true;
                builder.TrustServerCertificate = true;
                return builder.ConnectionString;
            }
        }

        private void SelectQuery(string query, System.Predicate<SqlDataReader> predicate)
        {
            //setup connection
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                //create command
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query; //set command

                    //reads the result of the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        predicate?.Invoke(reader);
                    }
                }
            }
        }

        public Task<IList<Customer>> GetAll()
        {
            IList<Customer> result = new List<Customer>();

            SelectQuery("SELECT * FROM Chinook.dbo.Customer;", (reader) =>
            {
                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2)
                    };

                    if (!reader.IsDBNull(7))
                        customer.Country = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        customer.PostalCode = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        customer.Phonenumber = reader.GetString(9);
                    if (!reader.IsDBNull(11))
                        customer.Email = reader.GetString(11);

                    result.Add(customer);
                }

                return true;
            });

            return Task.FromResult(result);
        }

        public Task<IList<Customer>> GetRange(int offset, int limit)
        {
            IList<Customer> result = new List<Customer>();

            SelectQuery($"SELECT * FROM Chinook.dbo.Customer ORDER BY CustomerId OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;", (reader) =>
            {
                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2)
                    };

                    if (!reader.IsDBNull(7))
                        customer.Country = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        customer.PostalCode = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        customer.Phonenumber = reader.GetString(9);
                    if (!reader.IsDBNull(11))
                        customer.Email = reader.GetString(11);

                    result.Add(customer);
                }

                return true;
            });

            return Task.FromResult(result);
        }

        public Task<Customer> Get(int id)
        {
            IList<Customer> result = new List<Customer>();

            SelectQuery($"SELECT * FROM Chinook.dbo.Customer WHERE CustomerId = {id};", (reader) =>
            {
                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2)
                    };

                    if (!reader.IsDBNull(7))
                        customer.Country = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        customer.PostalCode = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        customer.Phonenumber = reader.GetString(9);
                    if (!reader.IsDBNull(11))
                        customer.Email = reader.GetString(11);

                    result.Add(customer);
                }

                return true;
            });

            if (result.Count > 1)
                Console.WriteLine("Recieved multiple results from a single id");
            if (result.Count == 0)
            {
                Console.WriteLine("No results!");
                return Task.FromResult(default(Customer));
            }
            return Task.FromResult(result[0]);
        }

        // 5. Add a new customer to the database
        //setup connection
        using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                //
                StringBuilder sqlString = new StringBuilder("INSERT INTO Customer(FirstName, LastName, Company, Address, City, State, Country, ");


                sqlString.Append("Postal Code, Phine, Fax, Email, SupportRepId) VALUES(";
                //create command
                sqlString.Append(customer.FirstName);
                customer.Append(", ");
                sqlString.Append(customer.LastName);
                customer.Append(", ");
                sqlString.Append(customer.Country);
                customer.Append(", ");
                sqlString.Append(customer.PostalCode);
                customer.Append(", ");
                sqlString.Append(customer.Phone);
                customer.Append(", ");
                sqlString.Append(customer.Email);
                customer.Append(")");


                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sbSQL.ToString(); //set command

                    // Insert the customer into the database 
                    IAsyncResult asyncres = command.BeginExecuteNonQuery(null, null);
                }
            }

            // throw new NotImplementedException();
        }

        public bool Update(int id, Customer updatedCustomer)
        {
            String update;
            // 6. Update an existing customer to the database
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                // build command

                StringBuilder sqlString = new StringBuilder("UPDATE Customer SET ");

                sqlString.Append("FirstName = ");
                update = "'" + updatedCustomer.FirstName + "'";
                sqlString.Append(update);

                sqlString.Append("LastName = ");
                update = "'" + updatedCustomer.LastName + "'";
                sqlString.Append(update);

                sqlString.Append("Country = ");
                update = "'" + updatedCustomer.Country + "'";
                sqlString.Append(update);

                sqlString.Append("PostalCode = ");
                update = "'" + updatedCustomer.PostalCode + "'";
                sqlString.Append(update);

                sqlString.Append("Phone = ");
                update = "'" + updatedCustomer.Phonenumber + "'";
                sqlString.Append(update);

                sqlString.Append("Email = ");
                update = "'" + updatedCustomer.Email + "'";
                sqlString.Append(update);

                sqlString.Append("WHERE CustomerId = ");
                sqlString.Append(updatedCustomer.Id.ToString());


                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString.ToString(); //set command

                    // Insert the customer into the database 
                    IAsyncResult asyncres = command.BeginExecuteNonQuery(null, null);
                }

 //               throw new NotImplementedException();
        }


        public IList<String, int> CustomersByCountry()
        {
                // 7. Return the number of customers in each country, ordered high to low
                IList<CountryNumber> result = new List<CountryNumber>();

                //setup connection
                using (SqlConnection connection = new SqlConnection(ConncectionString))
                {
                    connection.Open();

                    string sqlString = "SELECT Country, count(*) FROM Customer GROUP BY Country";

                    //create command
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sqlString; //set command

                        //reads the result of the command
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new CountryNumber
                                {
                                    country = reader.GetString(0),
                                    count = Convert.ToInt64(reader.GetString(1))
                                });
                            }
                        }
                    }
                }

                return result;

                // throw new NotImplementedException();
            }
            public IList<Customer> HighestSpenders(int n)
        {
                // 8. Return a list of the n highest spenders 
                    string sqlString = = "SELECT c.CustomerId, c.FirstName, c.LastName, SUM(i.Total) as TotalSpent " +
                             "FROM Customer c " +
                             "INNER JOIN Invoice i ON c.CustomerId = i.CustomerId " +
                             "GROUP BY c.CustomerId, c.FirstName, c.LastName " +
                             "ORDER BY TotalSpent DESC";
                //Setup connection

                using (SqlConnection connection = new SqlConnection(ConncectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int customerId = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        decimal totalSpent = reader.GetDecimal(3);

                        Console.WriteLine("{0}: {1} {2} - {3}", customerId, firstName, lastName, totalSpent);
                    }

                    reader.Close();
                }
            }
        }


                    throw new NotImplementedException();
        }
        public String MostPopularGenre(int id)
        {
            // 9. For a given customer, return the most pupo\ular genre (genre with most tracks) 

            throw new NotImplementedException();
        }
    }
}
