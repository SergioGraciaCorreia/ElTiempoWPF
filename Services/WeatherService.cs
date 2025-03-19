using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ElTiempoWPF.Models;

namespace ElTiempoWPF.Services
{
	public class WeatherService
	{
		// API key y URL base de OpenWeatherMap.
		private readonly string _apiKey = "c9aab10c899eb9d7707fecac615ddfab";
		private readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/weather";

		// Obtiene los datos del clima para una ciudad.
		public async Task<WeatherData> GetWeatherDataAsync(string city)
		{
			using (HttpClient client = new HttpClient())
			{
				try
				{
					// Construye la URL con parámetros.
					string url = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric&lang=es";

					// Envía la petición y verifica que sea exitosa.
					HttpResponseMessage response = await client.GetAsync(url);
					response.EnsureSuccessStatusCode();

					// Lee la respuesta en formato JSON.
					string result = await response.Content.ReadAsStringAsync();

					// Convierte el JSON a un objeto WeatherData.
					WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(result);
					return weatherData;
				}
				catch (Exception ex)
				{
					// Muestra error y retorna null.
					// MessageBox.Show("Error al obtener datos: " + ex.Message);
					// De momento comentado porque con lo que se ve en pantalla es suficiente.
					return null;
				}
			}
		}

		public async Task<string> GetForecastDataAsync(string city)
		{
			using (HttpClient client = new HttpClient())
			{
				try
				{
					// Usamos el endpoint "forecast" para pronósticos.
					string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric&lang=es";
					HttpResponseMessage response = await client.GetAsync(url);
					response.EnsureSuccessStatusCode();
					string result = await response.Content.ReadAsStringAsync();
					return result;
				}
				catch (Exception ex)
				{
					//MessageBox.Show("Error al obtener el pronóstico: " + ex.Message);
					// De momento comentado porque con lo que se ve en pantalla es suficiente.
					return null;
				}
			}
		}

	}


}