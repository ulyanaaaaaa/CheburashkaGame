using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _player; 
    [SerializeField] private float _speed;
    [SerializeField] private LevelStateMachine _level;

    private void Update()
    {
        Vector3 direction = _player.position - transform.position;
        direction.Normalize();
        
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            _level.ChangeState(LevelState.Fail);
        }
    }
}
