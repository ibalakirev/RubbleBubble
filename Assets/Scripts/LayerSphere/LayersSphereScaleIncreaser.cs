using System.Collections.Generic;
using UnityEngine;

public class LayersSphereScaleIncreaser : MonoBehaviour
{
    private const float FirstIdentifier = 1f;
    private const float SecondIdentifier = 2f;
    private const float ThirdIdentifier = 3f;

    [SerializeField] private SpawnerLayersSpheres _spawnerInterlayer;
    [SerializeField] private SpawnPointFirstLayerSphere _spawnPointFirstInterlayer;

    private float _radius = 100f;
    private List<Vector3> _scaleInterlayer;
    private Vector3 _firstScaleTarget;
    private Vector3 _secondScaleTarget;
    private Vector3 _thirdScaleTarget;


    private void Start()
    {
        _scaleInterlayer = new List<Vector3>
        {
            new Vector3(0.7f, 0.7f, 0.7f),
            new Vector3(1.1f, 1.1f, 1.1f),
            new Vector3(1.8f, 1.8f, 1.8f)
        };

        _firstScaleTarget = _scaleInterlayer[0];
        _secondScaleTarget = _scaleInterlayer[1];
        _thirdScaleTarget = _scaleInterlayer[2];
    }

    private void OnEnable()
    {
        _spawnerInterlayer.InterlayerReleased += TryIncreaseInterlayers;
    }

    private void OnDisable()
    {
        _spawnerInterlayer.InterlayerReleased -= TryIncreaseInterlayers;
    }

    private void TryIncreaseInterlayers(LayerSphere Interlayer)
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(_spawnPointFirstInterlayer.transform.position, _radius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            if (overlappedColliders[i].TryGetComponent(out LayerSphere interlayer))
            {
                interlayer.IncreaseIdentifier();

                TryIncreaseInterlayer(FirstIdentifier, interlayer, _firstScaleTarget);
                TryIncreaseInterlayer(SecondIdentifier, interlayer, _secondScaleTarget);
                TryIncreaseInterlayer(ThirdIdentifier, interlayer, _thirdScaleTarget);
            }
        }
    }

    private void TryIncreaseInterlayer(float identifierInterlayer, LayerSphere interlayer, Vector3 targetScale)
    {
        if (interlayer.Identifier == identifierInterlayer)
        {
            interlayer.IncreaseScale(targetScale);
        }
    }
}
