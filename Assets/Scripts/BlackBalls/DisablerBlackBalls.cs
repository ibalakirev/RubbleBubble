using UnityEngine;

public class DisablerBlackBalls : MonoBehaviour
{
    [SerializeField] private LayersSpheresDisabler _disablerInterlayers;
    [SerializeField] private ColoredBallsPool _coloredBallsPool;


    private void OnEnable()
    {
        _disablerInterlayers.InterlayerDisabled += RemoveBlackSphere;
    }

    private void OnDisable()
    {
        _disablerInterlayers.InterlayerDisabled -= RemoveBlackSphere;
    }

    private void RemoveBlackSphere()
    {
        if(_disablerInterlayers.CurrentInterlayer != null)
        {
            for (int i = 0; i < _disablerInterlayers.CurrentInterlayer.BlackBalls.Count; i++)
            {
                _coloredBallsPool.ReturnObject(_disablerInterlayers.CurrentInterlayer.BlackBalls[i]);
            }
        }
    }
}
