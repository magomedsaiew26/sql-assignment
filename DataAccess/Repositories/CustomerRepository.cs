
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Numerics;
using System.Text;

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
            // 5. Add a new customer to the database
            //setup connection
            using (SqlConnection connection = new SqlConnection(ConncectionString))
            {
                connection.Open();

                StringBuilder sqlStringql = new StringBuilder("INSERT INTO Customer(FirstName, LastName, Company, Address, City, State, Country, ");


                sqlString.Append("Postal Code, Phine, Fax, Email, SupportRepId) VALUES(";
                //create command
                sqlString.Append(customer.FirstName);
                customer.Append(", ");
                sqlString.Append(customer.LastName);
                customer.Append(", ");
                sqlString.Append(customer.Country);
                customer.Append(", "););
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

            throw new NotImplementedException();
        }
        public IList<Customer> HighestSpenders(int n)
        {
            // 8. Return a list of the n highest spenders 

            throw new NotImplementedException();
        }
        public String MostPopularGenre(int id)
        {
            // 9. For a given customer, return the most pupo\ular genre (genre with most tracks) 

            throw new NotImplementedException();
        }
    }
}
