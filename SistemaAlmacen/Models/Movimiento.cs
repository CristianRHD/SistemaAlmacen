using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlmacen.Models
{
    public class Movimiento
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El tipo es requerido")]
        [StringLength(20)]
        public string Tipo { get; set; } = ""; 

        [Required(ErrorMessage = "La referencia es requerida")]
        [StringLength(50)]
        public string Referencia { get; set; } = "";

        [Required(ErrorMessage = "El producto es requerido")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int Cantidad { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }
    }
}