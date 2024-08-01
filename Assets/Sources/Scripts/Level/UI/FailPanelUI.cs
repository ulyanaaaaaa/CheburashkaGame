using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FailPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    private LevelStateMachine _levelStateMachine;
    private SaveService _saveService;
    private SceneService _sceneService;
    private bool _isBegin;

    [Inject]
    public void Constructor(LevelStateMachine levelStateMachine, SceneService sceneService, SaveService saveService)
    {
        _levelStateMachine = levelStateMachine;
        _sceneService = sceneService;
        _saveService = saveService;
    }

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
        _menuButton.onClick.AddListener(LoadMenu);
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
    
    private void LoadMenu()
    {
        if(_isBegin)
            return;
        _saveService.Save();
        _isBegin = true;
        _sceneService.LoadScene("Menu");
    }
    
    public void Open()
    {
        _panel.gameObject.SetActive(true);
    }
}
