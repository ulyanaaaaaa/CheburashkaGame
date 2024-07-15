using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FinishPanelUI : MonoBehaviour
{
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _starsPosition;
    [SerializeField] private float _spaceBetweenStars = 65;
    [SerializeField] private Button _menuButton;
    private SceneService _sceneService;
    private SaveService _saveService;
    private LevelStateMachine _levelStateMachine;
    private Image _star;
    private bool _isBegin;
    
    [Inject]
    public void Constructor(LevelStateMachine levelStateMachine, SceneService sceneService, SaveService saveService)
    {
        _levelStateMachine = levelStateMachine;
        _sceneService = sceneService;
        _saveService = saveService;
    }

    private void Awake()
    {
        _star = Resources.Load<Image>(AssetsPath.UIPath.Star);
    }

    public void Start()
    {
        _menuButton.onClick.AddListener(LoadMenu);
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
        if(state == LevelState.Finish)
            Open();
    }
    
    private void Open()
    {
        _panel.gameObject.SetActive(true);
        
        for (int i = 0; i < _levelProgress.CollectStars; i++)
        {
            Instantiate(_star, _starsPosition.position + new Vector3(_spaceBetweenStars * i, 0, 0), Quaternion.identity, _starsPosition);
        }
    }

    private void LoadMenu()
    {
        if(_isBegin)
            return;
        _saveService.Save();
        _isBegin = true;
        _sceneService.LoadScene("Menu");
    }
}
