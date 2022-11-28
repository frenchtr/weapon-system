using TravisRFrench.WeaponSystem.Runtime.Triggers;
using UnityEngine;

namespace TravisRFrench.WeaponSystem.Runtime
{
    public abstract class Weapon<TContext> : MonoBehaviour, IWeapon<TContext>
    {
        protected abstract IWeaponTrigger<TContext> Trigger { get; set; }
        
        public TContext TriggerContext { get; private set; }

        protected virtual void OnEnable()
        {
            this.Trigger.Triggered += this.OnTriggered;
        }

        protected virtual void OnDisable()
        {
            this.Trigger.Triggered -= this.OnTriggered;
        }

        protected virtual void Reset()
        {
            this.Trigger = this.GetComponent<IWeaponTrigger<TContext>>();
        }

        private void OnTriggered(TContext context)
        {
            this.TriggerContext = context;
            
            
        }
    }
}
