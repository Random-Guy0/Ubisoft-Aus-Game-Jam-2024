namespace Jam.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(int damageAmount);

        public delegate void OnTakeDamageHandler(int damageAmount);

        public event OnTakeDamageHandler OnTakeDamage;
    }
}
