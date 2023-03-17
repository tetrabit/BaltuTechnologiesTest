using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClothingViewController : IInitializable
{
    private SignalBus _signalBus;
    private List<ClothingView> _clothingViews;
    private ClothingDataManager _clothingDataManager;

    [Inject]
    public void Construct(SignalBus signalBus, List<ClothingView> clothingViewer, ClothingDataManager clothingDataManager)
    {
        _signalBus = signalBus;
        _clothingViews = clothingViewer;
        _clothingDataManager = clothingDataManager;
    }

    public void Initialize()
    {
        OutfitGeneratorController.OnOutfitChanged += UpdateOutfit;
    }

    /// <summary>
    /// Updates the clothing views with the new outfit
    /// </summary>
    /// <param name="outfit"></param>
    public void UpdateOutfit(Outfit outfit)
    {
        foreach (Clothing.Type item in outfit.Collection.Keys)
        {
            var clothingView = _clothingViews.Find(x => x.ClothingType == item);
            clothingView.UpdateClothing(outfit.Collection[item]);
        }
    }
}
