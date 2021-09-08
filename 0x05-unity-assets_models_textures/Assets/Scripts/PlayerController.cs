using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    ///<summary>Handles player movement</summary>
    public class PlayerController : MonoBehaviour
    {
        // Input actions
        public InputActionMap MovementMap;
        private InputAction _Move;
        private InputAction _Jump;

        // Camera for rotation
        public GameObject Camera;

        // Movement
        private Vector3 MoveVector;
        public float Speed = 10;

        // Jumping
        private bool IsGrounded = true;
        private float JumpSpeed = 10;
        private Vector3 JumpVector;


        // Setup
        void Start()
        {
            _Move = MovementMap.FindAction("Move");
            _Jump = MovementMap.FindAction("Jump");

            _Jump.performed += PerformJump;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void PerformJump(InputAction.CallbackContext context)
        {
            if (IsGrounded)
            {
                JumpVector = new Vector3(0, 10, 0);
                IsGrounded = false;
            }
        }

        private void OnEnable()
        {
            _Move.Enable();
            _Jump.Enable();
        }

        private void OnDisable()
        {
            _Move.Disable();
            _Jump.Disable();
        }
    }
}
