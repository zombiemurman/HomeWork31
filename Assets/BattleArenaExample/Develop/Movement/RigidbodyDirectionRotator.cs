using UnityEngine;

public class RigidbodyDirectionRotator : DirectionalRotator
{
    private Rigidbody _rigidbody;

    public RigidbodyDirectionRotator(Rigidbody rigidbody, float rotationSpeed) : base(rotationSpeed)
    {
        _rigidbody = rigidbody;
    }

    public override Quaternion CurrentRotation => _rigidbody.rotation;

    protected override void ApplyRotation(Quaternion rotation)
    {
        _rigidbody.MoveRotation(rotation);
    }
}
