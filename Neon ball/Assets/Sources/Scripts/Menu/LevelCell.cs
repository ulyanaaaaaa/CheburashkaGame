using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class LevelCell : MonoBehaviour
{
    [SerializeField] private Image[] _stars;
    
    private Button _button;
    private LevelData _levelData;
    private SceneService _sceneService;
    private SaveService _saveService;
    private LevelSaveData _levelSaveData;

    [Inject]
    public void Constructor(SceneService sceneService, SaveService saveService)
    {
        _sceneService = sceneService;
        _saveService = saveService;
    }
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(OpenLevel);
        if (_saveService.CurrentSaveData.TryGetData(_levelData.Id, out LevelSaveData data))
        {
            for (int i = 0; i < data.CollectStars; i++)
            {
                _stars[i].gameObject.SetActive(true);
            }
        }
    }

    public LevelCell Setup(LevelData data)
    {
        _levelData = data;
        return this;
    }

    private void OpenLevel()
    {
        _sceneService.LoadScene(_levelData.Scene.Name);
    }
}
