using System;
using Baruah.HackNSlash.Characters.Stats;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Baruah.HackNSlash
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterHealth : MonoBehaviour
    {
        public UnityEvent<CharacterHealth, int> OnDamage;
        public UnityEvent<CharacterHealth> OnDeath;
        
        [ShowInInspector] public int TotalHealth => CalculateHealth();
        [ShowInInspector] public int CurrentHealth => currentHealth;

        private int Constitution => characterStats == null ? 0 : characterStats.Stats.Constitution;
        private int CurrentLevel => characterStats == null ? 1 : characterStats.CurrentLevel;
        
        [SerializeField] private int baseHealth;

        private int currentHealth;
        private CharacterStats characterStats;

        private void Start()
        {
            TryGetComponent(out characterStats);
            
            currentHealth = TotalHealth;
        }

        public int CalculateHealth()
        {
            return baseHealth + Constitution * CurrentLevel;
        }

        public void TakePhysicalDamage(int damage)
        {
            damage -= characterStats.Stats.Defence;
            
            Debug.Log(characterStats.name + " takes " + damage + " Physical damage");
            TakeDamage(damage);
        }
        
        public void TakeMagicDamage(int damage)
        {
            damage -= characterStats.Stats.MagicDefence;
            
            Debug.Log(characterStats.name + " takes " + damage + " Magical damage");
            TakeDamage(damage);
        }

        private void TakeDamage(int damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);

            if (currentHealth == 0)
            {
                OnDeath?.Invoke(this);
            }
            else
            {
                OnDamage?.Invoke(this, damage);
            }
        }
    }
}
