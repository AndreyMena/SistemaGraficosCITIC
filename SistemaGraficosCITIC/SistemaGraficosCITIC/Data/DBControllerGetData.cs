using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SistemaGraficosCITIC.Models.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SistemaGraficosCITIC.Data
{
    public class DBControllerGetData : DBControllerBase
    {

        public DBControllerGetData() { }

        public bool myValueExist(string myTable, string myField, string myValue)
        {
            connection.Open();

            using (var cmd = new SqlCommand("select COUNT(*) from " + myTable + " where " + myField + "=@myValue", connection))
            {
                cmd.Parameters.AddWithValue("@myValue", myValue);

                var result = cmd.ExecuteScalar();
                connection.Close();

                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }


        public Author GetAuthorByName(string authorName)
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Author WHERE AuthorName = @authorName", connection);
            {
                command.Parameters.AddWithValue("@authorName", authorName);
                using (var reader = command.ExecuteReader())
                {
                    connection.Close();
                    if (reader.Read())
                    {
                        return new Author
                        {
                            AuthorId = (Guid)reader["AuthorId"],
                            AuthorName = reader["AuthorName"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            
        }
    }
}
