using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaAlmacen.Data;

namespace SistemaAlmacen.Services
{
    public class UsuarioService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<RolInfo> ObtenerRolesDisponibles()
        {
            return new List<RolInfo>
            {
                new RolInfo
                {
                    Nombre = "Admin",
                    Descripcion = "Acceso total al sistema",
                    Icono = "👑",
                    Color = "#667eea",
                    Permisos = new List<string>
                    {
                        "Dashboard", "Productos", "Categorías", "Proveedores",
                        "Movimientos", "Reportes", "Usuarios"
                    }
                },
                new RolInfo
                {
                    Nombre = "Gerente",
                    Descripcion = "Gestión de inventario y reportes",
                    Icono = "👔",
                    Color = "#2196f3",
                    Permisos = new List<string>
                    {
                        "Dashboard", "Productos", "Categorías", "Proveedores",
                        "Movimientos", "Reportes"
                    }
                },
                new RolInfo
                {
                    Nombre = "Analista",
                    Descripcion = "Consulta y análisis de datos",
                    Icono = "📊",
                    Color = "#ff9800",
                    Permisos = new List<string>
                    {
                        "Dashboard", "Productos (solo lectura)", "Categorías (solo lectura)",
                        "Proveedores (solo lectura)", "Reportes"
                    }
                },
                
                new RolInfo
                {
                    Nombre = "Almacenista",
                    Descripcion = "Control de stock y movimientos",
                    Icono = "📦",
                    Color = "#9c27b0",
                    Permisos = new List<string>
                    {
                        "Dashboard", "Productos", "Movimientos"
                    }
                }
            };
        }

        public async Task<List<UsuarioDto>> ObtenerTodosAsync()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            var usuariosDto = new List<UsuarioDto>();

            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                usuariosDto.Add(new UsuarioDto
                {
                    Id = usuario.Id,
                    UserName = usuario.UserName ?? "",
                    Email = usuario.Email ?? "",
                    NombreCompleto = usuario.NombreCompleto ?? "",
                    FechaRegistro = usuario.FechaRegistro,
                    UltimoAcceso = usuario.UltimoAcceso,
                    Rol = roles.FirstOrDefault() ?? "Sin rol",
                    Activo = !usuario.LockoutEnabled || usuario.LockoutEnd == null || usuario.LockoutEnd <= DateTimeOffset.Now
                });
            }

            return usuariosDto.OrderByDescending(u => u.FechaRegistro).ToList();
        }

        public async Task<(bool Success, string Message, string? UserId)> CrearUsuarioAsync(string userName, string email, string nombreCompleto, string password, string rol)
        {
            try
            {
                var existeUsuario = await _userManager.FindByNameAsync(userName);
                if (existeUsuario != null)
                {
                    return (false, "El nombre de usuario ya existe", null);
                }

                var existeEmail = await _userManager.FindByEmailAsync(email);
                if (existeEmail != null)
                {
                    return (false, "El email ya está registrado", null);
                }

                if (!await _roleManager.RoleExistsAsync(rol))
                {
                    return (false, "El rol seleccionado no existe", null);
                }

                var nuevoUsuario = new ApplicationUser
                {
                    UserName = userName,
                    Email = email,
                    NombreCompleto = nombreCompleto,
                    EmailConfirmed = true,
                    FechaRegistro = DateTime.Now
                };

                var resultado = await _userManager.CreateAsync(nuevoUsuario, password);

                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
                    return (false, $"Error al crear usuario: {errores}", null);
                }

                await _userManager.AddToRoleAsync(nuevoUsuario, rol);

                return (true, "Usuario creado exitosamente", nuevoUsuario.Id);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> ActualizarUsuarioAsync(string userId, string userName, string email, string nombreCompleto, string? nuevaPassword, string rol)
        {
            try
            {
                var usuario = await _userManager.FindByIdAsync(userId);
                if (usuario == null)
                {
                    return (false, "Usuario no encontrado");
                }

                var existeUsuario = await _userManager.Users
                    .AnyAsync(u => u.UserName == userName && u.Id != userId);
                if (existeUsuario)
                {
                    return (false, "El nombre de usuario ya existe");
                }

                var existeEmail = await _userManager.Users
                    .AnyAsync(u => u.Email == email && u.Id != userId);
                if (existeEmail)
                {
                    return (false, "El email ya está registrado");
                }

                usuario.UserName = userName;
                usuario.Email = email;
                usuario.NombreCompleto = nombreCompleto;

                var resultado = await _userManager.UpdateAsync(usuario);
                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
                    return (false, $"Error al actualizar: {errores}");
                }

                var rolesActuales = await _userManager.GetRolesAsync(usuario);
                if (rolesActuales.Any())
                {
                    await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);
                }
                await _userManager.AddToRoleAsync(usuario, rol);

                if (!string.IsNullOrWhiteSpace(nuevaPassword))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var resultadoPassword = await _userManager.ResetPasswordAsync(usuario, token, nuevaPassword);

                    if (!resultadoPassword.Succeeded)
                    {
                        var errores = string.Join(", ", resultadoPassword.Errors.Select(e => e.Description));
                        return (false, $"Usuario actualizado pero error al cambiar contraseña: {errores}");
                    }
                }

                return (true, "Usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> EliminarUsuarioAsync(string userId)
        {
            try
            {
                var usuario = await _userManager.FindByIdAsync(userId);
                if (usuario == null)
                {
                    return (false, "Usuario no encontrado");
                }

                if (usuario.UserName == "admin")
                {
                    return (false, "No se puede eliminar el usuario administrador");
                }

                var roles = await _userManager.GetRolesAsync(usuario);
                if (roles.Contains("Admin"))
                {
                    return (false, "No se puede eliminar un usuario administrador");
                }

                var resultado = await _userManager.DeleteAsync(usuario);
                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
                    return (false, $"Error al eliminar: {errores}");
                }

                return (true, "Usuario eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> CambiarEstadoAsync(string userId)
        {
            try
            {
                var usuario = await _userManager.FindByIdAsync(userId);
                if (usuario == null)
                {
                    return (false, "Usuario no encontrado");
                }

                if (usuario.UserName == "admin")
                {
                    return (false, "No se puede desactivar el usuario administrador");
                }

                if (usuario.LockoutEnd == null || usuario.LockoutEnd <= DateTimeOffset.Now)
                {
                    await _userManager.SetLockoutEndDateAsync(usuario, DateTimeOffset.MaxValue);
                    return (true, "Usuario bloqueado");
                }
                else
                {
                    await _userManager.SetLockoutEndDateAsync(usuario, null);
                    return (true, "Usuario desbloqueado");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }

    public class UsuarioDto
    {
        public string Id { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public string Rol { get; set; } = "";
        public bool Activo { get; set; }
    }

    public class RolInfo
    {
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Icono { get; set; } = "";
        public string Color { get; set; } = "";
        public List<string> Permisos { get; set; } = new();
    }
}