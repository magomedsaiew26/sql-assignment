
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

                    if(!reader.IsDBNull(7))
                        customer.Country = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        customer.PostalCode= reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        customer.Phonenumber= reader.GetString(9);
                    if (!reader.IsDBNull(11))
                        customer.Email= reader.GetString(11);

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
            throw new NotImplementedException();
        }        

        public bool Update(int id, Customer updatedCustomer)
        {
            throw new NotImplementedException();
        }
    }
}
