using Baruah.HackNSlash.Characters;
using DG.Tweening;
using UnityEngine;

namespace Baruah.HackNSlash.AbilitySystem.Skills
{
    [CreateAssetMenu(fileName = "Cast Spell", menuName = "Skills/Cast Spell")]
    public class CastSpell : BaseSkill
    {
        public int baseDamage;
        public Projectile prefab;

        public override void Execute(CharacterAbilitySystem users)
        {
            users.CharacterAnimation.CastSpell();
            //users.waitForHit += OnHit;
            DOVirtual.DelayedCall(users.CharacterAnimation.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 10f - 2f, () =>
            {
                var instance = Instantiate(prefab, users.rangedAbilitySpawnPoint.position, Quaternion.identity);

                instance.user = users;
                instance.direction = users.transform.forward;
                instance.OnHit.AddListener((GameObject target, string hitbox) =>
                {
                    OnHit(users, target.GetComponent<CharacterAbilitySystem>());
                    
                    Destroy(instance.gameObject);
                });
                
                users.waitForHit -= OnHit;
            });
        }

        public override void OnHit(CharacterAbilitySystem users, CharacterAbilitySystem target)
        {
            int dmg = CalculateMagicalDamage(users);
            target.Health.TakePhysicalDamage(baseDamage + dmg);
        }
    }
}
