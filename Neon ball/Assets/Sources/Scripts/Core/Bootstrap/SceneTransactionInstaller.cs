using UnityEngine;
using Zenject;

public class SceneTransactionInstaller : MonoInstaller
{
    [SerializeField] private FadePanel _fadePanel;

    public override void InstallBindings()
    {
        Container.Bind<FadePanel>().FromInstance(_fadePanel);
        
        Container.Bind<SceneService>()
            .FromComponentInNewPrefabResource(AssetsPath.ServicesPath.SceneService)
            .AsSingle();
    }
}
