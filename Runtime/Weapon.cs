using System;
using System.Collections.Generic;
using TravisRFrench.Common.Runtime.Registration;
using TravisRFrench.WeaponSystem.Runtime.Conditions;
using TravisRFrench.WeaponSystem.Runtime.Triggers;
using UnityEngine;

namespace TravisRFrench.WeaponSystem.Runtime
{
    public abstract class Weapon<TContext> : MonoBehaviour, IWeapon<TContext>
    {
        private IRegistrar<IWeaponCondition> conditions;

        protected abstract IWeaponTrigger<TContext> Trigger { get; set; }
        public virtual IRegistrar<IWeaponCondition> Conditions => this.conditions ??= new Registrar<IWeaponCondition>();

        public TContext TriggerContext { get; private set; }

        public event Action<WeaponUseSuccessArgs<TContext>> UseSucceeded;
        public event Action<WeaponUseFailedArgs<TContext>> UseFailed;

        protected virtual void Awake()
        {
            this.Trigger ??= this.GetComponent<IWeaponTrigger<TContext>>();
        }

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
        
        
        private void TryUse()
        {
            foreach (var condition in this.Conditions.Entities)
            {
                if (!condition.Evaluate())
                {
                    var failureArgs = new WeaponUseFailedArgs<TContext>()
                    {
                        Weapon = this,
                        ResponsibleCondition = condition,
                    };
                    
                    this.UseFailed?.Invoke(failureArgs);
                    return;
                }
            }

            var successArgs = new WeaponUseSuccessArgs<TContext>()
            {
                Weapon = this,
            };
            
            this.UseSucceeded?.Invoke(successArgs);
        }

        private void OnTriggered(TContext context)
        {
            this.TriggerContext = context;
            this.TryUse();
        }
    }
}
