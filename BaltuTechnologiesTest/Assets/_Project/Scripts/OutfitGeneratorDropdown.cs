using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OutfitGeneratorDropdown : MonoBehaviour
{
    public enum OutfitGeneratorDropdownType
    {
        Weather,
        Color
    }

    private SignalBus _signalBus;
    private TMP_Dropdown _dropdown;
    [SerializeField]
    private OutfitGeneratorDropdownType _outfitGeneratorDropdownType;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdown.onValueChanged.AddListener(delegate { OnValueChanged(_dropdown); });
    }

    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveListener(delegate { OnValueChanged(_dropdown); });
    }

    private void OnValueChanged(TMP_Dropdown dropdown)
    {
        switch (_outfitGeneratorDropdownType)
        {
            case OutfitGeneratorDropdownType.Color:
                _signalBus.Fire(new FilterColorSignal(ConvertDropdownToColor()));
                break;
            case OutfitGeneratorDropdownType.Weather:
                _signalBus.Fire(new FilterWeatherSignal(ConvertDropdownToWeather()));
                break;
        }
    }
    
    private Clothing.ColorType ConvertDropdownToColor()
    {
        switch (_dropdown.options[_dropdown.value].text)
        {
            case (MagicString.ColorDropdown.Black):
                return Clothing.ColorType.Black;
            case (MagicString.ColorDropdown.Blue):
                return Clothing.ColorType.Blue;
            case (MagicString.ColorDropdown.Tan):
                return Clothing.ColorType.Tan;
            case (MagicString.ColorDropdown.Purple):
                return Clothing.ColorType.Purple;
            default:
                return Clothing.ColorType.None;
        }
    }

    private Clothing.WeatherType ConvertDropdownToWeather()
    {
        switch (_dropdown.options[_dropdown.value].text)
        {
            case (MagicString.WeatherDropdown.Hot):
                return Clothing.WeatherType.Hot;
            case (MagicString.WeatherDropdown.Cold):
                return Clothing.WeatherType.Cold;
            default:
                return Clothing.WeatherType.None;
        }
    }
}
