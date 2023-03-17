public class FilterColorSignal
{
    private Clothing.ColorType _color;

    public Clothing.ColorType Color { get => _color; set => _color = value; }
    
    public FilterColorSignal(Clothing.ColorType color)
    {
        _color = color;
    }
}
