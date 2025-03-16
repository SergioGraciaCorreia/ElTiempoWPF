using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ElTiempoWPF.Models;
using ElTiempoWPF.Services;
using ElTiempoWPF.Helpers;

namespace ElTiempoWPF.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private string _currentDate;
		public string CurrentDate
		{
			get => _currentDate;
			set
			{
				if (_currentDate != value)
				{
					_currentDate = value;
					OnPropertyChanged();
				}
			}
		}

		// Propiedad enlazada al TextBox (se muestra vacía)
		private string _city;
		public string City
		{
			get => _city;
			set
			{
				if (_city != value)
				{
					_city = value;
					OnPropertyChanged();
				}
			}
		}

		// Comando para buscar una ciudad (usado por el botón "Buscar")
		public ICommand SearchCommand { get; }

		public MainViewModel()
		{
			// Deja la propiedad City vacía para que el TextBox inicie vacío.
			City = "";

			// Inicializa el comando.
			SearchCommand = new RelayCommand(o => LoadWeatherData(City));

			// Carga inicialmente los datos de "Alicante" como valor por defecto.
			LoadWeatherData("Alicante");
		}

		// Método asíncrono que obtiene los datos de la API y actualiza CurrentDate.
		// Recibe como parámetro la ciudad a consultar.
		private async void LoadWeatherData(string query)
		{
			WeatherService service = new WeatherService();
			WeatherData data = await service.GetWeatherDataAsync(query);
			if (data != null)
			{
				DateTime date = DateTimeOffset.FromUnixTimeSeconds(data.dt).DateTime;
				CurrentDate = date.ToString("dd/MM/yyyy");
			}
			else
			{
				CurrentDate = "¿?¿?¿?¿?¿?"; // Muestra si fecha inválida.
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}




