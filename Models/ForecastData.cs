using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElTiempoWPF.Models
{
	// Representa la respuesta completa del forecast.
	public class ForecastData
	{
		public ForecastItem[] list { get; set; }  // Array de entradas de pronóstico.
		public City city { get; set; }            // Información de la ciudad.
	}

	// Cada entrada en el pronóstico (cada 3 horas).
	public class ForecastItem
	{
		public long dt { get; set; }              // Timestamp Unix.
		public MainData main { get; set; }        // Datos principales (temperatura, etc).
		public Weather[] weather { get; set; }    // Array de condiciones climáticas.
	}

	// Información básica de la ciudad.
	public class City
	{
		public string name { get; set; }
	}
}


