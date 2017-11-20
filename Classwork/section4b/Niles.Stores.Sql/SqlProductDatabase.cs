using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nile.Stores.Sql
{
    /// <summary>
    /// Provides an implemen tation of IProductDatabase using SQL Sever
    /// </summary>
    public class SqlProductDatabase : ProductDatabase
    {
        #region Construction
        public SqlProductDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;

        #endregion

        protected override Product AddCore( Product product )
        {
            var id = 0;
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("AddProduct", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = product.Name; //alternative way
                cmd.Parameters.AddWithValue("@description", product.Description); //yet another alternative, preferred approach
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@isDiscontinued", product.IsDiscontinued);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetCore(id);
        }

        protected override IEnumerable<Product> GetAllCore()
        {
            var products = new List<Product>();
            using (var connection = OpenDatabase())
            {
                var cmd = new SqlCommand("GetAllProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        //reader.GetName(0); //return the name of the columns
                        //reader.GetFieldType(1);
                        //Convert.ToInt32(reader["Id"]);

                        var product = new Product() {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),  
                            Name = reader.GetFieldValue<string>(1),
                            Price = reader.GetDecimal(2),
                            Description = reader.GetString(3),
                            IsDiscontinued = reader.GetBoolean(4)
                        };
                        products.Add(product);
                    }
                };
                    return products;
            };
        }

        protected override Product GetCore( int id )
        {
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("GetProduct", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@id", id);

                //using a dataset instead of a reader
                var ds = new DataSet();
                var da = new SqlDataAdapter() {
                    SelectCommand = cmd
                };

                da.Fill(ds);

                var tables = ds.Tables.OfType<DataTable>().FirstOrDefault();
                if(tables != null)
                {
                    var row = tables.AsEnumerable().FirstOrDefault();
                    if(row != null)
                    {
                        return new Product() {
                            Id = Convert.ToInt32(row["id"]),
                            Name = row.Field<string>("Name"),
                            Description = row.Field<string>("Description"),
                            Price = row.Field<decimal>("Price"),
                            IsDiscontinued = row.Field<bool>("IsDiscontinued")
                        };
                        
                    }
                }
            }

            return null;
        }

        protected override void RemoveCore( int id )
        {
            using (var conn = OpenDatabase())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "RemoveProduct";
                cmd.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter("@id", id);
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
            }
             
        }

        protected override Product UpdateCore( Product existing, Product product )
        {
            var id = 0;
            using (var conn = OpenDatabase())
            {
                var cmd = new SqlCommand("UpdateProduct", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@id", existing.Id);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@description", product.Description); //yet another alternative, preferred approach
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@isDiscontinued", product.IsDiscontinued);

                cmd.ExecuteNonQuery();
            }

            return GetCore(existing.Id);
        }

        private SqlConnection OpenDatabase()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open(); //explicitly creates a connection to the database
            return connection;
        }
    }
}
