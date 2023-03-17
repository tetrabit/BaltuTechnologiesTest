public class FilterWeatherSignal
{
    private Clothing.WeatherType _weather;

    public Clothing.WeatherType Weather { get => _weather; set => _weather = value; }

    public FilterWeatherSignal(Clothing.WeatherType weather)
    {
        _weather = weather;
    }
}