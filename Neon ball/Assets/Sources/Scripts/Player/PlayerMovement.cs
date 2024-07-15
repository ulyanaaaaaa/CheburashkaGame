using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IPause
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private Vector3 _impulseBuffer;
    private bool _isPause;

    private PauseService _pauseService;

    [Inject]
    public void Constructor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _pauseService.AddPause(this);
        _playerInput.OnForward += GoForward;
        _playerInput.OnBack += GoBack;
        _playerInput.OnLeft += GoLeft;
        _playerInput.OnRight += GoRight;
    }
    
    private void OnDisable()
    {
        _pauseService.RemovePause(this);
        _playerInput.OnForward -= GoForward;
        _playerInput.OnBack -= GoBack;
        _playerInput.OnLeft -= GoLeft;
        _playerInput.OnRight -= GoRight;
    }

    private void Update()
    {
        if(_isPause)
            return;
        
        RotateCamera();
    }

    private void GoForward()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }
    
    private void GoBack()
    {
        _rigidbody.velocity = -transform.forward * _speed;
    }
    
    private void GoRight()
    {
        _rigidbody.velocity = transform.right * _speed;
    }
    
    private void GoLeft()
    {
        _rigidbody.velocity = -transform.right * _speed;
    }
    
    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed;
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }

    #region Pause
    public void Pause()
    {
        _impulseBuffer = _rigidbody.velocity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        _isPause = true;
    }

    public void Resume()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.velocity = _impulseBuffer;
        _isPause = false;
    }
    #endregion
}
