namespace TravisRFrench.WeaponsSystem
{
    public interface IWeapon<TContext>
    {
        TContext TriggerContext { get; }
    }
}
