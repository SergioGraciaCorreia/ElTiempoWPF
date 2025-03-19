using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq; // Para FirstOrDefault
using System.Windows.Input;
using ElTiempoWPF.Models;
using ElTiempoWPF.Services;
using ElTiempoWPF.Helpers;
using Newtonsoft.Json; // Para parsear JSON

namespace ElTiempoWPF.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		// Fecha actual mostrada en la UI.
		private string _currentDate;
		public string CurrentDate
		{
			get => _currentDate;
			set { if (_currentDate != value) { _currentDate = value; OnPropertyChanged(); } }
		}

		// Ruta del gif a mostrar.
		private string _weatherGif;
		public string WeatherGif
		{
			get => _weatherGif;
			set { if (_weatherGif != value) { _weatherGif = value; OnPropertyChanged(); } }
		}

		// Ciudad ingresada en el TextBox.
		private string _city;
		public string City
		{
			get => _city;
			set { if (_city != value) { _city = value; OnPropertyChanged(); } }
		}

		// Comandos para buscar, y para "Hoy" y "Mañana".
		public ICommand SearchCommand { get; }
		public ICommand TodayCommand { get; }
		public ICommand TomorrowCommand { get; }

		public MainViewModel()
		{
			// Inicia con el TextBox vacío.
			City = "";
			// Inicializa los comandos.
			SearchCommand = new RelayCommand(o => LoadTodayWeatherData());
			TodayCommand = new RelayCommand(o => LoadTodayWeatherData());
			TomorrowCommand = new RelayCommand(o => LoadTomorrowWeatherData());

			// Carga datos por defecto para "Alicante" (Hoy).
			LoadTodayWeatherData("Alicante");
		}

		// Obtiene el clima actual (hoy) usando el endpoint "weather".
		private async void LoadTodayWeatherData(string query)
		{
			WeatherService service = new WeatherService();
			WeatherData data = await service.GetWeatherDataAsync(query);
			if (data != null)
			{
				DateTime date = DateTimeOffset.FromUnixTimeSeconds(data.dt).DateTime;
				CurrentDate = date.ToString("dd/MM/yyyy");
				SetWeatherGif(data, date);
			}
			else
			{
				CurrentDate = "¿?¿?¿?¿?¿?";
				WeatherGif = "Assets/default.gif";
			}
		}

		// Versión sin parámetro: usa la ciudad ingresada o "Alicante" si está vacía.
		private void LoadTodayWeatherData()
		{
			string query = string.IsNullOrEmpty(City) ? "Alicante" : City;
			LoadTodayWeatherData(query);
		}

		// Obtiene el pronóstico para "mañana" usando el endpoint "forecast".
		private async void LoadTomorrowWeatherData()
		{
			WeatherService service = new WeatherService();
			string query = string.IsNullOrEmpty(City) ? "Alicante" : City;
			// Llama al método que obtiene el JSON del forecast.
			string forecastJson = await service.GetForecastDataAsync(query);
			if (!string.IsNullOrEmpty(forecastJson))
			{
				// Convierte el JSON a un objeto ForecastData.
				ForecastData forecastData = JsonConvert.DeserializeObject<ForecastData>(forecastJson);
				// Calcula la fecha de mañana.
				DateTime tomorrow = DateTime.Today.AddDays(1);
				// Filtra los pronósticos de mañana y selecciona el que esté más cercano a las 12:00 (mediodía)
				ForecastItem tomorrowForecast = forecastData.list
					.Where(item => DateTimeOffset.FromUnixTimeSeconds(item.dt).DateTime.Date == tomorrow)
					.OrderBy(item => Math.Abs(DateTimeOffset.FromUnixTimeSeconds(item.dt).DateTime.Hour - 12))
					.FirstOrDefault();
				if (tomorrowForecast != null)
				{
					// Convierte el timestamp del pronóstico a DateTime y actualiza la fecha.
					DateTime date = DateTimeOffset.FromUnixTimeSeconds(tomorrowForecast.dt).DateTime;
					CurrentDate = date.ToString("dd/MM/yyyy");
					// Asigna el gif según la condición y hora del pronóstico seleccionado.
					SetWeatherGif(tomorrowForecast);
				}
				else
				{
					CurrentDate = "No forecast available";
					WeatherGif = "Assets/default.gif";
				}
			}
			else
			{
				CurrentDate = "¿?¿?¿?¿?¿?";
				WeatherGif = "Assets/default.gif";
			}
		}


		// Asigna el gif basado en datos actuales (WeatherData).
		private void SetWeatherGif(WeatherData data, DateTime date)
		{
			string condition = data.Weather[0].main;
			int hour = date.Hour;
			if (condition == "Clear")
				WeatherGif = (hour >= 7 && hour <= 19) ? "Assets/sun.gif" : "Assets/moon.gif";
			else if (condition == "Clouds")
				WeatherGif = "Assets/cloud.gif";
			else if (condition == "Rain")
				WeatherGif = "Assets/rain.gif";
			else
				WeatherGif = "Assets/default.gif";
		}

		// Asigna el gif basado en un pronóstico (ForecastItem) para mañana.
		private void SetWeatherGif(ForecastItem forecastItem)
		{
			string condition = forecastItem.weather[0].main;
			DateTime date = DateTimeOffset.FromUnixTimeSeconds(forecastItem.dt).DateTime;
			int hour = date.Hour;
			if (condition == "Clear")
				WeatherGif = (hour >= 7 && hour <= 19) ? "Assets/sun.gif" : "Assets/moon.gif";
			else if (condition == "Clouds")
				WeatherGif = "Assets/cloud.gif";
			else if (condition == "Rain")
				WeatherGif = "Assets/rain.gif";
			else
				WeatherGif = "Assets/default.gif";
		}

		// INotifyPropertyChanged para actualizar la UI.
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}







