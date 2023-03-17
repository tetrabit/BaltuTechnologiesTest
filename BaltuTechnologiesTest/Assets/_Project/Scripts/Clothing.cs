using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Clothing", menuName = "ScriptableObjects/Clothing", order = 1)]
public class Clothing : ScriptableObject
{
    public enum Type
    {
        Jacket,
        Shirt,
        Pants,
        Shoes,
        Hat
    }

    public enum ColorType
    {
        None,
        Blue,
        Purple,
        Tan,
        Black,
        White,
        Red,
        Grey,
        Green
    }

    public enum WeatherType
    {
        None,
        Hot,
        Cold
    }

    [SerializeField]
    private Type _clothingType;
    [SerializeField]
    private string _name;
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private ColorType _color;
    [SerializeField]
    private WeatherType _weather;

    public Type ClothingType { get => _clothingType; set => _clothingType = value; }
    public string Name { get => _name; set => _name = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public ColorType Color { get => _color; set => _color = value; }
    public WeatherType Weather { get => _weather; set => _weather = value; }
}
