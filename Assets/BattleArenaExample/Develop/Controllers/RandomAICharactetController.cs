using UnityEngine;

public class RandomAICharactetController : Controller
{
    private Character _character;

    private float _time;
    private float _timeToChangeDirection;

    private Vector3 _inputDirection;

    public RandomAICharactetController(Character character, float timeToChangeDirection)
    {
        _character = character;
        _timeToChangeDirection = timeToChangeDirection;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _time += Time.deltaTime;

        if (_time >= _timeToChangeDirection)
        {
            _inputDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            _time = 0;
        }

        _character.SetMoveDirection(_inputDirection);
        _character.SetRotateDirection(_inputDirection);
    }
}
