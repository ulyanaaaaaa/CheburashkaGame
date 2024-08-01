using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private FailTrigger _failTrigger;
    [SerializeField] private LevelStateMachine _levelStateMachine;

    public override void InstallBindings()
    {
        Container.Bind<PauseService>()
            .FromComponentInNewPrefabResource(AssetsPath.ServicesPath.PauseService)
            .AsSingle();      
        
        Container.Bind<SaveService>()
            .FromComponentInNewPrefabResource(AssetsPath.ServicesPath.SaveService)
            .AsSingle();  
        
        Container.Bind<TranslateService>()
            .FromComponentInNewPrefabResource(AssetsPath.ServicesPath.TranslateService)
            .AsSingle();
        
        Container.Bind<FinishTrigger>()
            .FromInstance(_finishTrigger)
            .AsSingle();       
        
        Container.Bind<FailTrigger>()
            .FromInstance(_failTrigger)
            .AsSingle();
        
        Container.Bind<LevelStateMachine>()
            .FromInstance(_levelStateMachine)
            .AsSingle();
    }
}
