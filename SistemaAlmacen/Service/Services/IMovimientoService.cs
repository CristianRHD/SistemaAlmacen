using SistemaAlmacen.Models;

namespace SistemaAlmacen.Services
{
    public interface IMovimientoService
    {
        Task<List<Movimiento>> ObtenerTodosAsync();

    
        Task<bool> RegistrarEntradaAsync(
            int productoId,
            int cantidad,
            decimal precioCompra,
            int proveedorId,
            string? observaciones,
            string usuario);

        
        Task<bool> RegistrarSalidaAsync(
            int productoId,
            int cantidad,
            string destino,
            string? observaciones,
            string usuario);
    }
}