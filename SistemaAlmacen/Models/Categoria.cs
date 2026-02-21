using System.ComponentModel.DataAnnotations;

namespace SistemaAlmacen.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}