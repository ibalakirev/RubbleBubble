using UnityEngine;

public class Gunner : MonoBehaviour
{
    [SerializeField] private float _power = 10;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BulletsReloader _projectileReloader;

    private Camera _mainCamera;
    private Vector3 _forceProjectile;
    private Vector3 _endPositionProjectile;

    public Vector3 ForceProjectile => _forceProjectile;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _inputReader.AimingReleased += SetTrajectoryPath;
        _inputReader.ShootReleased += ReleaseProjectile;
    }

    private void OnDisable()
    {
        _inputReader.AimingReleased -= SetTrajectoryPath;
        _inputReader.ShootReleased -= ReleaseProjectile;
    }

    private void SetTrajectoryPath(Vector3 position)
    {
        float enter;
        Vector3 direction = new Vector3(0f, 1f, -1f);

        Ray ray = _mainCamera.ScreenPointToRay(position);

        new Plane(direction, transform.position).Raycast(ray, out enter);

        _endPositionProjectile = ray.GetPoint(enter);

        _forceProjectile = (_endPositionProjectile - transform.position) * _power;
    }

    private void ReleaseProjectile()
    {
        if (_projectileReloader.CurrentProjectile != null)
        {
            _projectileReloader.CurrentProjectile.SetForce(_forceProjectile);
            _projectileReloader.CurrentProjectile.UseForce();

            _projectileReloader.Recharge();
        }
    }
}