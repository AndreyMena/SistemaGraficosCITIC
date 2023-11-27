using Microsoft.Data.SqlClient;
using SistemaGraficosCITIC.Models.Domain;
using System.Data;

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
      var command = new SqlCommand("SELECT * FROM Author WHERE AuthorName = @researcherId", connection);
      {
        command.Parameters.AddWithValue("@researcherId", authorName);
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

    public Researcher GetResearcherById(string researcherId)
    {
      connection.Open();
      var command = new SqlCommand("SELECT * FROM Researcher WHERE Id = @researcherId", connection);
      command.Parameters.AddWithValue("@researcherId", researcherId);

      Researcher researcher = null;

      using (var reader = command.ExecuteReader())
      {
        if (reader.Read())
        {
          researcher = new Researcher
          {
            Id = (Guid)reader["Id"],
            Name = reader["Name"].ToString(),
            LastName = reader["LastName"].ToString(),
            Type = reader["Type"].ToString(),
          };
        }
      }

      connection.Close();

      return researcher;
    }

    public List<PublicationType> GetPublicationTypes()
    {
      List<PublicationType> publiTypes = new List<PublicationType>();

      string request = "SELECT * FROM PublicationType;";

      DataTable tablaDeDesglose = CrearTablaConsulta(request);
      foreach (DataRow columna in tablaDeDesglose.Rows)
      {
        publiTypes.Add(new PublicationType
        {
          PublicationTypeId = Convert.ToInt32(columna["PublicationTypeId"]),
          PublicationTypeName = Convert.ToString(columna["PublicationTypeName"])
        });
      }
      return publiTypes;
    }
  }
}
