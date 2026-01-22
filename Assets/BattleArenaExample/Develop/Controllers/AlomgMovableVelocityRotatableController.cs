using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlomgMovableVelocityRotatableController : Controller
{
    private IDirectionalRotatable _rotatable;
    private IDirectionalMovable _movable;

    public AlomgMovableVelocityRotatableController(IDirectionalRotatable rotatable, IDirectionalMovable movable)
    {
        _rotatable = rotatable;
        _movable = movable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotateDirection(_movable.CurrentVelocity);
    }
}
