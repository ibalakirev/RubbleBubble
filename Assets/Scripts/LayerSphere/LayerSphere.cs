using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
public class LayerSphere : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private Vector3[] _spawnPointsPositionsColoredBalls;
    private List<ColoredBall> _coloredBalls;
    private List<ColoredBall> _blackBalls;
    private Coroutine _coroutine;
    private float _identifier;
    private float _speedIncreaseScale;

    public float Identifier => _identifier;
    public Vector3[] SpawnPointsPositionsColoredBalls => _spawnPointsPositionsColoredBalls;
    public List<ColoredBall> ColoredBalls => _coloredBalls;
    public List<ColoredBall> BlackBalls => _blackBalls;

    private void OnEnable()
    {
        float startScaleX = 0.02f;
        float startScaleY = 0.02f;
        float startScaleZ = 0.02f;

        _meshFilter = GetComponent<MeshFilter>();
        _coloredBalls = new List<ColoredBall>();
        _blackBalls = new List<ColoredBall>();

        Vector3 defaultScale = new Vector3(startScaleX, startScaleY, startScaleZ);

        SetDefaultScale(defaultScale);

        _identifier = 0f;
        _speedIncreaseScale = 1f;

        SetSpawnPointsPositionsColoredBalls(_meshFilter.mesh.vertices);
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

    private void SetDefaultScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private void SetSpawnPointsPositionsColoredBalls(Vector3[] spawnPoints)
    {
        _spawnPointsPositionsColoredBalls = spawnPoints.Distinct().ToArray();
    }
}
