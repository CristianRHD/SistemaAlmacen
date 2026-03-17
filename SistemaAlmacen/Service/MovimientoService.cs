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
                    .ThenInclude(p => p.Categoria)
                .Include(m => m.Proveedor)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();
        }

        public async Task<bool> RegistrarEntradaAsync(
            int productoId,
            int cantidad,
            decimal precioCompra,
            int proveedorId,
            string? observaciones,
            string usuario)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var producto = await _context.Productos.FindAsync(productoId);
                if (producto == null) return false;

                var movimiento = new Movimiento
                {
                    Fecha = DateTime.Now,
                    Tipo = "Entrada",
                    ProductoId = productoId,
                    Cantidad = cantidad,
                    PrecioCompra = precioCompra,
                    ProveedorId = proveedorId,
                    Observaciones = observaciones,
                    UsuarioResponsable = usuario
                };

                _context.Movimientos.Add(movimiento);
                producto.Existencias += cantidad;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> RegistrarSalidaAsync(
            int productoId,
            int cantidad,
            string destino,
            string? observaciones,
            string usuario)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var producto = await _context.Productos.FindAsync(productoId);
                if (producto == null || producto.Existencias < cantidad) return false;

                var ultimaEntrada = await _context.Movimientos
                    .Where(m => m.ProductoId == productoId && m.Tipo == "Entrada" && m.PrecioCompra != null)
                    .OrderByDescending(m => m.Fecha)
                    .FirstOrDefaultAsync();

                var movimiento = new Movimiento
                {
                    Fecha = DateTime.Now,
                    Tipo = "Salida",
                    ProductoId = productoId,
                    Cantidad = cantidad,
                    PrecioCompra = ultimaEntrada?.PrecioCompra,
                    Destino = destino,
                    Observaciones = observaciones,
                    UsuarioResponsable = usuario
                };

                _context.Movimientos.Add(movimiento);
                producto.Existencias -= cantidad;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> RegistrarPerdidaAsync(
            int productoId,
            int cantidadLotes,
            int unidadesPerdidas,
            string motivoMerma,
            string? observaciones,
            string usuario)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var producto = await _context.Productos.FindAsync(productoId);
                if (producto == null) return false;

               
                int totalUnidadesPerdidas = (cantidadLotes * producto.UnidadesPorLote) + unidadesPerdidas;

               
                int totalUnidadesDisponibles = producto.Existencias * producto.UnidadesPorLote;
                if (totalUnidadesDisponibles < totalUnidadesPerdidas) return false;

             
                var ultimaEntrada = await _context.Movimientos
                    .Where(m => m.ProductoId == productoId && m.Tipo == "Entrada" && m.PrecioCompra != null)
                    .OrderByDescending(m => m.Fecha)
                    .FirstOrDefaultAsync();

                decimal precioPorLote = ultimaEntrada?.PrecioCompra ?? 0;

              
                var movimiento = new Movimiento
                {
                    Fecha = DateTime.Now,
                    Tipo = "Pérdida",
                    ProductoId = productoId,
                    Cantidad = cantidadLotes, 
                    UnidadesPerdidas = unidadesPerdidas, 
                    PrecioCompra = precioPorLote,
                    MotivoMerma = motivoMerma,
                    Observaciones = observaciones,
                    UsuarioResponsable = usuario
                };

                _context.Movimientos.Add(movimiento);

               
                producto.Existencias -= cantidadLotes;

               
                if (unidadesPerdidas > 0)
                {
                    producto.Existencias -= 1;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}