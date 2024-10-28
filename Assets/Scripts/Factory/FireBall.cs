using System;
using UnityEngine;


public class FireBall : Projectile, IProjectile
{
    [field:SerializeField] public float Damage { get; set; }

    public void Initialize(float unitDamage, Action<float> targetHealth)
    {
        Damage = unitDamage;
        startPos = transform.position;
        this.targetHealth = null;
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
            GameObject explode = PoolManager.Instance.Get(PoolEnum.HitScan, "Explode", transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySFX(SoundType.Explode);
            explode.GetComponent<Explode>().Damage = Mathf.Round(Damage/2);
            gameObject.SetActive(false);
        }
    }
}