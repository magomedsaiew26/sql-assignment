
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

        public ICollection<Customer> GetAll()
        {
            ICollection<Customer> result = new List<Customer>();

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

            return result;
        }

        public ICollection<Customer> GetRange(int offset, int limit)
        {
            ICollection<Customer> result = new List<Customer>();

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

            return result;
        }

        public Customer Get(int id)
        {
            ICollection<Customer> result = new List<Customer>();

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
                return default(Customer);
            }
            return result.First();
        }

        public ICollection<Customer> Get(string name)
        {
            ICollection<Customer> result = new List<Customer>();

            SelectQuery($"SELECT * FROM Chinook.dbo.Customer WHERE FirstName LIKE '%{name}%' OR LastName LIKE '%{name}%';", (reader) =>
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

            return result;
        }

        public bool Add(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                //
                StringBuilder sqlString = new StringBuilder("INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email) VALUES(");
                //create command

                string p = "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}') ";
                string parameters = string.Format(p, customer.FirstName, customer.LastName, customer.Country, customer.PostalCode, customer.Phonenumber, customer.Email);
                sqlString.Append(parameters);

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString.ToString(); //set command

                    // Insert the customer into the database 
                    IAsyncResult asyncres = command.BeginExecuteNonQuery(null, null);
                    int result = command.EndExecuteNonQuery(asyncres);
                    // 1 rows should beaffected
                    return (result == 1);

                }
            }
        }

        public bool Update(Customer update)
        {
            // 6. Update an existing customer to the database
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                // build command

                StringBuilder sqlString = new StringBuilder("UPDATE Customer SET ");

                sqlString.Append($"FirstName = {update.FirstName}");
                sqlString.Append($"LastName = {update.LastName}");
                sqlString.Append($"Country = {update.Country}");
                sqlString.Append($"PostalCode = {update.PostalCode}");
                sqlString.Append($"Phone = {update.Phonenumber}");
                sqlString.Append($"Email = {update.Email}");

                sqlString.Append($"WHERE CustomerId = {update.Id}");


                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString.ToString(); //set command

                    // Insert the customer into the database 
                    IAsyncResult asyncres = command.BeginExecuteNonQuery(null, null);

                    int result = command.EndExecuteNonQuery(asyncres);
                    // 1 rows should be affected
                    return (result == 1);
                }
            }
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
                    }
                }
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

                return result;
            }


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
                $"WHERE i2.CustomerId = {customerId} " +
                "GROUP BY t2.GenreId " +
                ") AS genre_counts " +
                "); ";

            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) { 
                    result.firstname = reader.GetString(0);
                    result.lastname = reader.GetString(1);
                    result.genre = new List<string>();
                    result.genre.Add(reader.GetString(2));
                    result.count = reader.GetInt32(3);
                }
                
                while(reader.Read())
                {
                    result.genre.Add(reader.GetString(2));
                }

                reader.Close();
            }

            return result;
        }
    }
}
