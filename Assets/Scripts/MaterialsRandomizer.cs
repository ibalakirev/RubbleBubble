using System.Collections.Generic;
using UnityEngine;

public class MaterialsRandomizer : MonoBehaviour
{
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _yellowMaterial;
    [SerializeField] private Material _blackMaterial;

    private List<Material> _materialsColoredBalls;

    public Material BlackMaterial => _blackMaterial;
    public List<Material> MaterialsColoredBalls => _materialsColoredBalls;

    private void Awake()
    {
        _materialsColoredBalls = new List<Material>()
        {
            _redMaterial,
            _greenMaterial,
            _yellowMaterial
        };
    }

    public Material GetRandomMaterialColoredBall()
    {
        float valueProbabilityBlackMaterial = 10;

        float minValueProbability = 0;
        float maxValueProbability = 100;

        float minIndexColor = 0f;
        float maxIndexColor = _materialsColoredBalls.Count;

        float randomMaterial = Random.Range(minIndexColor, maxIndexColor);
        float randomProbability = Random.Range(minValueProbability, maxValueProbability);

        if (randomProbability <= valueProbabilityBlackMaterial)
        {
            return _blackMaterial;
        }
        else
        {
            return GetMaterial(randomMaterial);
        }
    }

    public Material GetRandomMaterialProjectile()
    {
        float minValueColor = 0f;
        float maxValueColor = _materialsColoredBalls.Count;

        float randomColor = Random.Range(minValueColor, maxValueColor);

        return GetMaterial(randomColor);
    }

    private Material GetMaterial(float index)
    {
        return _materialsColoredBalls[(int)index];
    }
}

