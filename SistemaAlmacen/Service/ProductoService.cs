using Microsoft.EntityFrameworkCore;
using SistemaAlmacen.Data;
using SistemaAlmacen.Models;

namespace SistemaAlmacen.Services
{
    public class ProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<List<Categoria>> ObtenerCategoriasAsync()
        {
            return await _context.Categorias
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> AgregarAsync(Producto producto)
        {
            try
            {
                var existe = await _context.Productos.AnyAsync(p => p.Codigo == producto.Codigo);
                if (existe) return false;

                producto.FechaCreacion = DateTime.Now;
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            try
            {
                var existe = await _context.Productos
                    .AnyAsync(p => p.Codigo == producto.Codigo && p.Id != producto.Id);
                if (existe) return false;

                var productoExistente = await _context.Productos.FindAsync(producto.Id);
                if (productoExistente == null) return false;

                productoExistente.Codigo = producto.Codigo;
                productoExistente.Nombre = producto.Nombre;
                productoExistente.Descripcion = producto.Descripcion;
                productoExistente.CategoriaId = producto.CategoriaId;
                productoExistente.PrecioCompra = producto.PrecioCompra;
                productoExistente.PrecioVenta = producto.PrecioVenta;
                productoExistente.Stock = producto.Stock;
                productoExistente.StockMinimo = producto.StockMinimo;
                productoExistente.Unidad = producto.Unidad;
                productoExistente.Activo = producto.Activo;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}