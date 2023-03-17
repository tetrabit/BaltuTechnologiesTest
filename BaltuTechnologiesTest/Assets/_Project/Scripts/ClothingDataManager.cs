using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class AssetReferenceClothing : AssetReferenceT<Clothing>
{
    public AssetReferenceClothing(string guid) : base(guid) { }
}

public class ClothingDataManager : IInitializable
{
    private const string CLOTHING_LABEL = "clothing";

    [SerializeField]
    private List<Clothing> _jackets = new List<Clothing>();
    [SerializeField]
    private List<Clothing> _shirts = new List<Clothing>();
    [SerializeField]
    private List<Clothing> _pants = new List<Clothing>();
    [SerializeField]
    private List<Clothing> _shoes = new List<Clothing>();
    [SerializeField]
    private List<Clothing> _hats = new List<Clothing>();
    AsyncOperationHandle<IList<Clothing>> _pullClothing;

    public List<Clothing> Jackets { get => _jackets; set => _jackets = value; }
    public List<Clothing> Shirts { get => _shirts; set => _shirts = value; }
    public List<Clothing> Pants { get => _pants; set => _pants = value; }
    public List<Clothing> Shoes { get => _shoes; set => _shoes = value; }
    public List<Clothing> Hats { get => _hats; set => _hats = value; }
    public AsyncOperationHandle<IList<Clothing>> PullClothing { get => _pullClothing; set => _pullClothing = value; }
    public static Action PullClothingCompleted;

    public ClothingDataManager() { }

    public void Initialize()
    {
        PullClothingData();
    }

    private void PullClothingData()
    {
        _pullClothing = Addressables.LoadAssetsAsync<Clothing>(
           new List<string> { "clothing" }, // Either a single key or a List of keys 
           addressable =>
           {
               //Gets called for every loaded asset
               if (addressable != null)
               {
                   switch(addressable.ClothingType)
                   {
                       case Clothing.Type.Jacket:
                           Jackets.Add(addressable);
                           break;
                       case Clothing.Type.Shirt:
                           Shirts.Add(addressable);
                           break;
                       case Clothing.Type.Pants:
                           Pants.Add(addressable);
                           break;
                       case Clothing.Type.Shoes:
                           Shoes.Add(addressable);
                           break;
                       case Clothing.Type.Hat:
                           Hats.Add(addressable);
                           break;
                   }
               }
           }, Addressables.MergeMode.Union, // How to combine multiple labels 
           false); // Whether to fail if any asset fails to load
        _pullClothing.Completed += LoadHandle_Completed;
    }

    private void LoadHandle_Completed(AsyncOperationHandle<IList<Clothing>> operation)
    {
        if (operation.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning("Some assets did not load.");
        }
        PullClothingCompleted?.Invoke();
    }

    /// <summary>
    /// Get the full list of clothes filtered by clothing type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<Clothing> GetClothingList(Clothing.Type type)
    {
        switch (type)
        {
            case Clothing.Type.Jacket:
                return _jackets;
            case Clothing.Type.Shirt:
                return _shirts;
            case Clothing.Type.Pants:
                return _pants;
            case Clothing.Type.Shoes:
                return _shoes;
            case Clothing.Type.Hat:
                return _hats;
        }

        return null;
    }
    
    /// <summary>
    /// Get a random clothing item from the list based on clothing type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Clothing GetRandomClothingItem(Clothing.Type type)
    {
        return GetClothingList(type)[UnityEngine.Random.Range(0, GetClothingList(type).Count)];
    }

    /// <summary>
    /// Get a random clothing item from the list based on clothing type and clothing color
    /// if no matches just return a random clothing item of correc type
    /// </summary>
    /// <param name="type"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public Clothing GetColorFilteredClothingItem(Clothing.Type type, Clothing.ColorType color)
    {
        List<Clothing> filteredList = GetClothingList(type).Where(x => x.Color == color).ToList();

        if(filteredList.Count != 0)
            return filteredList[UnityEngine.Random.Range(0, filteredList.Count)];
        else
            return GetRandomClothingItem(type);
    }

    /// <summary>
    /// Get a random clothing item from the list based on clothing type and clothing weather
    /// if no matches just return a random clothing item of correct type
    /// </summary>
    /// <param name="type"></param>
    /// <param name="weather"></param>
    /// <returns></returns>
    public Clothing GetWeatherFilteredClothingItem(Clothing.Type type, Clothing.WeatherType weather)
    {
        List<Clothing> filteredList = GetClothingList(type).Where(x => x.Weather == weather).ToList();

        if (filteredList.Count != 0)
            return filteredList[UnityEngine.Random.Range(0, filteredList.Count)];
        else
            return GetRandomClothingItem(type);
    }
}
