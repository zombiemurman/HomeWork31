using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Bullet : MonoBehaviour, IDisposable
{
    private float _speed;

    private float _lifeTime;

    private Vector3 _direction;

    private float _damage;

    private void Update()
    {
         transform.Translate(_direction * _speed * Time.deltaTime);

        _lifeTime -= Time.deltaTime;

        if (_lifeTime < 0)
            Dispose();
    }

    private void OnTriggerEnter(Collider other)
    {
        ITakingDamage objectDamage = other.GetComponent<ITakingDamage>();

        objectDamage?.TakeDamage(_damage);
    }

    public void Initialize(Vector3 direction, float speed, float lifeTime, float damage)
    {
        _speed = speed;

        _direction = direction;

        _lifeTime = lifeTime;

        _damage = damage;
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}

