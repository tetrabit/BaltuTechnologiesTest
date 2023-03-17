using Zenject;

public class OutfitGeneratorDropdownState : IInitializable
{
    private SignalBus _signalBus;
    private Clothing.ColorType _colorFilter = Clothing.ColorType.None;
    private Clothing.WeatherType _weatherFilter = Clothing.WeatherType.None;

    public Clothing.ColorType ColorFilter { get => _colorFilter; set => _colorFilter = value; }
    public Clothing.WeatherType WeatherFilter { get => _weatherFilter; set => _weatherFilter = value; }

    public OutfitGeneratorDropdownState(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<FilterColorSignal>(x => ColorFilter = x.Color);
        _signalBus.Subscribe<FilterWeatherSignal>(x => WeatherFilter = x.Weather);
    }
}
