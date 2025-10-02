# Credimap

Aplicación ASP.NET Core 8 con SignalR y frontend Mapbox GL JS.

## Requisitos
- .NET SDK 8
- (Opcional) Token público de Mapbox

## Ejecutar (HTTP o HTTPS)
```bash
cd Credimap
# HTTP y HTTPS; puedes usar solo HTTP si prefieres
 dotnet run --urls "http://0.0.0.0:5000;https://0.0.0.0:5001"
```

Abre en el navegador del PC:
- http://localhost:5000
- https://localhost:5001 (certificado dev)

Desde un celular Android en la misma red Wi‑Fi:
- Averigua la IP de tu PC (por ejemplo 192.168.0.10)
- Abre: `http://192.168.0.10:5000` o `https://192.168.0.10:5001`

## Configura tu token de Mapbox
Edita `wwwroot/index.html` y reemplaza `REEMPLAZAR_CON_TU_TOKEN_PUBLICO_MAPBOX`.

## API
- GET `/api/sucursales` → `[ { nombre, lat, lng } ]`
- SignalR hub `/hubs/location`
	- Cliente envía: `SendLocation(userId, lat, lng)`
	- Servidor emite a otros: `ReceiveLocationUpdate({ userId, lat, lng })` 