using System;
using System.Collections;
using UnityEngine;

public class LayersSpheresDisabler : MonoBehaviour
{
    [SerializeField] private LayerSpherePool _interlayerSpherePool;
    [SerializeField] private SpawnPointFirstLayerSphere _spawnPointFirstInterlayer;

    private LayerSphere _currentInterlayer;
    private float _radius = 100f;

    public event Action InterlayerDisabled;

    public LayerSphere CurrentInterlayer => _currentInterlayer;

    private void Start()
    {
        StartCoroutine(TryRemoveInterlayers());
    }

    private IEnumerator TryRemoveInterlayers()
    {
        float delay = 0.2f;
        float minQuantityColoredBalls = 0f;
        float maxValueIdentifier = 3f;

        WaitForSeconds timeWait = new WaitForSeconds(delay);

        while (enabled)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(_spawnPointFirstInterlayer.transform.position, _radius);

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                if (overlappedColliders[i].TryGetComponent(out LayerSphere layerSphere))
                {
                    if (layerSphere.Identifier == maxValueIdentifier)
                    {
                        if(layerSphere.ColoredBalls.Count == minQuantityColoredBalls)
                        {
                            _currentInterlayer = layerSphere;

                            InterlayerDisabled?.Invoke();

                            _interlayerSpherePool.ReturnObject(layerSphere);
                        }
                    }
                }
            }

            yield return timeWait;
        }
    }
}
