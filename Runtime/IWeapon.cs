using System;
using System.Collections.Generic;
using TravisRFrench.Common.Runtime.Registration;
using TravisRFrench.WeaponSystem.Runtime.Conditions;

namespace TravisRFrench.WeaponSystem.Runtime
{
    public interface IWeapon<TContext>
    {
        TContext TriggerContext { get; }
        IRegistrar<IWeaponCondition> Conditions { get; }

        event Action<WeaponUseSuccessArgs<TContext>> UseSucceeded;
        event Action<WeaponUseFailedArgs<TContext>> UseFailed;
    }
}
