using System;
using UnityEngine;


public class FireBall : Projectile, IProjectile
{
    [field:SerializeField] public float Damage { get; set; }
    public string targetString;

    public void Initialize(float unitDamage,string targetString, Action<float> targetHealth)
    {
        Damage = unitDamage;
        startPos = transform.position;
        this.targetHealth = targetHealth;
        this.targetString = targetString;
    }
    public void Shoot(Vector3 targetPosition)
    {
        dirPos = targetPosition - startPos;
        dirPos.Normalize();

        rigid.AddForce(dirPos * speed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetString))
        {
            targetHealth.Invoke(Damage);
            GameObject explode = PoolManager.Instance.Get(PoolEnum.HitScan, "Explode", transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySFX(SoundType.Explode);
            explode.GetComponent<Explode>().Damage = Mathf.Round(Damage/2);
            gameObject.SetActive(false);
        }
    }
}