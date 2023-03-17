using System;
using Zenject;
using UniRx;

public class OutfitGeneratorController : IInitializable
{
    private SignalBus _signalBus;
    private OutfitGenerator _outfitGenerator;
    private Outfit _outfit;
    private OutfitGeneratorDropdownState _outfitGeneratorDropdownState;

    public Outfit Outfit { get => _outfit; set => _outfit = value; }

    public static Action<Outfit> OnOutfitChanged;
    
    public OutfitGeneratorController(SignalBus signalBus, OutfitGenerator outfitGenerator,
        OutfitGeneratorDropdownState outfitGeneratorDropdownState)
    {
        _signalBus = signalBus;
        _outfitGenerator = outfitGenerator;
        _outfitGeneratorDropdownState = outfitGeneratorDropdownState;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GenerateOutfitSignal>(x => GenerateOutfit(x.Type));
    }

    /// <summary>
    /// Generates an outfit based on the type of generation
    /// </summary>
    /// <param name="type"></param>
    private void GenerateOutfit(OutfitGenerator.OutfitGenerationType type)
    {
        switch (type)
        {
            case OutfitGenerator.OutfitGenerationType.None:
                return;
            case OutfitGenerator.OutfitGenerationType.Random:
                GenerateRandomOutfit();
                break;
            case OutfitGenerator.OutfitGenerationType.Climate:
                GenerateWeatherSpecificOutfit();
                break;
            case OutfitGenerator.OutfitGenerationType.Color:
                GenerateColorSpecificOutfit();
                break;
        }

        OnOutfitChanged(_outfit);
    }

    /// <summary>
    /// Generates a random outfit
    /// </summary>
    private void GenerateRandomOutfit()
    {
        _outfit = _outfitGenerator.GenerateRandomOutfit();
    }

    /// <summary>
    /// Generates a weather specific outfit
    /// </summary>
    private void GenerateWeatherSpecificOutfit()
    {
        _outfit = _outfitGenerator.GenerateWeatherFilteredOutfit(_outfitGeneratorDropdownState.WeatherFilter);
    }

    /// <summary>
    /// Generates a color specific outfit
    /// </summary>
    private void GenerateColorSpecificOutfit()
    {
        _outfit = _outfitGenerator.GenerateColorFilteredOutfit(_outfitGeneratorDropdownState.ColorFilter);
    }
}

