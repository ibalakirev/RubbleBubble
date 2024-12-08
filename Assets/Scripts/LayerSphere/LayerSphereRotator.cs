using UnityEngine;

public class LayerSphereRotator : MonoBehaviour
{
    private float _speedRotation = 10f;

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.localRotation *= Quaternion.Euler(_speedRotation * Time.fixedDeltaTime, 0, 0);
    }
}
