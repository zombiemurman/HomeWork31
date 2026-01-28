using UnityEngine;

public class Shooting
{
    private Bullet _prefab;

    private float _speed;

    private float _lifeTime;

    private float _damage;

    public Shooting(Bullet prefab, float speed, float lifeTime, float damage)
    {
        _prefab = prefab;
        _speed = speed;
        _lifeTime = lifeTime;
        _damage = damage;
    }

    public void Shoot(Vector3 direction, Vector3 position)
    {
        Bullet bullet = Object.Instantiate(_prefab, position, Quaternion.identity, null);
        bullet.Initialize(direction, _speed, _lifeTime, _damage);
    }
}

