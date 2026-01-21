window.descargarEnNuevaPestana = function (url) {
    // Abrir en nueva pestaña es la CLAVE para que Blazor no se desconecte
    const link = document.createElement('a');
    link.href = url;
    link.download = ''; // Intenta forzar descarga
    link.target = '_blank'; // <--- ESTO EVITA QUE TU APP SE CIERRE
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};