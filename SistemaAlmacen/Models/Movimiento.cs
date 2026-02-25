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

        [Required(ErrorMessage = "El producto es requerido")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int Cantidad { get; set; }

      
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PrecioCompra { get; set; }

        
        public int? ProveedorId { get; set; }

        [ForeignKey("ProveedorId")]
        public Proveedor? Proveedor { get; set; }

        
        [StringLength(200)]
        public string? Destino { get; set; }

       
        public string? Observaciones { get; set; }

        public string? UsuarioResponsable { get; set; }
    }
}