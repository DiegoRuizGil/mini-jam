using GameEvents;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _reloadingTime = 1f;
    [SerializeField] private int _maxHealth = 3;

    private int _currentHealth;

    [Header("Events")]
    [SerializeField] private VoidEvent _onShootEvent;
    [SerializeField] private VoidEvent _onPlayerDeath;
    
    private Camera _camera;

    private Vector2 _movementDirection;
    
    private bool _isReloading;

    private Animator _animator;

    private void Awake()
    {
        _camera = Camera.main;
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        ApplyRotation();
        
        ApplyMovement();
        CheckShoot();
    }

    private void CheckShoot()
    {
        if (_isReloading) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_onShootEvent != null)
            {
                StartCoroutine(StartReloading());
                _onShootEvent.Raise();
            }
        }
    }

    public void UpdateHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

        if (_currentHealth == 0)
        {
            if (_onPlayerDeath != null)
                _onPlayerDeath.Raise();
        }
    }

    private IEnumerator StartReloading()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadingTime);
        _isReloading = false;
    }

    private void ApplyRotation()
    {
        float angle = GetShootRotation(GetMousePosition());
        _shootPoint.rotation = Quaternion.Euler(0f, 0f, angle);

        if (angle < 45 && angle > 315)
        {
            _animator.SetTrigger("Right");
        }
        else if (angle > 45 && angle < 135)
        {
            _animator.SetTrigger("Up");
        }
        else if (angle > 135 && angle < 225)
        {
            _animator.SetTrigger("Left");
        }
        else if (angle > 225 && angle < 315)
        {
            _animator.SetTrigger("Down");
        }
    }

    private void ApplyMovement()
    {
        UpdateMovementInput();
        transform.Translate(_movementDirection * (_movementSpeed * Time.deltaTime));
    }

    private void UpdateMovementInput()
    {
        _movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private float GetShootRotation(Vector3 mousePosition)
    {
        Vector3 distance = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        
        return angle;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
