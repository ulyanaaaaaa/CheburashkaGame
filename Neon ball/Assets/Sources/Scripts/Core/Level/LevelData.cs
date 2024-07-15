using Udar.SceneManager;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/Data", order = 1)]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public SceneField Scene { get; private set; }
}
