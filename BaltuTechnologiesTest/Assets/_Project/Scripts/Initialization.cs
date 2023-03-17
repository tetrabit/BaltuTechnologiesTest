using System;
using UnityEngine;
using Zenject;

public class Initialization : IInitializable, IDisposable
{
    public Initialization() { }

    public void LoadMainScene()
    {
        SceneLoader.LoadScene(MagicString.Scene.OutfitRecommendation);
    }

    public void Initialize()
    {
        ClothingDataManager.PullClothingCompleted += LoadMainScene;
    }

    public void Dispose()
    {
        ClothingDataManager.PullClothingCompleted -= LoadMainScene;
    }
}