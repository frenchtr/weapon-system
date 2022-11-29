using System.Collections;
using NUnit.Framework;
using TravisRFrench.Common.Runtime.Timing;
using TravisRFrench.WeaponSystem.Runtime;
using UnityEngine;
using UnityEngine.TestTools;

namespace TravisRFrench.WeaponSystem.Tests.Runtime.Tests.Runtime
{
    [TestFixture]
    [Category(RuntimeTestCategories.ComponentTests)]
    public class FullAutomaticWeaponTriggerTests
    {
        private Weapon weapon;
        private FullAutomaticWeaponTrigger trigger;
        
        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject();
            this.trigger = gameObject.AddComponent<FullAutomaticWeaponTrigger>();
            this.weapon = gameObject.AddComponent<Weapon>();
        }
        
        [UnityTest]
        public IEnumerator GivenTriggerWithSpecifiedInterval_WhenPulled_ItShouldFireAfterThatIntervalHasElapsed()
        {
            // GIVEN
            var duration = 0.5f;

            ((Interval)this.trigger.Interval).Duration = duration;
            
            var wasTriggered = false;
            
            void OnTriggered(TriggerContext context)
            {
                wasTriggered = true;
            }

            yield return null;

            // WHEN
            this.trigger.Triggered += OnTriggered;
            this.trigger.Pull();
            
            // THEN
            yield return new WaitForSeconds(duration);
            yield return null;
            this.trigger.Release();
            this.trigger.Triggered -= OnTriggered;
            Assert.IsTrue(wasTriggered);
        }

        [UnityTest]
        public IEnumerator GivenTriggerWithSpecifiedInterval_WhenPulled_ItShouldNotFireBeforeThatIntervalHasElapsed()
        {
            // GIVEN
            var duration = 0.5f;

            ((Interval)this.trigger.Interval).Duration = duration;
            
            var wasTriggered = false;

            void OnTriggered(TriggerContext context)
            {
                wasTriggered = true;
            }

            yield return null;

            // WHEN
            this.trigger.Triggered += OnTriggered;
            this.trigger.Pull();
            
            // THEN
            yield return new WaitForSeconds(duration);
            Assert.IsFalse(wasTriggered);
            yield return null;
            this.trigger.Release();
            this.trigger.Triggered -= OnTriggered;
        }

        [UnityTest]
        public IEnumerator GivenTrigger_WhenPulledWithSpecificTriggerContext_ItShouldReturnThatSameContext()
        {
            // GIVEN
            var duration = 0.5f;
            ((Interval)this.trigger.Interval).Duration = duration;
            var expected = new TriggerContext()
            {
                String = "foo",
            };
            TriggerContext actual = default;
            
            void OnTriggered(TriggerContext context)
            {
                actual = context;
            }
            
            // WHEN
            this.trigger.Triggered += OnTriggered;
            this.trigger.Pull(expected);
            
            // THEN
            yield return new WaitForSeconds(duration);
            yield return null;
            this.trigger.Release();
            this.trigger.Triggered -= OnTriggered;
            
            Assert.AreEqual(expected, actual);
        }
    }
}
