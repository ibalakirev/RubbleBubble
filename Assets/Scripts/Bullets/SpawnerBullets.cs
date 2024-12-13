using System;
using UnityEngine;

public class SpawnerBullets : Spawner<BulletsPool>
{
    [SerializeField] private MaterialsRandomizer _colorRandomizer;

    public event Action<Bullet> ProjectileCreated;

    public Bullet GetCreatedProjectile(Vector3 position, Quaternion rotation)
    {
        Bullet newProjectile = ObjectsPool.GetObject(position, rotation);

        _colorRandomizer.GetRandomMaterialProjectile();

        newProjectile.SetMaterial(_colorRandomizer.GetRandomMaterialProjectile());

        newProjectile.EnableKinematic();

        ProjectileCreated?.Invoke(newProjectile);

        return newProjectile;
    }
}
