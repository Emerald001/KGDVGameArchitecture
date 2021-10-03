namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int _damage);
    }
    
    public interface IPoolable
    {
        void OnFixedUpdate();
        void OnEnableObject();
        void OnDisableObject();
    }
}