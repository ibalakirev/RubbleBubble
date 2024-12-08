using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSphere : MonoBehaviour
{
    [SerializeField] private List<SpawnPointColoredBall> _ballsPositions;

    private List<ColoredBall> _coloredBalls;
    private List<ColoredBall> _blackBalls;
    private Coroutine _coroutine;
    private Vector3 _targetScale;
    private float _identifier;
    private float _speedIncreaseScale;

    public float Identifier => _identifier;

    public List<SpawnPointColoredBall> BallsPositions => _ballsPositions;
    public List<ColoredBall> ColoredBalls => _coloredBalls;
    public List<ColoredBall> BlackBalls => _blackBalls;

    private void OnEnable()
    {
        float startScaleX = 0.02f;
        float startScaleY = 0.02f;
        float startScaleZ = 0.02f;

        _coloredBalls = new List<ColoredBall>();
        _blackBalls = new List<ColoredBall>();

        _targetScale = new Vector3(startScaleX, startScaleY, startScaleZ);
        transform.localScale = _targetScale;

        _identifier = 0f;
        _speedIncreaseScale = 1f;
    }

    public void IncreaseIdentifier()
    {
        _identifier++;
    }

    public void AddBlackBall(ColoredBall blackBall)
    {
        _blackBalls.Add(blackBall);
    }

    public void AddColoredBall(ColoredBall coloredBall)
    {
        _coloredBalls.Add(coloredBall);
    }

    public void RemoveColoredBall(ColoredBall coloredBall)
    {
        _coloredBalls.Remove(coloredBall);
    }

    public void IncreaseScale(Vector3 targetScale)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ReachTargetScale(targetScale));
    }

    private IEnumerator ReachTargetScale(Vector3 targetScale)
    {
        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, _speedIncreaseScale * Time.deltaTime);

            yield return null;
        }
    }
}
