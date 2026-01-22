using UnityEngine;

public interface IDirectionalRotatable : ITransformPosition
{
    Quaternion CurrentRotation { get; }

    void SetRotateDirection(Vector3 inputDirection);
}
