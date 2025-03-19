# 🌤 EL TIEMPO WPF

**Welcome to El Tiempo WPF!**  
This is a simple weather application developed in WPF (Windows Presentation Foundation) that fetches current weather data and forecast from the OpenWeatherMap API. The app displays the date and an animated GIF representing the current weather condition. Users can search for a city and toggle between "Hoy" and "Mañana" to view today's weather or tomorrow's forecast.

---

## 🌡️ App Description  
El Tiempo WPF provides current weather information and a simple forecast for the selected city. It retrieves data such as the current date and weather condition, and uses that information to display one of the following GIFs:  
- **sun.gif** for clear conditions during the day,  
- **moon.gif** for clear conditions at night,  
- **cloud.gif** for cloudy weather, and  
- **rain.gif** for rainy conditions.  

For any unrecognized condition, a default GIF is shown.

---

## 🛠️ Main Methods  
### 1. Weather Data Retrieval  
- **`GetWeatherDataAsync(string city)`**:  
  Fetches current weather data from the OpenWeatherMap "weather" endpoint.  
- **`GetForecastDataAsync(string city)`**:  
  Retrieves forecast data (5-day, 3-hour intervals) from the "forecast" endpoint.

### 2. Data Processing & UI Update  
- **`LoadTodayWeatherData(string query)`**:  
  Processes current weather data, converts the timestamp, and updates the UI with the current date and corresponding weather GIF.  
- **`LoadTomorrowWeatherData()`**:  
  Processes forecast data to extract the prediction closest to 12:00 PM for tomorrow and updates the UI.  
- **`SetWeatherGif(...)`**:  
  Determines which GIF to display based on the weather condition (Clear, Clouds, Rain) and the time of day.

### 3. User Interaction  
- **Commands (Search, Hoy, Mañana)**:  
  Allow the user to search for a city and switch between today's weather and tomorrow's forecast.

---

## 📖 How to Use  
1. **Search for a City**:  
   Type a city name into the search box and click the "Buscar" button.  
2. **View Weather**:  
   Use the "Hoy" and "Mañana" buttons to toggle between the current weather and the forecast for tomorrow.  
3. **Weather Visualization**:  
   The application displays the date and an animated GIF corresponding to the weather condition (sun for clear day, moon for clear night, etc.).

---

## ⚙️ System Requirements  
- **OS**: Windows 7 or higher.  
- **.NET Framework**: Version 4.7.2 or higher.  
- **API Key**: A valid OpenWeatherMap API key is required (configured in the app).  
- **Resources**: Animated GIFs are stored in the `Assets` folder.

---

## 📂 Project Structure  
- **`MainWindow.xaml`**:  
  Contains the UI layout.  
- **`MainWindow.xaml.cs`**:  
  Code-behind for UI initialization.  
- **`ViewModels/`**:  
  Contains `MainViewModel`, which implements MVVM logic for data binding and commands.  
- **`Models/`**:  
  Contains `WeatherData` and `ForecastData` classes representing the API responses.  
- **`Services/`**:  
  Contains `WeatherService` for fetching weather and forecast data from OpenWeatherMap.  
- **`Helpers/`**:  
  Contains utility classes like `RelayCommand` for handling commands.  
- **`Assets/`**:  
  Contains animated GIFs (e.g., sun.gif, moon.gif, cloud.gif, rain.gif, default.gif).

---

## 🎨 Credits  
- **Developed by**: Sergio Gracia Correia.  
- **GIFs**: Created with Aseprite.  
- **API**: Weather data provided by [OpenWeatherMap](https://openweathermap.org/).  
- **Inspiration**: This project was built to learn WPF, the MVVM pattern, and GitHub workflows.
