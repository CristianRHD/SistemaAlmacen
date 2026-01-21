using System.ComponentModel.DataAnnotations;

namespace SistemaAlmacen.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }

        [StringLength(50)]
        public string? RNC { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}