using UnityEngine;

public class BulletsDisabler : MonoBehaviour
{
    [SerializeField] private SpawnerBullets _spawnerProjectiles;
    [SerializeField] private BulletsPool _projectilesPool;

    private void OnEnable()
    {
        _spawnerProjectiles.ProjectileCreated += SetProjectile;
    }

    private void OnDisable()
    {
        _spawnerProjectiles.ProjectileCreated -= SetProjectile;
    }

    private void SetProjectile(Bullet projectile)
    {
        projectile.Released += RemoveProjectile;
    }

    private void RemoveProjectile(Bullet projectile)
    {
        projectile.Released -= RemoveProjectile;

        _projectilesPool.ReturnObject(projectile);
    }
}
