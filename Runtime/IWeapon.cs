namespace TravisRFrench.WeaponSystem.Runtime
{
    public interface IWeapon<TContext>
    {
        TContext TriggerContext { get; }
    }
}
