using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuracion;
        private readonly SqlConnection _conexionBD;

        public RepositorioDapper(IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _conexionBD = new SqlConnection(_configuracion.GetConnectionString("BD"));
        }

        public SqlConnection ObtenerConexion()
        {
            return _conexionBD;
        }
    }
}
