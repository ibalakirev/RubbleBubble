using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bullet : Ball
{
    private Vector3 _force;

    public event Action<Bullet> Released;

    public void ReportRelease()
    {
        Released?.Invoke(this);
    }

    public void SetForce(Vector3 force)
    {
        _force = force;
    }

    public void UseForce()
    {
        if (_force != null)
        {
            DisableKinematic();

            Rigidbody.AddForce(_force, ForceMode.VelocityChange);
        }
    }
}
