using System;
using UnityEngine;

namespace Baruah.HackNSlash
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        public readonly int MeleeSingle = Animator.StringToHash("MeleeSingle");
        public readonly int Hit = Animator.StringToHash("Hit");
        public readonly int Death = Animator.StringToHash("Death");
        
        public Animator animator { get; private set; }
        
        private void Start()
        {
            animator = GetComponent<Animator>();
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
