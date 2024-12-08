using System;

public class ColoredBall : Ball
{
    private LayerSphere _currentLayerSphere;
    private float _identifierLayer;
    private bool _isCollision;
    private bool _isBlack;

    public event Action<ColoredBall> Released;

    public LayerSphere LayerSphere => _currentLayerSphere;
    public float IdentifierLayer => _identifierLayer;
    public bool IsCollision => _isCollision;
    public bool IsBlack => _isBlack;

    private void Awake()
    {
        _isCollision = false;
        _isBlack = false;
    }

    public void FallDown()
    {
        DisableKinematic();

        if (_currentLayerSphere != null)
        {
            _currentLayerSphere.RemoveColoredBall(this);
            transform.SetParent(null);
        }
    }

    public void SetLayerSphere(LayerSphere layerSphere)
    {
        _currentLayerSphere = layerSphere;
    }

    public void EnableIsCollision()
    {
        _isCollision = true;

        Released?.Invoke(this);
    }

    public void EnableIsBlack()
    {
        _isBlack = true;
    }
}
