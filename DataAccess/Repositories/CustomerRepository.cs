
using DataAccess.Models;
using Microsoft.Data.SqlClient;

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

        public Task<IList<Customer>> GetAll()
        {
            IList<Customer> result = new List<Customer>();

            //setup connection
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();
                string sqlString = "SELECT * FROM Chinook.dbo.Customer;";
                //create command
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString; //set command
                    

                    //reads the result of the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Country = reader.GetString(7)
                            };
                            if(!reader.IsDBNull(8))
                                customer.PostalCode = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                customer.Phonenumber = reader.GetString(9);
                            if (!reader.IsDBNull(11))
                                customer.Email = reader.GetString(11);

                            result.Add(customer);
                        }
                    }
                }
            }

            var tsk = Task.FromResult(result);

            return tsk;
        }

        public IList<Customer> GetRange(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Get(string name)
        {
            throw new NotImplementedException();
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
