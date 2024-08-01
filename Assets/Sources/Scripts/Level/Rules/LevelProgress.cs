using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Transform[] _starsStartPlace;
    [SerializeField] private LevelData _levelData;
    
    private Star _star;
    private DiContainer _container;
    private List<Star> _starsCreated = new List<Star>();
    private SaveService _saveService;
    
    [Inject]
    public void Constructor(DiContainer container, SaveService saveService)
    {
        _container = container;
        _saveService = saveService;
    }
    
    public int CollectStars { get; private set; }
    
    private void Awake()
    {
        _star = Resources.Load<Star>(AssetsPath.LevelPath.Star);
    }

    private void Start()
    {
        for (int i = 0; i < _starsStartPlace.Length; i++)
        {
            Star starCreated = _container.InstantiatePrefab(_star, _starsStartPlace[i].position, Quaternion.identity, null).GetComponent<Star>();
            _starsCreated.Add(starCreated);
            starCreated.OnCollect += () => CollectStars++;
        }

        _saveService.OnSave += Save;
    }

    public void OnDrawGizmos()
    {
        if(_starsStartPlace.Length == 0)
            return;
        for (int i = 0; i < _starsStartPlace.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_starsStartPlace[i].position, 0.2f);
        }
    }

    private void Save()
    {
        if (_saveService.CurrentSaveData.TryGetData(_levelData.Id, out LevelSaveData data))
        {
            if (data.CollectStars > CollectStars)
                CollectStars = data.CollectStars;
        }
        _saveService.CurrentSaveData.AddData(_levelData.Id, new LevelSaveData(CollectStars, _levelData.Id, typeof(LevelProgress)));
    }
}

[Serializable]
public class LevelSaveData : SaveData
{
    public int CollectStars { get; private set; }
    
    public LevelSaveData(int collectStars, string id, Type type) : base(id, type)
    {
        CollectStars = collectStars;
    }
}
