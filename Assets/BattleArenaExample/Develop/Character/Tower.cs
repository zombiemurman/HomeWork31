using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDirectionalRotatable
{
    private TransformDirectionalRotater _rotator;

    [SerializeField] private float _rotationSpeed;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _rotator = new TransformDirectionalRotater(transform, _rotationSpeed);
    }

    private void Update()
    {
        _rotator.Upadate(Time.deltaTime);
    }

    public void SetRotateDirection(Vector3 inputDirection)
    {
        _rotator.SetInputDirection(inputDirection);
    }

}
