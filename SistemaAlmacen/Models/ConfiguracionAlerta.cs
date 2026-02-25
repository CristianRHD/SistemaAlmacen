using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlmacen.Models
{
    public class ConfiguracionAlerta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required]
        public int ExistenciasMinimas { get; set; }

        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public string? UsuarioModifico { get; set; }
    }
}