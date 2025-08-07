using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class CategoriaDA : ICategoriaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearCategoria(CategoriaRequest categoria)
        {
            string sqlQuery = @"sp_Categoria_Crear";

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idCategoria = Guid.NewGuid(),
                nombre = categoria.Nombre,
                descripcion = categoria.Descripcion,
                fechaCreacion = DateTime.Now,
                estado = categoria.Estado
            });

            return resultadoQuery;
        }

        public async Task<Guid> EditarCategoria(Guid id, CategoriaRequest categoria)
        {
            await VerificarCategoria(id);

            string sqlQuery = @"sp_Categoria_Editar";

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idCategoria = id,
                nombre = categoria.Nombre,
                descripcion = categoria.Descripcion,
                estado = categoria.Estado
            });

            return resultadoQuery;
        }

        public async Task<Guid> EliminarCategoria(Guid id)
        {
            await VerificarCategoria(id);

            string sqlQuery = @"sp_Categoria_Eliminar";

            return await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new { idCategoria = id });
        }

        public async Task<CategoriaResponse> ObtenerCategoriaPorId(Guid id)
        {
            string sqlQuery = @"sp_Categoria_ObtenerPorId";

            return await _sqlConnection.QueryFirstOrDefaultAsync<CategoriaResponse>(sqlQuery, new { idCategoria = id });
        }

        public async Task<IEnumerable<CategoriaResponse>> ObtenerTodasLasCategorias()
        {
            string sqlQuery = @"sp_Categoria_ObtenerTodos";

            return await _sqlConnection.QueryAsync<CategoriaResponse>(sqlQuery);
        }

        private async Task VerificarCategoria(Guid id)
        {
            var categoriaCheck = await ObtenerCategoriaPorId(id);

            if (categoriaCheck == null)
            {
                throw new Exception($"No se encontró la categoría con ID: {id}");
            }
        }
    }
}
