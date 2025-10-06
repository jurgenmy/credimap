/*
Cómo iniciar (desde la carpeta Credimap/):

1) Instalar dependencias:
   npm install

2) Iniciar servidor en http://localhost:5000
   npm start

3) Abrir un túnel público con localtunnel (requiere devDependency instalada):
   npm run tunnel
   # o manualmente: npx localtunnel --port 5000

Luego accede desde el celular:
 - URL pública que imprime localtunnel (por ej. https://algo.loca.lt)
*/

const path = require('path');
const fs = require('fs');
const express = require('express');

const app = express();
const PORT = process.env.PORT || 5000;

// Servir archivos estáticos desde wwwroot
const staticDir = path.join(__dirname, 'wwwroot');
app.use(express.static(staticDir));

// Endpoint para leer sucursales.csv desde la raíz del proyecto (Credimap/)
app.get('/api/sucursales', (req, res) => {
	const csvPath = path.join(__dirname, 'sucursales.csv');
	if (!fs.existsSync(csvPath)) {
		return res.status(404).json({ message: 'sucursales.csv no encontrado en la raíz del proyecto' });
	}
	const content = fs.readFileSync(csvPath, 'utf8');
	const lines = content.split(/\r?\n/).map(l => l.trim()).filter(Boolean);
	const result = [];
	for (const line of lines) {
		const parts = line.split(',');
		if (parts.length < 3) continue;
		let nombre, rubro = '', lat, lng;
		if (parts.length >= 4) {
			[nombre, rubro] = [parts[0].trim(), parts[1].trim()];
			lat = Number(parts[2]);
			lng = Number(parts[3]);
		} else {
			nombre = parts[0].trim();
			lat = Number(parts[1]);
			lng = Number(parts[2]);
		}
		if (Number.isFinite(lat) && Number.isFinite(lng)) {
			result.push({ nombre, rubro, lat, lng });
		}
	}
	res.json(result);
});

// Fallback: servir index.html
app.get('*', (_req, res) => {
	res.sendFile(path.join(staticDir, 'index.html'));
});

app.listen(PORT, () => {
	console.log(`Servidor Node escuchando en http://localhost:${PORT}`);
});
