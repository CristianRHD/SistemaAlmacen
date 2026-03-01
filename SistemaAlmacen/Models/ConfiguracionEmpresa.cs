using System.ComponentModel.DataAnnotations;

namespace SistemaAlmacen.Models
{
    public class ConfiguracionEmpresa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string NombreEmpresa { get; set; } = "AlmaTrack";

        [StringLength(200)]
        public string? Slogan { get; set; }

        [Required]
        [StringLength(5)]
        public string Iniciales { get; set; } = "AT";

        [StringLength(20)]
        public string? RNC { get; set; }

        [StringLength(30)]
        public string? Telefono { get; set; }

        [StringLength(300)]
        public string? Direccion { get; set; }

        [StringLength(150)]
        public string? Email { get; set; }

        // Logo almacenado en base64
        public string? LogoBase64 { get; set; }

        [StringLength(50)]
        public string? LogoMediaType { get; set; } = "image/png";
    }
}