using UnityEngine;

namespace TravisRFrench.WeaponSystem.Runtime.Conditions
{
    public abstract class WeaponCondition : MonoBehaviour, IWeaponCondition
    {
        public abstract bool Evaluate();
    }
}
