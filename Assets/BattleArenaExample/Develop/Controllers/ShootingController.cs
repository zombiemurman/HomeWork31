
using UnityEngine;


public class ShootingController : Controller
{
    IShooting _shooting;

    public ShootingController(IShooting shooting)
    {
        _shooting = shooting;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _shooting.Shoot(Vector3.zero);
        }
    }
}

