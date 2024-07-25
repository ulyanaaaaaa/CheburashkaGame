using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour, IPause
{ 
    public bool IsPause;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce;
    
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PauseService _pauseService;
    private Vector3 _impulseBuffer;
    private GroundChecker _groundCheker;
    [SerializeField] private bool _isGround;
    
    [Inject]
    public void Constructor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _groundCheker = GetComponentInChildren<GroundChecker>();
    }

    private void OnEnable()
    {
        _pauseService.AddPause(this);
        _playerInput.OnForward += GoForward;
        _playerInput.OnBack += GoBack;
        _playerInput.OnLeft += GoLeft;
        _playerInput.OnRight += GoRight;
        _playerInput.OnRun += Run;
        _playerInput.OnIdle += Idle;
        _playerInput.OnJump += Jump;
        _groundCheker.OnEnter += SetGround;
        _groundCheker.OnExit += RemoveGround;
    }
    
    private void OnDisable()
    {
        _pauseService.RemovePause(this);
        _playerInput.OnForward -= GoForward;
        _playerInput.OnBack -= GoBack;
        _playerInput.OnLeft -= GoLeft;
        _playerInput.OnRight -= GoRight;
        _playerInput.OnRun -= Run;
        _playerInput.OnIdle -= Idle;
        _playerInput.OnJump -= Jump;
        _groundCheker.OnEnter -= SetGround;
        _groundCheker.OnExit -= RemoveGround;
    }

    private void Update()
    {
        RotateCamera();

        if (transform.position.y > 0)
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * 7.5f * Time.deltaTime;;
    }
    
    private void Jump()
    {
        if (!_isGround)
            return; 
        
        _rigidbody.velocity =  new Vector3(_rigidbody.velocity.x, _jumpForce, _rigidbody.velocity.z);
    }

    private void Idle()
    {
        _animator.SetFloat("Animation", 0.33333333333f);
    }

    private void Run()
    {
        _rigidbody.velocity = transform.forward * _speed * 2;
        _animator.SetFloat("Animation", 0.222222222f);
    }

    private void GoForward()
    {
        _rigidbody.velocity = transform.forward * _speed;
        _animator.SetFloat("Animation", 0);
    }
    
    private void GoBack()
    {
        _rigidbody.velocity = -transform.forward * _speed;
        _animator.SetFloat("Animation", 0.1111111111f);
    }
    
    private void GoRight()
    {
        _rigidbody.velocity = transform.right * _speed;
        _animator.SetFloat("Animation", 0);
    }
    
    private void GoLeft()
    {
        _rigidbody.velocity = -transform.right * _speed;
        _animator.SetFloat("Animation", 0);
    }
    
    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed;
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
    
    private void SetGround()
    {
        _isGround = true;
    }

    private void RemoveGround()
    {
        _isGround = false;
    }

    #region Pause
    public void Pause()
    {
        _impulseBuffer = _rigidbody.velocity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        IsPause = true;
    }

    public void Resume()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.velocity = _impulseBuffer;
        IsPause = false;
    }
    #endregion
}
