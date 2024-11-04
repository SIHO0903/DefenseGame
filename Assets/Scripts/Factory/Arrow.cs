using System;
using UnityEngine;


public class Arrow : Projectile, IProjectile
{
    public float Damage { get; set; }
    public string TargetString;

    public void Initialize(float unitDamage,string TargetString, Action<float> targetHealth)
    {
        Damage = unitDamage;
        startPos = transform.position;
        this.targetHealth = targetHealth;
        this.TargetString = TargetString;
    }

    public void Shoot(Vector3 targetPosition)
    {
        dirPos = targetPosition - startPos;
        dirPos.Normalize();
        spriteRenderer.flipX = dirPos.x < 0;
        rigid.AddForce(dirPos * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetString))
        {
            targetHealth.Invoke(Damage);
            gameObject.SetActive(false);
        }
    }
}
