using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlmacen.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(50)]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioCompra { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioVenta { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int StockMinimo { get; set; } = 10;

        [StringLength(50)]
        public string? Unidad { get; set; } = "Unidad";

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        public int CategoriaId { get; set; }

    

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } = null!;

      
        public Proveedor? Proveedor { get; set; }

        public string? UserId { get; set; } // ID del usuario de Identity que lo creó
    }
}

