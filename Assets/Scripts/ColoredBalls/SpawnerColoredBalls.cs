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
        Vector3 defaultScaleColoredSphere = new Vector3(0.15f, 0.15f, 0.15f);

        for (int i = 0; i < layerSphere.SpawnPointsPositionsColoredBalls.Length; i++)
        {
            ColoredBall coloredSphere = ObjectsPool.GetObject(layerSphere.SpawnPointsPositionsColoredBalls[i], Quaternion.identity);

            coloredSphere.EnableKinematic();

            SetMaterial(coloredSphere, _materialsRandomizer.GetRandomMaterialColoredBall());

            SetParent(coloredSphere, layerSphere.transform);

            TryAddSphere(coloredSphere, layerSphere);

            SetDefaultLocalScale(coloredSphere, defaultScaleColoredSphere);

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

    private void SetDefaultLocalScale(ColoredBall coloredSphere, Vector3 scale)
    {
        coloredSphere.transform.localScale = scale;
    }
}
