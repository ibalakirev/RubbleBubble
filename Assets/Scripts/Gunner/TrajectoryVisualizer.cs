using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryVisualizer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        float divider = 2;
        float timeMultiplier = 0.1f;
        float minQuantityPoints = 0f;
        int indexPoint = 1;
        int quantityPoints = 40;


        Vector3[] points = new Vector3[quantityPoints];

        _lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * timeMultiplier;

            points[i] = origin + speed * time + Physics.gravity * time * time / divider;

            if (points[i].y < minQuantityPoints)
            {
                _lineRenderer.positionCount = i + indexPoint;

                break;
            }
        }

        _lineRenderer.SetPositions(points);
    }

    public void EnableLinear()
    {
        _lineRenderer.gameObject.SetActive(true);
    }

    public void DisableLinear()
    {
        _lineRenderer.gameObject.SetActive(false);
    }

    public void SetColor(Color color)
    {
        _lineRenderer.startColor = color;
    }
}
