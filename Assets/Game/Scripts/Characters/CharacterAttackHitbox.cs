using System;
using Baruah.HackNSlash.AbilitySystem;
using UnityEngine;
using UnityEngine.Events;

namespace Baruah.HackNSlash.Characters
{
    public class CharacterAttackHitbox : MonoBehaviour
    {
        public CharacterAbilitySystem user;
        public UnityEvent<GameObject, string> OnHit;

        private void Start()
        {
            if (user == null)
            {
                user = transform.parent.GetComponent<CharacterAbilitySystem>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (user.transform != other.transform)
            {
                OnHit?.Invoke(other.gameObject, gameObject.name);
            }
        }
    }
}
