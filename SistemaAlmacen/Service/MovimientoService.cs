using Microsoft.EntityFrameworkCore;
using SistemaAlmacen.Data;
using SistemaAlmacen.Models;

namespace SistemaAlmacen.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly ApplicationDbContext _context;

        public MovimientoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movimiento>> ObtenerTodosAsync()
        {
            return await _context.Movimientos
                .Include(m => m.Producto)
                .Include(m => m.Proveedor)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();
        }

        /// <summary>
        /// ENTRADA: sube Existencias, guarda precio de compra y proveedor en el movimiento.
        /// </summary>
        public async Task<bool> RegistrarEntradaAsync(
            int productoId,
            int cantidad,
            decimal precioCompra,
            int proveedorId,
            string? observaciones,
            string usuario)
        {
            if (cantidad <= 0) return false;

            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null) return false;

            producto.Existencias += cantidad;

            var movimiento = new Movimiento
            {
                Tipo = "Entrada",
                Fecha = DateTime.Now,
                ProductoId = productoId,
                Cantidad = cantidad,
                PrecioCompra = precioCompra,
                ProveedorId = proveedorId,
                Observaciones = observaciones,
                UsuarioResponsable = usuario
            };

            _context.Movimientos.Add(movimiento);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// SALIDA: baja Existencias, toma el último precio de compra registrado
        /// para ese producto y lo guarda en el movimiento de salida.
        /// Así el reporte puede calcular el valor de lo despachado.
        /// </summary>
        public async Task<bool> RegistrarSalidaAsync(
            int productoId,
            int cantidad,
            string destino,
            string? observaciones,
            string usuario)
        {
            if (cantidad <= 0) return false;

            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null) return false;

            if (producto.Existencias < cantidad) return false;

            // Buscar el último precio de compra registrado para este producto
            var ultimoPrecio = await _context.Movimientos
                .Where(m => m.ProductoId == productoId
                         && m.Tipo == "Entrada"
                         && m.PrecioCompra != null
                         && m.PrecioCompra > 0)
                .OrderByDescending(m => m.Fecha)
                .Select(m => m.PrecioCompra)
                .FirstOrDefaultAsync();

            producto.Existencias -= cantidad;

            var movimiento = new Movimiento
            {
                Tipo = "Salida",
                Fecha = DateTime.Now,
                ProductoId = productoId,
                Cantidad = cantidad,
                
                PrecioCompra = ultimoPrecio ?? 0,
                Destino = destino,
                Observaciones = observaciones,
                UsuarioResponsable = usuario
            };

            _context.Movimientos.Add(movimiento);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}