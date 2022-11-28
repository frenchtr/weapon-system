using TravisRFrench.WeaponSystem.Runtime.Conditions;

namespace TravisRFrench.WeaponSystem.Runtime
{
    public class WeaponUseFailedArgs<TContext> : WeaponUseArgs<TContext>
    {
        public IWeaponCondition ResponsibleCondition { get; set; }
    }
}
