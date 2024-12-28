using UnityEngine;

namespace Baruah.HackNSlash
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        public readonly int MeleeSingle = Animator.StringToHash("MeleeSingle");
        public readonly int Hit = Animator.StringToHash("Hit");
        public readonly int Death = Animator.StringToHash("Death");
        public readonly int SpeedParam = Animator.StringToHash("speed");
        
        public Animator animator { get; private set; }
        
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void SetMovementSpeed(float speed)
        {
            animator.SetFloat(SpeedParam, speed);
        }

        public void PlayMeleeAnimation()
        {
            animator.Play(MeleeSingle);
        }

        public void HitAnimation(CharacterHealth health, int damage)
        {
            animator.Play(Hit);
        }
        
        public void DeadAnimation(CharacterHealth health)
        {
            animator.Play(Death);
        }
    }
}
