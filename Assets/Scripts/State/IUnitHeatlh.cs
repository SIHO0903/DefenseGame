public interface IUnitHeatlh
{
    public float Health { get; set; }
    public void Init();
    public void GetHit(float damage);
    public bool IsMaxHealth();
    public void GetHeal(float amount);
    public bool IsDead();
    public void Die();
}