window.descargarEnNuevaPestana = function (url) {
    const link = document.createElement('a');
    link.href = url;
    link.download = '';
    link.target = '_blank';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

window.printConduce = function (data) {
    const html = `
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8" />
    <title>Conduce ${data.referencia}</title>
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }

        body {
            font-family: Arial, Helvetica, sans-serif;
            background: white;
            color: #000;
            padding: 2cm 2.2cm;
            font-size: 11pt;
        }

        /* ── CABECERA ── */
        .conduce-header {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            border-bottom: 3px solid #1414b8;
            padding-bottom: 18px;
            margin-bottom: 22px;
        }

        .brand-box { display: flex; align-items: center; gap: 14px; }

        .brand-logo {
            width: 56px; height: 56px;
            background: #1414b8;
            color: white;
            font-size: 18pt;
            font-weight: 900;
            display: flex; align-items: center; justify-content: center;
            border-radius: 10px;
        }

        .brand-name {
            font-size: 24pt;
            font-weight: 900;
            color: #1414b8;
            letter-spacing: -0.5px;
        }

        .brand-sub {
            font-size: 7.5pt;
            color: #555;
            text-transform: uppercase;
            letter-spacing: 0.1em;
            margin-top: 3px;
        }

        .reference-box { text-align: right; }

        .ref-label {
            font-size: 8pt;
            text-transform: uppercase;
            letter-spacing: 0.1em;
            color: #555;
            font-weight: 700;
        }

        .ref-number {
            font-size: 15pt;
            font-weight: 900;
            color: #1414b8;
            font-family: 'Courier New', monospace;
            margin: 4px 0;
        }

        .ref-date { font-size: 9pt; color: #333; }

        /* ── INFO GRID ── */
        .info-grid {
            display: flex;
            border: 1px solid #ccc;
            margin-bottom: 22px;
        }

        .info-item {
            flex: 1;
            padding: 10px 14px;
            border-right: 1px solid #ccc;
        }

        .info-item:last-child { border-right: none; }

        .info-label {
            display: block;
            font-size: 7pt;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 0.08em;
            color: #666;
            margin-bottom: 4px;
        }

        .info-value {
            font-size: 10.5pt;
            font-weight: 700;
            color: #000;
        }

        /* ── TABLA ── */
        table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
        }

        thead tr { background: #1414b8; }

        thead th {
            color: white;
            padding: 9px 12px;
            font-size: 8.5pt;
            font-weight: 700;
            text-align: left;
            text-transform: uppercase;
            letter-spacing: 0.07em;
        }

        tbody td {
            padding: 9px 12px;
            border-bottom: 1px solid #e0e0e0;
            font-size: 10.5pt;
            color: #000;
            word-wrap: break-word;
        }

        tbody tr:nth-child(even) td { background: #f5f5f5; }

        .total-row td {
            padding: 10px 12px;
            border-top: 2.5px solid #1414b8;
            border-bottom: none;
            font-weight: 700;
            font-size: 10.5pt;
        }

        /* ── OBSERVACIONES ── */
        .obs {
            margin-top: 18px;
            padding: 10px 14px;
            border: 1px solid #ddd;
            font-size: 9.5pt;
            color: #333;
        }

        /* ── FIRMAS ── */
        .firmas {
            display: flex;
            justify-content: space-around;
            margin-top: 90px;
        }

        .firma {
            width: 36%;
            text-align: center;
        }

        .linea {
            border-top: 1.5px solid #000;
            margin-bottom: 8px;
        }

        .firma p {
            font-size: 8pt;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 0.08em;
        }

        @media print {
            body { padding: 0; }
            @page { size: letter portrait; margin: 1.5cm 2cm; }
        }
    </style>
</head>
<body>

    <div class="conduce-header">
        <div class="brand-box">
            <div class="brand-logo">AT</div>
            <div>
                <div class="brand-name">ALMATRACK</div>
                <div class="brand-sub">Sistema de Control de Inventario</div>
            </div>
        </div>
        <div class="reference-box">
            <div class="ref-label">Conduce de Salida</div>
            <div class="ref-number">${data.referencia}</div>
            <div class="ref-date">${data.fecha}</div>
        </div>
    </div>

    <div class="info-grid">
        <div class="info-item">
            <span class="info-label">Origen</span>
            <span class="info-value">ALMACÉN CENTRAL</span>
        </div>
        <div class="info-item">
            <span class="info-label">Destino</span>
            <span class="info-value">${data.destino || 'NO ESPECIFICADO'}</span>
        </div>
        <div class="info-item">
            <span class="info-label">Autorizado por</span>
            <span class="info-value">${data.usuario}</span>
        </div>
    </div>

    <table>
        <thead>
            <tr>
                <th style="width:18%">Código</th>
                <th style="width:62%">Descripción del Artículo</th>
                <th style="width:20%; text-align:center">Cantidad</th>
            </tr>
        </thead>
        <tbody>
            ${data.items.map(i => `
            <tr>
                <td>${i.codigo}</td>
                <td>${i.nombre}</td>
                <td style="text-align:center">${i.cantidad}</td>
            </tr>`).join('')}
        </tbody>
        <tfoot>
            <tr class="total-row">
                <td colspan="2" style="text-align:right">Total Unidades Despachadas:</td>
                <td style="text-align:center">${data.items.reduce((s, i) => s + i.cantidad, 0)}</td>
            </tr>
        </tfoot>
    </table>

    <div class="obs">
        <strong>Observaciones:</strong> ${data.observaciones || 'Sin observaciones.'}
    </div>

    <div class="firmas">
        <div class="firma">
            <div class="linea"></div>
            <p>Despachado por</p>
        </div>
        <div class="firma">
            <div class="linea"></div>
            <p>Recibido por (Destino)</p>
        </div>
    </div>

    <script>
        window.onload = function () {
            window.print();
            window.onafterprint = function () { window.close(); };
        };
    </script>
</body>
</html>`;

    const win = window.open('', '_blank', 'width=900,height=700');
    win.document.open();
    win.document.write(html);
    win.document.close();
};
