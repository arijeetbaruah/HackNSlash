using DG.Tweening;
using UnityEngine;

namespace Baruah.HackNSlash.AbilitySystem.Skills
{
    [CreateAssetMenu(fileName = "Slash Attack", menuName = "Skills/Slash Attack")]
    public class SlashAttack : BaseSkill
    {
        public int baseDamage;

        public override void Execute(CharacterAbilitySystem users)
        {
            users.CharacterAnimation.PlayMeleeAnimation();
            users.waitForHit += OnHit;
            DOVirtual.DelayedCall(users.CharacterAnimation.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 10f, () =>
            {
                users.waitForHit -= OnHit;
            });
        }

        public override void OnHit(CharacterAbilitySystem users, CharacterAbilitySystem target)
        {
            int dmg = CalculatePhysicalDamage(users);
            target.Health.TakePhysicalDamage(baseDamage + dmg);
        }
    }
}
