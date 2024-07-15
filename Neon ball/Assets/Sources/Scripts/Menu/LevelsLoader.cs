using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelsLoader : MonoBehaviour
{
    [SerializeField] private List<LevelData> _data;
    
    private LevelCell _cell;
    private DiContainer _container;

    [Inject]
    public void Constructor(DiContainer container)
    {
        _container = container;
    }
    
    private void Awake()
    {
        _cell = Resources.Load<LevelCell>(AssetsPath.DataPath.LevelCell);
        GenerateCells();
    }

    private void GenerateCells()
    {
        for (int i = 0; i < _data.Count; i++)
        {
            _container.InstantiatePrefabForComponent<LevelCell>(_cell, transform.position + new Vector3(150 * i, 0, 0), Quaternion.identity, transform)
                .Setup(_data[i]);
        }
    }
}
