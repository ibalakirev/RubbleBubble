using UnityEngine;

public class ColoredBallsDisabler : MonoBehaviour
{
    [SerializeField] private ColoredBallsPool _coloredSpheresPool;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ColoredBall coloredBalls))
        {
            RemoveColoredSphere(coloredBalls);
        }
    }

    public void RemoveColoredSphere(ColoredBall coloredBalls)
    {
        _coloredSpheresPool.ReturnObject(coloredBalls);
    }
}
