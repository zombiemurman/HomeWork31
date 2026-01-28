
public class Health
{
    private float _health;

    public Health(float health)
    {
        _health = health;
    }

    public float CurrentHealth => _health;

    public void Remove(float damage)
    {
        if(damage < 0)
            return;

        _health -= damage;

        if(_health < 0)
            _health = 0;
    }

}

