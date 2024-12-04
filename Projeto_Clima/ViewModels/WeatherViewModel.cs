using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ProjetoClima.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ProjetoClima.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private const string ApiKey = "adc3136478ba1b7b728b05af03d861f1\r\n"; 
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        private string _cityName;
        private string _weatherInfo;
        private bool _isBusy;

        public string CityName
        {
            get => _cityName;
            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }

        public string WeatherInfo
        {
            get => _weatherInfo;
            set
            {
                _weatherInfo = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetWeatherCommand { get; }

        public WeatherViewModel()
        {
            GetWeatherCommand = new Command(async () => await GetWeatherAsync());
        }

        private async Task GetWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(CityName)) return;

            IsBusy = true;
            try
            {
                using var client = new HttpClient();
                var url = $"{BaseUrl}?q={CityName}&appid={ApiKey}&units=metric&lang=pt_br";
                var response = await client.GetStringAsync(url);
                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

                WeatherInfo = $"Clima em {weatherResponse.Name}:\n" +
                              $"{weatherResponse.Weather[0].Description}\n" +
                              $"Temperatura: {weatherResponse.Main.Temp}°C\n" +
                              $"Umidade: {weatherResponse.Main.Humidity}%";
            }
            catch
            {
                WeatherInfo = "Erro ao obter o clima. Verifique o nome da cidade.";
            }
            finally
            {
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
