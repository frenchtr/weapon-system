using System;

namespace TravisRFrench.WeaponSystem.Runtime.Triggers
{
    public abstract class SemiAutomaticWeaponTrigger<TContext> : WeaponTrigger<TContext>
    {
        public override event Action<TContext> Triggered;

        public override void Pull(TContext context = default(TContext))
        {
            base.Pull(context);
            
            this.Triggered?.Invoke(context);
        }
    }
}
