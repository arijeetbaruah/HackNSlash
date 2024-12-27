using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Baruah.HackNSlash.AbilitySystem.Skills
{
    public abstract class BaseSkill : ScriptableObject
    {
        public enum SkillType
        {
            Physical,
            Magical
        }

        public string skillID;
        public string skillName;
        [TextArea] public string description;
        public Sprite icon;
        public float cooldownTime;
        public float activateTime;

        public SkillType skillType;

        public abstract void Execute(CharacterAbilitySystem users);
        public abstract void OnHit(CharacterAbilitySystem users, CharacterAbilitySystem target);

        protected int CalculatePhysicalDamage(CharacterAbilitySystem users)
        {
            return users.CharacterStats.Stats.Attack * users.CharacterStats.CurrentLevel;
        }

        protected int CalculateMagicalDamage(CharacterAbilitySystem users)
        {
            return users.CharacterStats.Stats.MagicAttack * users.CharacterStats.CurrentLevel;
        }
    }
}
