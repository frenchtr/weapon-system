using System;
using TravisRFrench.Common.Runtime.Timing;
using TravisRFrench.WeaponSystem.Runtime.Triggers;
using UnityEngine;

namespace TravisRFrench.WeaponSystem.Tests.Runtime.Tests.Runtime
{
    public class FullAutomaticWeaponTrigger : FullAutomaticWeaponTrigger<TriggerContext>
    {
        [SerializeField]
        private Interval interval;
        
        public override IInterval Interval => this.interval ??= new Interval();
    }
}
