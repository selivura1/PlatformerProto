namespace Selivura
{
    public class Killzone : Trigger
    {
        protected override void OnTriggeredDamageable(IDamageable damageable)
        {
            damageable.TakeDamage(99999);
        }
    }
}
