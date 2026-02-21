using SistemaAlmacen.Models;

namespace SistemaAlmacen.Services
{
    public interface IMovimientoService
    {
        Task<List<Movimiento>> ObtenerTodosAsync();
        // Este es el método clave para tus entradas automatizadas
        Task<bool> RegistrarEntradaCompletaAsync(int productoId, int cantidad, decimal pCompra, decimal pVenta, int proveedorId, string nota, string usuario);
        // Este para las salidas internas de la empresa
        Task<bool> RegistrarSalidaInternaAsync(int productoId, int cantidad, string destino, string usuario);
    }
}