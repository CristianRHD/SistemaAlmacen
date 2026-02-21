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

        public async Task<bool> RegistrarEntradaCompletaAsync(int productoId, int cantidad, decimal pCompra, decimal pVenta, int proveedorId, string nota, string usuario)
        {
            // 1. Buscamos el producto en su tabla original
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null) return false;

            // 2. ALTERAMOS LOS CAMPOS EN PRODUCTOS (Lo que ya tienes programado)
            // Esto asegura que tus tablas de productos actuales vean el cambio [cite: 2026-02-12]
            producto.Stock += cantidad;
            producto.PrecioCompra = pCompra;
            producto.PrecioVenta = pVenta;

            // 3. CREAMOS EL REGISTRO DE MOVIMIENTO (Para el historial)
            var mov = new Movimiento
            {
                ProductoId = productoId,
                Cantidad = cantidad,
                Tipo = "Entrada",
                PrecioCompraMov = pCompra, // Campo nuevo en la BD
                PrecioVentaMov = pVenta,   // Campo nuevo en la BD
                ProveedorId = proveedorId,
                Nota = nota,
                UsuarioResponsable = usuario,
                Fecha = DateTime.Now
            };

            _context.Movimientos.Add(mov);
            
           
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RegistrarSalidaInternaAsync(int productoId, int cantidad, string destino, string usuario)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null || producto.Stock < cantidad) return false;

            // Actualizamos stock en la tabla original
            producto.Stock -= cantidad; 

            var mov = new Movimiento
            {
                ProductoId = productoId,
                Cantidad = cantidad,
                Tipo = "Salida",
                Referencia = destino,
                UsuarioResponsable = usuario,
                Fecha = DateTime.Now
            };

            _context.Movimientos.Add(mov);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}