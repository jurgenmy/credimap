using System.Globalization;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Credimap.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SucursalesController : ControllerBase
	{
		private readonly IWebHostEnvironment _env;

		public SucursalesController(IWebHostEnvironment env)
		{
			_env = env;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var csvPath = Path.Combine(_env.ContentRootPath, "sucursales.csv");
			if (!System.IO.File.Exists(csvPath))
			{
				return NotFound(new { message = "sucursales.csv no encontrado en la raíz del proyecto" });
			}

			var lines = System.IO.File.ReadAllLines(csvPath);
			var culture = CultureInfo.InvariantCulture;
			var result = new List<object>();

			foreach (var rawLine in lines)
			{
				var line = rawLine.Trim();
				if (string.IsNullOrWhiteSpace(line)) continue;

				var parts = line.Split(',');
				if (parts.Length < 3) continue;

				var nombre = parts[0].Trim();
				if (!double.TryParse(parts[1], NumberStyles.Float, culture, out var lat)) continue;
				if (!double.TryParse(parts[2], NumberStyles.Float, culture, out var lng)) continue;

				result.Add(new { nombre, lat, lng });
			}

			return Ok(result);
		}
	}
} 