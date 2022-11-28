using System;
using TravisRFrench.Common.Runtime.Timing;
using UnityEngine;

namespace TravisRFrench.WeaponSystem.Runtime.Conditions
{
    public abstract class WeaponCooldown<TContext> : WeaponCondition
    {
        protected abstract IWeapon<TContext> Weapon { get; }
        protected abstract ICountdown Countdown { get; }

        public override bool Evaluate()
        {
            if (this.Countdown.IsRunning)
            {
                if (this.Countdown.Time <= 0f)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            
            return false;
        }

        protected virtual void OnEnable()
        {
            this.Weapon.Conditions.Register(this);
            
            this.Weapon.UseSucceeded += this.OnUseSucceeded;
            this.Weapon.UseFailed += this.OnUseFailed;
        }

        protected virtual void OnDisable()
        {
            this.Weapon.Conditions.Deregister(this);
            
            this.Weapon.UseSucceeded -= this.OnUseSucceeded;
            this.Weapon.UseFailed -= this.OnUseFailed;
        }

        protected virtual void Update()
        {
            this.Countdown.Tick(Time.deltaTime);
        }

        private void OnUseSucceeded(WeaponUseSuccessArgs<TContext> args)
        {
            this.Countdown.Reset();
            this.Countdown.Start();
        }
        
        private void OnUseFailed(WeaponUseFailedArgs<TContext> args)
        {
        }
    }
}
