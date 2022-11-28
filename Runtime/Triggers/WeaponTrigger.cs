using System;
using UnityEngine;

namespace TravisRFrench.WeaponSystem.Runtime.Triggers
{
    public abstract class WeaponTrigger<TContext> : MonoBehaviour, IWeaponTrigger<TContext>
    {
        public bool IsHeld { get; protected set; }
        
        public abstract event Action<TContext> Triggered;
        
        public virtual void Pull(TContext context = default(TContext))
        {
            this.IsHeld = true;
        }

        public virtual void Release(TContext context = default(TContext))
        {
            this.IsHeld = false;
        }
    }
}
