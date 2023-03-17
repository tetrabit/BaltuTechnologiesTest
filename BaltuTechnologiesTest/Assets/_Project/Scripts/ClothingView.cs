using UnityEngine;
using UnityEngine.UI;

public class ClothingView : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Clothing.Type _clothingType;

    public Clothing.Type ClothingType { get => _clothingType; set => _clothingType = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateClothing(Clothing clothing)
    {
        _image.sprite = clothing.Sprite;
        _image.preserveAspect = true;
    }
}