using UnityEngine;

namespace Baruah.HackNSlash.Characters
{
    public class Projectile : CharacterAttackHitbox
    {
        public Vector3 direction;
        public float movementSpeed;

        public float destroyAfter;
        
        private float destroyTimer;

        private void Update()
        {
            transform.Translate(direction * movementSpeed * Time.deltaTime);
            
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyAfter)
            {
                Destroy(gameObject);
            }
        }
    }
}
