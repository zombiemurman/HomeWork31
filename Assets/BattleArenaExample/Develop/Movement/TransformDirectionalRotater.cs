using UnityEngine;

public class TransformDirectionalRotater : DirectionalRotator
{
    private Transform _transform;

    public TransformDirectionalRotater(Transform transform, float rotationSpeed) : base(rotationSpeed)
    {
        _transform = transform;
    }

    public override Quaternion CurrentRotation => _transform.rotation;

    protected override void ApplyRotation(Quaternion rotation)
    {
        _transform.rotation = rotation;
    }
}
