using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FailPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _restartButton;
    private LevelStateMachine _levelStateMachine;
    private SceneService _sceneService;
    private bool _isBegin;

    [Inject]
    public void Constructor(LevelStateMachine levelStateMachine, SceneService sceneService)
    {
        _levelStateMachine = levelStateMachine;
        _sceneService = sceneService;
    }

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        if (_isBegin)
            return;
        _isBegin = true;
        _sceneService.Restart();
    }

    private void OnEnable()
    {
        _levelStateMachine.OnStateChange += LevelStateHandle;
    } 
    
    private void OnDisable()
    {
        _levelStateMachine.OnStateChange -= LevelStateHandle;
    }

    private void LevelStateHandle(LevelState state)
    {
        if(state == LevelState.Fail)
            Open();
    }
    
    public void Open()
    {
        _panel.gameObject.SetActive(true);
    }
}
