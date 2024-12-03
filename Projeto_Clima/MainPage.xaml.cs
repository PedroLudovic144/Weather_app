namespace Projeto_Clima
{
    public partial class MainPage : ContentPage
    {
        private WeatherService _weatherService = new WeatherService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {
            try
            {
                var city = CityEntry.Text;
                if (string.IsNullOrWhiteSpace(city))
                {
                    await DisplayAlert("Erro", "Por favor, insira o nome de uma cidade.", "OK");
                    return;
                }

                var weather = await _weatherService.GetWeatherByCity(city);
                WeatherLabel.Text = $"Clima em {weather.Name}:\n" +
                                    $"{weather.Weather[0].Description}\n" +
                                    $"Temperatura: {weather.Main.Temp}°C\n" +
                                    $"Umidade: {weather.Main.Humidity}%";
            }
            catch
            {
                await DisplayAlert("Erro", "Não foi possível obter o clima. Verifique o nome da cidade.", "OK");
            }
        }
    }


}
