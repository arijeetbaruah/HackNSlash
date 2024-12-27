using System;
using Baruah.HackNSlash.AbilitySystem;
using Baruah.HackNSlash.AbilitySystem.Skills;
using Baruah.HackNSlash.Input;
using Baruah.HackNSlash.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Baruah.HackNSlash
{
    public class PlayerControler : MonoBehaviour
    {
        [SerializeField] private CharacterAbilitySystem abilitySystem;
        [SerializeField] private BaseSkill primarySkill;
        [SerializeField] private float movementSpeed = 1.5f;
        [SerializeField] private float rotationSpeed = 1f;
        
        private InputManager IM => ServiceManager.Instance.GetService<InputManager>();

        private Vector2 p_movementDirection = Vector2.zero;
        private Vector2 p_rotationDirection = Vector2.zero;
        
        private void OnEnable()
        {
            IM.Player.PrimaryAttack.performed += OnPrimaryAttack;
            IM.Player.Move.performed += OnMovement;
            IM.Player.Look.performed += OnLook;
        }

        private void OnDisable()
        {
            IM.Player.PrimaryAttack.performed -= OnPrimaryAttack;
            IM.Player.Move.performed -= OnMovement;
            IM.Player.Look.performed -= OnLook;
        }

        private void Update()
        {
            Vector3 movementDirection = new Vector3(p_movementDirection.x, 0, p_movementDirection.y);
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
            
            transform.Rotate(Vector3.up, p_rotationDirection.x * rotationSpeed * Time.deltaTime);
        }

        private void OnMovement(InputAction.CallbackContext obj)
        {
            p_movementDirection = obj.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext obj)
        {
            p_rotationDirection = obj.ReadValue<Vector2>();
        }

        private void OnPrimaryAttack(InputAction.CallbackContext obj)
        {
            abilitySystem.ExecuteSkill(primarySkill);
        }
    }
}
