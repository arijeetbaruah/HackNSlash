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

        protected void Start()
        {
            if (user == null)
            {
                user = transform.parent.GetComponent<CharacterAbilitySystem>();
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (user.transform != other.transform)
            {
                OnHit?.Invoke(other.gameObject, gameObject.name);
            }
        }
    }
}
