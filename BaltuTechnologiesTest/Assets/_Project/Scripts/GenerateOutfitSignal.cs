public class GenerateOutfitSignal 
{
    private OutfitGenerator.OutfitGenerationType _type;

    public OutfitGenerator.OutfitGenerationType Type { get => _type; set => _type = value; }

    public GenerateOutfitSignal()
    {

    }

    public GenerateOutfitSignal(OutfitGenerator.OutfitGenerationType type)
    {
        Type = type;
    }
}