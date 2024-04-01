public interface IDamageable
{
    public int Health { get; }

    public void TakeDamage(int damageAmount);
    
    public delegate void OnTakeDamageHandler(int damageAmount);

    public event OnTakeDamageHandler OnTakeDamage;
}
