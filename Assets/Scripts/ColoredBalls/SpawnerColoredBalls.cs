using System;
using UnityEngine;

public class SpawnerColoredBalls : Spawner<ColoredBallsPool>
{
    [SerializeField] private SpawnerLayersSpheres _spawnerLayersSpheres;
    [SerializeField] private MaterialsRandomizer _materialsRandomizer;

    public event Action<ColoredBall> ColoredSphereReleased;

    private void OnEnable()
    {
        _spawnerLayersSpheres.InterlayerReleased += Create;
    }

    private void OnDisable()
    {
        _spawnerLayersSpheres.InterlayerReleased -= Create;
    }

    private void Create(LayerSphere layerSphere)
    {
        for (int i = 0; i < layerSphere.BallsPositions.Count; i++)
        {
            Transform spawnPoint = layerSphere.BallsPositions[i].transform;

            ColoredBall coloredSphere = ObjectsPool.GetObject(spawnPoint.position, Quaternion.identity);

            SetMaterial(coloredSphere, _materialsRandomizer.GetRandomMaterialColoredBall());

            TryAddSphere(coloredSphere, layerSphere);

            SetParent(coloredSphere, layerSphere.transform);

            SetPosition(coloredSphere, spawnPoint.position);

            SetLocalScale(coloredSphere, spawnPoint);

            coloredSphere.SetLayerSphere(layerSphere);

            coloredSphere.Released += ReportReleasedColoredSphere;
        }
    }

    private void ReportReleasedColoredSphere(ColoredBall coloredSphere)
    {
        coloredSphere.Released -= ReportReleasedColoredSphere;

        ColoredSphereReleased?.Invoke(coloredSphere);
    }

    private void TryAddSphere(ColoredBall coloredSphere, LayerSphere layerSphere)
    {
        if (coloredSphere.Color == _materialsRandomizer.BlackMaterial.color)
        {
            layerSphere.AddBlackBall(coloredSphere);

            coloredSphere.EnableIsBlack();
        }
        else
        {
            layerSphere.AddColoredBall(coloredSphere);
        }
    }

    private void SetMaterial(ColoredBall coloredSphere, Material material)
    {
        coloredSphere.SetMaterial(material);
    }

    private void SetParent(ColoredBall coloredSphere, Transform parent)
    {
        coloredSphere.transform.SetParent(parent, false);
    }

    private void SetPosition(ColoredBall coloredSphere, Vector3 position)
    {
        coloredSphere.transform.position = position;
    }

    private void SetLocalScale(ColoredBall coloredSphere, Transform scale)
    {
        coloredSphere.transform.localScale = scale.localScale;
    }
}
