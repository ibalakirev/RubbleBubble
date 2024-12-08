using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputController _inputController;
    private bool _isAiming;

    public event Action<Vector3> AimingReleased;
    public event Action AimingEnabled;
    public event Action AimingDisabled;
    public event Action ShootReleased;

    private void Awake()
    {
        _inputController = new InputController();
    }

    private void OnEnable()
    {
        _inputController.ShootController.Shoot.canceled += Shoot;
        _inputController.AimController.Aimimg.performed += EnableAim;
        _inputController.AimController.Aimimg.canceled += DisableAim;

        _inputController.Enable();
    }

    private void OnDisable()
    {
        _inputController.ShootController.Shoot.canceled -= Shoot;
        _inputController.AimController.Aimimg.performed -= EnableAim;
        _inputController.AimController.Aimimg.canceled -= DisableAim;

        _inputController.Disable();
    }

    private void Update()
    {
        if (_isAiming == true)
        {
            TakeAim();

            AimingEnabled?.Invoke();
        }
    }

    public void EnableAim(InputAction.CallbackContext context)
    {
        _isAiming = true;
    }

    public void DisableAim(InputAction.CallbackContext context)
    {
        AimingDisabled?.Invoke();

        _isAiming = false;
    }

    private void TakeAim()
    {
        Vector3 position = _inputController.AimController.AimPosition.ReadValue<Vector2>();

        AimingReleased?.Invoke(position);
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        ShootReleased?.Invoke();
    }
}
