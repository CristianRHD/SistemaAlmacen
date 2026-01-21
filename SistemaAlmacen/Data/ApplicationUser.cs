// Data/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace SistemaAlmacen.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Propiedades adicionales personalizadas
        [PersonalData]
        public string? NombreCompleto { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? UltimoAcceso { get; set; }

        // UserName será el campo para login (no email)
        // Identity ya lo tiene por defecto
    }
}