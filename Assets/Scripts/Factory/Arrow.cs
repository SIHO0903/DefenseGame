using System;
using UnityEngine;


public class Arrow : Projectile, IProjectile
{
    //화살 로직
    public float Damage { get; set; }
    public void Initialize(float unitDamage, Action<float> targetHealth)
    {
        Damage = unitDamage;
        startPos = transform.position;
        this.targetHealth = targetHealth;
    }

    public void Shoot(Vector3 targetPosition)
    {
        dirPos = targetPosition - startPos;
        dirPos.Normalize();

        rigid.AddForce(dirPos * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targetHealth.Invoke(Damage);
            gameObject.SetActive(false);
        }
    }
}
