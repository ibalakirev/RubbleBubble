using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ColoredBall coloredBall))
        {
            if (coloredBall.IsBlack == true)
            {
                Debug.Log("Тут будет событие для GameOver");
            }

            if (coloredBall.Color == _bullet.Color)
            {
                coloredBall.EnableIsCollision();

                _bullet.ReportRelease();
            }
        }

        if (collision.gameObject.TryGetComponent(out ColoredBallsDisabler disablerColoredBalls))
        {
            _bullet.ReportRelease();
        }
    }
}
