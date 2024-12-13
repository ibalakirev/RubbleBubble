using UnityEngine;

public class DisablerBlackBalls : MonoBehaviour
{
    [SerializeField] private LayersSpheresDisabler _disablerInterlayers;
    [SerializeField] private ColoredBallsPool _coloredBallsPool;


    private void OnEnable()
    {
        _disablerInterlayers.InterlayerDisabling += RemoveBlackSphere;
    }

    private void OnDisable()
    {
        _disablerInterlayers.InterlayerDisabling -= RemoveBlackSphere;
    }

    private void RemoveBlackSphere(LayerSphere layerSphere)
    {
        for (int i = 0; i < layerSphere.BlackBalls.Count; i++)
        {
            layerSphere.BlackBalls[i].FallDown();
        }
    }
}

