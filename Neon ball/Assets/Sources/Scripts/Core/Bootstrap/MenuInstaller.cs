using Zenject;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SaveService>()
            .FromComponentInNewPrefabResource(AssetsPath.ServicesPath.SaveService)
            .AsSingle();
        
        Container.Bind<TranslateService>()
            .FromComponentInNewPrefabResource(AssetsPath.ServicesPath.TranslateService)
            .AsSingle();
    }
}
