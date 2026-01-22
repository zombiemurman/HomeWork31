using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerDirectionalRotatableController : Controller
{
    private IDirectionalRotatable _rotatable;

    public PlayerDirectionalRotatableController(IDirectionalRotatable rotatable)
    {
        _rotatable = rotatable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        _rotatable.SetRotateDirection(inputDirection);
    }
}
