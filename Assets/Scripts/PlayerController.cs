using GameEvents;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform _shootPoint;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed;

    [Header("Events")]
    [SerializeField] private VoidEvent _onShootEvent;
    
    public Vector3 pos;
    
    private Camera _camera;
    private Vector3 _mousePosition;

    private Vector2 _movementDirection;

    
    private bool _isReloading;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        _mousePosition = GetMousePosition();
        float angle = GetShootRotation(GetMousePosition());
        _shootPoint.rotation = Quaternion.Euler(0f, 0f, angle);
        
        ApplyMovement();
        CheckShoot();
    }

    private void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_onShootEvent != null)
                _onShootEvent.Raise();
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
