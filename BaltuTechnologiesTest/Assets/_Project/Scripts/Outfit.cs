using System.Collections.Generic;

public class Outfit
{
    private Dictionary<Clothing.Type, Clothing> _collection = new Dictionary<Clothing.Type, Clothing>();

    public Dictionary<Clothing.Type, Clothing> Collection { get => _collection; set => _collection = value; }
}

