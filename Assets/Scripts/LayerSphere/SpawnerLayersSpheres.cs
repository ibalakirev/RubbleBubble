using System;
using UnityEngine;

public class SpawnerLayersSpheres : Spawner<LayerSpherePool>
{
    [SerializeField] private SpawnPointFirstLayerSphere _pointPosition;
    [SerializeField] private LayersSpheresDisabler _disablerInterlayers;

    public event Action<LayerSphere> InterlayerReleased;

    private void Start()
    {
        int maxQuantityInterlayers = 3;

        for (int i = 0; i < maxQuantityInterlayers; i++)
        {
            Create();
        }
    }

    private void OnEnable()
    {
        _disablerInterlayers.InterlayerDisabled += Create;
    }

    private void OnDisable()
    {
        _disablerInterlayers.InterlayerDisabled -= Create;
    }

    public void Create()
    {
        LayerSphere sphereLayer = ObjectsPool.GetObject(_pointPosition.transform.position, Quaternion.identity);

        InterlayerReleased?.Invoke(sphereLayer);
    }
}
