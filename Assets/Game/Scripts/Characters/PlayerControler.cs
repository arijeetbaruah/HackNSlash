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
        [SerializeField] private CharacterAnimation characterAnimation;
        [SerializeField] private BaseSkill primarySkill;
        [SerializeField] private float movementSpeed = 1.5f;
        [SerializeField] private float runningSpeed = 3f;
        [SerializeField] private float rotationSpeed = 1f;
        
        private InputManager IM => ServiceManager.Instance.GetService<InputManager>();

        private Vector2 p_movementDirection = Vector2.zero;
        
        private bool isRunning = false;
        
        private void OnEnable()
        {
            IM.Player.PrimaryAttack.performed += OnPrimaryAttack;
            IM.Player.Move.performed += OnMovement;
            IM.Player.Look.performed += OnLook;
            IM.Player.Sprint.started += OnSprintStart;
            IM.Player.Sprint.canceled += OnSprintEnd;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            IM.Player.PrimaryAttack.performed -= OnPrimaryAttack;
            IM.Player.Move.performed -= OnMovement;
            IM.Player.Look.performed -= OnLook;
            IM.Player.Sprint.started -= OnSprintStart;
            IM.Player.Sprint.canceled -= OnSprintEnd;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            Vector3 movementDirection = new Vector3(p_movementDirection.x, 0, p_movementDirection.y);
            transform.Translate(movementDirection * (isRunning ? runningSpeed : movementSpeed) * Time.deltaTime);
            var animationMovementSpeed = movementDirection.sqrMagnitude == 0 ? 0 : (isRunning ? 1 : 0.5f);
            characterAnimation.SetMovementSpeed(animationMovementSpeed);
        }

        private void OnSprintStart(InputAction.CallbackContext obj)
        {
            isRunning = true;
        }

        private void OnSprintEnd(InputAction.CallbackContext obj)
        {
            isRunning = false;
        }

        private void OnMovement(InputAction.CallbackContext obj)
        {
            p_movementDirection = obj.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext obj)
        {
            var rotationDirection = obj.ReadValue<Vector2>();
            transform.Rotate(Vector3.up, rotationDirection.x * rotationSpeed * Time.deltaTime);
        }

        private void OnPrimaryAttack(InputAction.CallbackContext obj)
        {
            abilitySystem.ExecuteSkill(primarySkill);
        }
    }
}
