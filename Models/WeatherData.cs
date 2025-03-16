using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ElTiempoWPF.Models
{
	// Representa la respuesta principal de la API
	public class WeatherData
	{
		public string Name { get; set; }            // Nombre de la ciudad
		public MainData Main { get; set; }          // Información principal (temperatura, etc.)
		public Weather[] Weather { get; set; }      // Array de información sobre el clima (descripción, icono, etc.)
		public long dt { get; set; }                // Timestamp de la actualización
	}

	public class MainData
	{
		public double temp { get; set; }            // Temperatura actual
													// Añadir otros campos como feels_like, pressure, humidity, etc.
	}

	public class Weather
	{
		public string main { get; set; }            // Categoría del clima (Clouds, Clear, etc.)
		public string description { get; set; }     // Descripción (broken clouds, clear sky, etc.)
		public string icon { get; set; }            // Código del icono
	}
}

