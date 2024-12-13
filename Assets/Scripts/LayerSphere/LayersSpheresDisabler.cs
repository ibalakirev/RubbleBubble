using System;
using System.Collections;
using UnityEngine;

public class LayersSpheresDisabler : MonoBehaviour
{
    [SerializeField] private LayerSpherePool _interlayerSpherePool;
    [SerializeField] private SpawnPointFirstLayerSphere _spawnPointFirstInterlayer;

    private float _radius = 100f;

    public event Action<LayerSphere> InterlayerDisabling;
    public event Action InterlayerDisabled;

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
                            InterlayerDisabling?.Invoke(layerSphere);

                            _interlayerSpherePool.ReturnObject(layerSphere);

                            InterlayerDisabled?.Invoke();
                        }
                    }
                }
            }

            yield return timeWait;
        }
    }
}
