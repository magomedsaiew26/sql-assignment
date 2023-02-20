
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //TODO: GetAll, Get, GetLimited, Delete, Update...

        private static string ConncectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "";
                builder.IntegratedSecurity = true;
                builder.TrustServerCertificate = true;
                return builder.ConnectionString;
            }
        }

        public IList<Customer> GetAll()
        {
            IList<Customer> result = new List<Customer>();

            //setup connection
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                string sqlString = "SELECT * FROM Customer";

                //create command
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlString; //set command

                    //reads the result of the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Customer
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Country = reader.GetString(3),
                                PostalCode = reader.GetString(4),
                                Phonenumber = reader.GetString(5),
                                Email = reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return result;
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
