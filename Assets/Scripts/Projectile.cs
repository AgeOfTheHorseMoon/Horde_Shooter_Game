using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Projectile : MonoBehaviour
{
    int _damage;
    Rigidbody2D _rigidbody2D;

    public void Setup(Vector3 shootDir,int damage, float projectileSpeed, float projectileLifeTime)
    {
        _damage = damage;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(shootDir * projectileSpeed, ForceMode2D.Impulse);

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(shootDir));
        Destroy(gameObject, projectileLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damagableObj))
        {
            //Debug.Log("Projectile hit to:" + damagableObj + " Damage: " + _damage);
            damagableObj.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
