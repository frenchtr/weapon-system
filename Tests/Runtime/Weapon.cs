using TravisRFrench.WeaponSystem.Runtime;
using TravisRFrench.WeaponSystem.Runtime.Triggers;

namespace TravisRFrench.WeaponSystem.Tests.Runtime.Tests.Runtime
{
    public class Weapon : Weapon<TriggerContext>
    {
        protected override IWeaponTrigger<TriggerContext> Trigger { get; set; }
    }
}
