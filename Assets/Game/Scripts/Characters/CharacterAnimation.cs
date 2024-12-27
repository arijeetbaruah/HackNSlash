using System;
using UnityEngine;

namespace Baruah.HackNSlash
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        public readonly int MeleeSingle = Animator.StringToHash("MeleeSingle");
        
        public Animator animator { get; private set; }
        
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayMeleeAnimation()
        {
            animator.Play(MeleeSingle);
        }
    }
}
