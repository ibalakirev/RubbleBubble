using UnityEngine;

public class ColoredBallsSeparator : MonoBehaviour
{
    [SerializeField] private SpawnPointFirstLayerSphere _spawnPointFirstLayerSphere;
    [SerializeField] private SpawnerColoredBalls _spawnerColoredSpheres;

    private float _radius = 100f;

    private void OnEnable()
    {
        _spawnerColoredSpheres.ColoredSphereReleased += TryTearOffMonochromeColoredSpheres;
    }

    private void OnDisable()
    {
        _spawnerColoredSpheres.ColoredSphereReleased -= TryTearOffMonochromeColoredSpheres;
    }

    private void TryTearOffMonochromeColoredSpheres(ColoredBall coloredSphere)
    {
        TryTearOffColoredSphere(coloredSphere);

        Collider[] overlappedColliders = Physics.OverlapSphere(_spawnPointFirstLayerSphere.transform.position, _radius);

        for (int j = 0; j < overlappedColliders.Length; j++)
        {
            if (overlappedColliders[j].TryGetComponent(out ColoredBall coloredBall))
            {
                if (coloredBall.Color == coloredSphere.Color)
                {
                    if (coloredBall.LayerSphere.Identifier >= coloredSphere.LayerSphere.Identifier)
                    {
                        coloredBall.FallDown();
                    }
                }
            }
        }
    }

    private void TryTearOffColoredSphere(ColoredBall coloredSphere)
    {
        if (coloredSphere.IsCollision == true)
        {
            coloredSphere.FallDown();
        }
    }
}

