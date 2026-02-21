using Microsoft.EntityFrameworkCore;
using SistemaAlmacen.Data;
using SistemaAlmacen.Models;

namespace SistemaAlmacen.Services
{
    public class CategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObtenerTodosAsync()
        {
            
            return await _context.Categorias
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<Categoria?> ObtenerPorIdAsync(int id)
        {
            return await _context.Categorias
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> AgregarAsync(Categoria categoria)
        {
            try
            {
                
                var existe = await _context.Categorias.AnyAsync(c => c.Nombre == categoria.Nombre);
                if (existe) return false;

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(Categoria categoria)
        {
            try
            {
                var existe = await _context.Categorias
                    .AnyAsync(c => c.Nombre == categoria.Nombre && c.Id != categoria.Id);
                if (existe) return false;

                var categoriaExistente = await _context.Categorias.FindAsync(categoria.Id);
                if (categoriaExistente == null) return false;

                categoriaExistente.Nombre = categoria.Nombre;
                categoriaExistente.Descripcion = categoria.Descripcion;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

      
        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null) return false;

               
                _context.Categorias.Remove(categoria);

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                
                return false;
            }
        }

        public async Task<int> ContarProductosAsync(int categoriaId)
        {
            return await _context.Productos
                .Where(p => p.CategoriaId == categoriaId) 
                .CountAsync();
        }
    }
}