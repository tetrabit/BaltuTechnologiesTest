public class OutfitGenerator
{
    public enum OutfitGenerationType
    {
        None,
        Random,
        Climate,
        Color
    }

    private ClothingDataManager _clothingDataManager;
    
    public OutfitGenerator(ClothingDataManager clothingDataManager)
    {
        _clothingDataManager = clothingDataManager;
    }

    /// <summary>
    /// Generates a random outfit
    /// </summary>
    /// <returns></returns>
    public Outfit GenerateRandomOutfit()
    {
        Outfit outfit = new Outfit();

        for (int i = 0; i < Clothing.Type.GetNames(typeof(Clothing.Type)).Length; i++)
        {
            Clothing.Type clothingType = (Clothing.Type)i;
            outfit.Collection.Add(clothingType, _clothingDataManager.GetRandomClothingItem(clothingType));
        }

        return outfit;
    }

    /// <summary>
    /// Generates a weather specific outfit
    /// </summary>
    /// <returns></returns>
    public Outfit GenerateWeatherFilteredOutfit(Clothing.WeatherType weather)
    {
        Outfit outfit = new Outfit();

        for (int i = 0; i < Clothing.Type.GetNames(typeof(Clothing.Type)).Length; i++)
        {
            Clothing.Type clothingType = (Clothing.Type)i;
            outfit.Collection.Add(clothingType, _clothingDataManager.GetWeatherFilteredClothingItem(clothingType, weather));
        }

        return outfit;
    }

    /// <summary>
    /// Generates a color specific outfit
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public Outfit GenerateColorFilteredOutfit(Clothing.ColorType color)
    {
        Outfit outfit = new Outfit();

        for (int i = 0; i < Clothing.Type.GetNames(typeof(Clothing.Type)).Length; i++)
        {
            Clothing.Type clothingType = (Clothing.Type)i;
            outfit.Collection.Add(clothingType, _clothingDataManager.GetColorFilteredClothingItem(clothingType, color));
        }

        return outfit;
    }
}
