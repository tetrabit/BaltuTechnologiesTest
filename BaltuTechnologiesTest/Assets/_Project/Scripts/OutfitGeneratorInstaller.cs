using UnityEngine;
using Zenject;

public class OutfitGeneratorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<GenerateOutfitSignal>();
        Container.DeclareSignal<FilterWeatherSignal>();
        Container.DeclareSignal<FilterColorSignal>();
        Container.Bind<OutfitGenerator>().AsSingle();
        Container.Bind<ClothingView>().FromComponentsInHierarchy().AsCached();
        Container.BindInterfacesAndSelfTo<OutfitGeneratorDropdownState>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<OutfitGeneratorController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ClothingViewController>().AsSingle().NonLazy();
    }
}