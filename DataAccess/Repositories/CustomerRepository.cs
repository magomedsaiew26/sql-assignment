
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.RegularExpressions;

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

        public ICollection<Customer> GetAll()
        {
            ICollection<Customer> result = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Chinook.dbo.Customer;";

                    using (SqlDataReader reader = command.ExecuteReader())
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
                                customer.Phone = reader.GetString(9);
                            if (!reader.IsDBNull(11))
                                customer.Email = reader.GetString(11);

                            result.Add(customer);
                        }

                        reader.Close();
                    }
                }

                connection.Close();
            }

            return result;
        }

        public ICollection<Customer> GetRange(int offset, int limit)
        {
            ICollection<Customer> result = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                string sqlString = "SELECT * FROM Chinook.dbo.Customer ORDER BY CustomerId OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;";

                //create command
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString;
                    command.Parameters.AddWithValue("@Offset", offset);
                    command.Parameters.AddWithValue("@Limit", limit);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
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
                                customer.Phone = reader.GetString(9);
                            if (!reader.IsDBNull(11))
                                customer.Email = reader.GetString(11);

                            result.Add(customer);
                        }

                        reader.Close();
                    }
                }

                connection.Close();
            }

            return result;
        }

        public Customer Get(int id)
        {
            ICollection<Customer> result = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                string sqlString = "SELECT * FROM Chinook.dbo.Customer WHERE CustomerId = @CustomerId;";

                //create command
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString;
                    command.Parameters.AddWithValue("@CustomerId", id);

                    using(SqlDataReader reader = command.ExecuteReader()) {
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
                                customer.Phone = reader.GetString(9);
                            if (!reader.IsDBNull(11))
                                customer.Email = reader.GetString(11);

                            result.Add(customer);
                        }

                        reader.Close();
                    }
                }

                connection.Close();
            }

            return result.First();
        }

        public ICollection<Customer> Get(string name)
        {
            ICollection<Customer> result = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                string sqlString = "SELECT * FROM Chinook.dbo.Customer WHERE FirstName LIKE @FirstName OR LastName LIKE @LastName;";

                //create command
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString; //set command
                    command.Parameters.AddWithValue("@FirstName", "%" + name + "%");
                    command.Parameters.AddWithValue("@LastName", "%" + name + "%");

                    //reads the result of the command
                    using (SqlDataReader reader = command.ExecuteReader())
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
                                customer.Phone = reader.GetString(9);
                            if (!reader.IsDBNull(11))
                                customer.Email = reader.GetString(11);

                            result.Add(customer);
                        }

                        reader.Close();
                    }
                }

                connection.Close();
            }

            return result;
        }

        public bool Add(Customer customer)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                //
                string sqlString = "INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email) " +
                    "VALUES(@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email);";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString.ToString(); //set command

                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Country", customer.Country);
                    command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Email", customer.Email);

                    Console.WriteLine(command.CommandText);

                    result = command.ExecuteNonQuery() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public bool Update(Customer update)
        {
            bool success = false;

            // 6. Update an existing customer to the database
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                // build command

                string sqlString = "UPDATE Customer " +
                    "SET FirstName = @FirstName, LastName = @LastName, Country = @Country, PostalCode = @PostalCode, Phone = @Phone, Email = @Email " +
                    "WHERE CustomerId = @CustomerId;";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString.ToString(); //set command

                    command.Parameters.AddWithValue("@CustomerId", update.Id);
                    command.Parameters.AddWithValue("@FirstName", update.FirstName);
                    command.Parameters.AddWithValue("@LastName", update.LastName);
                    command.Parameters.AddWithValue("@Country", update.Country);
                    command.Parameters.AddWithValue("@PostalCode", update.PostalCode);
                    command.Parameters.AddWithValue("@Phone", update.Phone);
                    command.Parameters.AddWithValue("@EMail", update.Email);

                    success = (command.ExecuteNonQuery() == 1);
                }

                connection.Close();
            }

            return success;
        }

        public ICollection<CustomerCountry> GetCustomerCountries()
        {
            // 7. Return the number of customers in each country, ordered high to low
            ICollection<CustomerCountry> result = new List<CustomerCountry>();

            //setup connection
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                string sqlString = $"SELECT Country, count(*) FROM Customer GROUP BY Country ORDER BY count(*) DESC";

                //create command
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString; //set command

                    //reads the result of the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new CustomerCountry
                            {
                                country = reader.GetString(0),
                                count = reader.GetInt32(1)
                            });
                        }

                        reader.Close();
                    }
                }

                connection.Close();
            }

            return result;
        }

        public ICollection<CustomerSpender> GetHighestSpenders()
        {
            IList<CustomerSpender> result = new List<CustomerSpender>();
            // 8. Return a list of the n highest spenders 
            string sqlString = "SELECT c.CustomerId, c.FirstName, c.LastName, SUM(i.Total) AS TotalSpend " +
                        "FROM Customer c " +
                        "INNER JOIN Invoice i ON c.CustomerId = i.CustomerId " +
                        "GROUP BY c.CustomerId, c.FirstName, c.LastName, i.Total " +
                        "ORDER BY i.Total DESC;";

            //Setup connection

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new CustomerSpender{
                            customerId = reader.GetInt32(0), 
                            firstname = reader.GetString(1), 
                            lastname = reader.GetString(2), 
                            amount = (double)reader.GetDecimal(3) 
                        });
                    }

                    reader.Close();
                }

                connection.Close();
            }

            return result;
        }

        public CustomerGenre MostPopularGenre(int customerId)
        {
            // 9. For a given customer, return the most popular genre (genre with most tracks) 
            CustomerGenre result = new CustomerGenre();

            string sqlQuery = "SELECT c.FirstName, c.LastName, g.Name AS PopularGenre, COUNT(*) AS TrackCount " +
                "FROM Customer c\r\nJOIN Invoice i ON c.CustomerId = i.CustomerId " +
                "JOIN InvoiceLine il ON i.InvoiceId = il.InvoiceId " +
                "JOIN Track t ON il.TrackId = t.TrackId " +
                "JOIN Genre g ON t.GenreId = g.GenreId\r\nWHERE c.CustomerId = 12 " +
                "GROUP BY c.FirstName, c.LastName, g.Name " +
                "HAVING COUNT(*) = ( " +
                "SELECT MAX(cnt) " +
                "FROM ( " +
                "SELECT COUNT(*) AS cnt " +
                "FROM Invoice i2 " +
                "JOIN InvoiceLine il2 ON i2.InvoiceId = il2.InvoiceId " +
                "JOIN Track t2 ON il2.TrackId = t2.TrackId " +
                $"WHERE i2.CustomerId = @CustomerId " +
                "GROUP BY t2.GenreId " +
                ") AS genre_counts " +
                "); ";

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.firstname = reader.GetString(0);
                            result.lastname = reader.GetString(1);
                            result.genre = new List<string>();
                            result.genre.Add(reader.GetString(2));
                            result.count = reader.GetInt32(3);
                        }

                        while (reader.Read())
                        {
                            result.genre.Add(reader.GetString(2));
                        }

                        reader.Close();
                    }
                }

                connection.Close();
            }

            return result;
        }
    }
}
