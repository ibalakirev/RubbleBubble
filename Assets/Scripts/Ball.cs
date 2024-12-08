using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(Renderer))]
public abstract class Ball : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Renderer _renderer;
    private MeshRenderer _meshRenderer;
    private Color _color;

    protected Rigidbody Rigidbody => _rigidBody;
    public Color Color => _color;
    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();
        _rigidBody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _rigidBody.isKinematic = true;
    }

    public void SetMaterial(Material material)
    {
        _renderer.material = material;

        SetColor(_meshRenderer.material.color);
    }

    protected void DisableKinematic()
    {
        _rigidBody.isKinematic = false;
    }

    private void SetColor(Color color)
    {
        _color = color;
    }
}
