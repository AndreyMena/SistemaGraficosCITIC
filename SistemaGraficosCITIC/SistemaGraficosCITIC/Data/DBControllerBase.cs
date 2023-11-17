using Microsoft.Data.SqlClient;
using System.Data;

namespace SistemaGraficosCITIC.Data
{
    public abstract class DBControllerBase
    {
        protected SqlConnection connection;
        protected string connectionRoute;
        public DBControllerBase()
        {
            var builder = WebApplication.CreateBuilder();
            connectionRoute =
            builder.Configuration.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionRoute);
        }

        //método para llenar una tabla a partir de la información obtenida de una consulta a la base de datos
        protected DataTable CrearTablaConsulta(string consulta)
        {

            SqlCommand comandoParaConsulta = new(consulta,
            connection);
            SqlDataAdapter adaptadorParaTabla = new(comandoParaConsulta);
            DataTable consultaFormatoTabla = new();
            connection.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            connection.Close();
            return consultaFormatoTabla;
        }

        public void ReiniciarConexion()
        {
            var builder = WebApplication.CreateBuilder();
            connectionRoute = builder.Configuration.GetConnectionString("ContextoBaseDeDatosProyectoToDoList");
            connection = new SqlConnection(connectionRoute);
        }
    }
}
