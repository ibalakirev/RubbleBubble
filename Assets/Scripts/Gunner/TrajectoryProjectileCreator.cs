using UnityEngine;

public class TrajectoryProjectileCreator : MonoBehaviour
{
    [SerializeField] private TrajectoryVisualizer _path;
    [SerializeField] private Gunner _gunner;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BulletsReloader _projectileReloader;

    private void OnEnable()
    {
        _inputReader.AimingEnabled += EnableTrajectoryProjectile;
        _inputReader.AimingDisabled += DisableTrajectoryProjectile;
    }

    private void OnDisable()
    {
        _inputReader.AimingEnabled -= EnableTrajectoryProjectile;
        _inputReader.AimingDisabled += DisableTrajectoryProjectile;
    }

    private void TryShowTrajectoryProjectile()
    {
        _path.EnableLinear();

        _path.ShowTrajectory(transform.position, _gunner.ForceProjectile);

        if(_projectileReloader.CurrentProjectile != null)
        {
            _path.SetColor(_projectileReloader.CurrentProjectile.Color);
        }
    }

    private void EnableTrajectoryProjectile()
    {
        _path.EnableLinear();

        TryShowTrajectoryProjectile();
    }

    private void DisableTrajectoryProjectile()
    {
        _path.DisableLinear();
    }
}
