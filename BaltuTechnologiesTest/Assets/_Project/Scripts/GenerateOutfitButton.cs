using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GenerateOutfitButton : MonoBehaviour, IPointerClickHandler
{
    private SignalBus _signalBus;
    [SerializeField]
    private OutfitGenerator.OutfitGenerationType _type = OutfitGenerator.OutfitGenerationType.Random;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _signalBus.Fire(new GenerateOutfitSignal(_type));
    }
}
