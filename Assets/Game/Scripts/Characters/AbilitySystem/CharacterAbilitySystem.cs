using System;
using System.Collections.Generic;
using System.Linq;
using Baruah.HackNSlash.Characters.Stats;
using UnityEngine;

namespace Baruah.HackNSlash.AbilitySystem
{
    [RequireComponent(typeof(CharacterStats)), RequireComponent(typeof(CharacterHealth))]
    public class CharacterAbilitySystem : MonoBehaviour
    {
        public CharacterHealth Health => characterHealth;
        public CharacterStats CharacterStats => characterStats;
        public CharacterAnimation CharacterAnimation => characterAnimation;
        
        private CharacterStats characterStats;
        private CharacterHealth characterHealth;
        private CharacterAnimation characterAnimation;

        private Dictionary<string, Skills.BaseSkill> skills = new();
        private Dictionary<string, float> skillsCooldown = new();
        private Dictionary<string, float> skillsActivateCooldown = new();
        
        private Dictionary<string, Skills.BaseSkill> passiveSkill = new();

        public Action<CharacterAbilitySystem, CharacterAbilitySystem> waitForHit = delegate { };
        
        public Transform rangedAbilitySpawnPoint;
        
        private void Start()
        {
            TryGetComponent(out characterHealth);
            TryGetComponent(out characterStats);
            TryGetComponent(out characterAnimation);
        }

        private void Update()
        {
            UpdateCooldowns();
            ActivatePassiveSkill();
        }

        public void ExecuteSkill(Skills.BaseSkill skill)
        {
            if (skillsCooldown.TryGetValue(skill.skillID, out float cooldown) && cooldown > 0)
            {
                return;
            }
            
            skill.Execute(this);

            if (skill.cooldownTime > 0)
            {
                skillsCooldown.Add(skill.skillID, skill.cooldownTime);
            }
        }

        private void ActivatePassiveSkill()
        {
            foreach (var skill in passiveSkill)
            {
                if (!skillsCooldown.ContainsKey(skill.Key))
                {
                    ActivateSkill(skill.Value);
                }
            }
        }

        private void ActivateSkill(Skills.BaseSkill skill)
        {
            if (skill.activateTime == 0)
            {
                skillsCooldown.Add(skill.skillID, skill.cooldownTime);
            }
            else
            {
                if (skillsActivateCooldown.ContainsKey(skill.skillID))
                {
                    skillsActivateCooldown[skill.skillID] -= Time.deltaTime;
                    if (skillsActivateCooldown[skill.skillID] <= 0)
                    {
                        skillsActivateCooldown.Remove(skill.skillID);
                    }
                }
                else
                {
                    skillsActivateCooldown.Add(skill.skillID, skill.activateTime);
                }
                
                passiveSkill[skill.skillID].Execute(this);
            }
        }

        private void UpdateCooldowns()
        {
            foreach (var skill in skillsCooldown.Keys.ToArray())
            {
                skillsCooldown[skill] -= Time.deltaTime;
                if (skillsCooldown[skill] <= 0)
                {
                    skillsCooldown.Remove(skill);
                }
            }
        }

        public void OnHit(GameObject target, string hitbox)
        {
            if (target != gameObject)
                waitForHit.Invoke(this, target.GetComponent<CharacterAbilitySystem>());
        }
    }
}
