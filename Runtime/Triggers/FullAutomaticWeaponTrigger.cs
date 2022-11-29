using System;
using TravisRFrench.Common.Runtime.Timing;
using UnityEngine;

namespace TravisRFrench.WeaponSystem.Runtime.Triggers
{
    public abstract class FullAutomaticWeaponTrigger<TContext> : WeaponTrigger<TContext>
    {
        public abstract IInterval Interval { get; }
        protected TContext TriggerContext { get; set; }
        
        public override event Action<TContext> Triggered;

        public override void Pull(TContext context = default(TContext))
        {
            base.Pull(context);
            this.TriggerContext = context;
        }

        protected virtual void OnEnable()
        {
            this.Interval.Elapsed += this.OnElapsed;
        }

        private void OnDisable()
        {
            this.Interval.Elapsed -= this.OnElapsed;
        }

        private void Update()
        {
            if (this.IsHeld && !this.Interval.IsRunning)
            {
                this.Interval.Start();
            }
            
            this.Interval.Tick(Time.deltaTime);
        }
        
        private void OnElapsed(IInterval interval)
        {
            this.Triggered?.Invoke(this.TriggerContext);
            this.Interval.Reset();
        }
    }
}
