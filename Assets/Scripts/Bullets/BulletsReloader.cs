using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsReloader : MonoBehaviour
{
    [SerializeField] private SpawnerBullets _spawnerProjectiles;
    [SerializeField] private List<SpawnPointBullet> _spawnPointProjectiles;
    [SerializeField] private BulletsPool _projectilesPool;

    private Bullet _currentProjectile;
    private List<Bullet> _projectiles;
    private Coroutine _coroutine;
    private Vector3 _mainProjectilePosition;
    private Vector3 _nextProjectilePosition;
    public Bullet CurrentProjectile => _currentProjectile;

    private void Start()
    {
        _projectiles = new List<Bullet>();

        _mainProjectilePosition = _spawnPointProjectiles[0].transform.position;
        _nextProjectilePosition = _spawnPointProjectiles[1].transform.position;

        for (int i = 0; i < _spawnPointProjectiles.Count; i++)
        {
            Bullet bullet = _spawnerProjectiles.GetCreatedProjectile(_spawnPointProjectiles[i].transform.position, Quaternion.identity);

            _projectiles.Add(bullet);
        }

        SetCurrentProjectile(_projectiles[0]);
    }

    public void Recharge()
    {
        _projectiles.Remove(_projectiles[0]);

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(MoveProjectile(_projectiles[0]));

        SetCurrentProjectile(_projectiles[0]);

        TryCreateNewProjectile();
    }

    private IEnumerator MoveProjectile(Bullet projectiles)
    {
        float speedMovement = 15f;

        while (projectiles.transform.position != _mainProjectilePosition)
        {
            projectiles.transform.position = Vector3.Lerp(projectiles.transform.position, _mainProjectilePosition, speedMovement * Time.deltaTime);

            yield return null;
        }
    }

    private void TryCreateNewProjectile()
    {
        if (_mainProjectilePosition != _nextProjectilePosition)
        {
            Bullet newBullet = _spawnerProjectiles.GetCreatedProjectile(_nextProjectilePosition, Quaternion.identity);

            _projectiles.Add(newBullet);
        }
    }

    private void SetCurrentProjectile(Bullet projectile)
    {
        _currentProjectile = projectile;
    }
}
