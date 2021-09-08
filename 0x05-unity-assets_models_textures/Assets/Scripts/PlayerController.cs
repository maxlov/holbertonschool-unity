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
        public InputActionAsset MovementMap;
        private InputAction _Move;
        private InputAction _Jump;

        // Camera for rotation
        public Transform Camera;

        /// Character controller
        private CharacterController controller;
        private CapsuleCollider capsule;

        // Movement
        private Vector3 HorizontalVelocity;
        public float Speed = 10f;
        private float TurnSpeed = 0.1f;
        private float TurnVelocity;

        // Jumping
        private Vector3 VerticalVelocity;
        public float JumpSpeed = 10f;
        public float Gravity = -10f;
        private bool _Grounded;

        // Setup
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            controller = gameObject.GetComponent<CharacterController>();
            capsule = gameObject.GetComponent<CapsuleCollider>();

            _Move = MovementMap.FindAction("Movement");
            _Jump = MovementMap.FindAction("Jump");

            Enable();
        }

        // Update
        void Update()
        {
            _Grounded = GroundCheck();
            if (_Grounded && VerticalVelocity.y < 0f)
                VerticalVelocity.y = -2f;
            MoveHorizontal();
            MoveVertical();
        }

        // Handle movement input
        private void MoveHorizontal()
        {
            Vector2 MoveInput = _Move.ReadValue<Vector2>();

            HorizontalVelocity = new Vector3(MoveInput.x, 0f, MoveInput.y).normalized;

            if (HorizontalVelocity.magnitude > 0)
            {
                float targetAngle = Mathf.Atan2(HorizontalVelocity.x, HorizontalVelocity.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnVelocity, TurnSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * Speed * Time.deltaTime);
            }
        }

        // Handle vertical velocity
        private void MoveVertical()
        {
            if (_Jump.triggered && _Grounded)
                VerticalVelocity.y += Mathf.Sqrt(JumpSpeed * -3.0f * Gravity);
            VerticalVelocity.y += Gravity * Time.deltaTime;
            controller.Move(VerticalVelocity * Time.deltaTime);
            
        }

        private bool GroundCheck()
        {
            Vector3 SphereCenter;
            int layerMask = ~LayerMask.GetMask("Player");

            SphereCenter = transform.position - new Vector3(0, capsule.height / 2, 0);

            if (Physics.CheckSphere(SphereCenter, .4f, layerMask))
                return true;
            return false;
        }


        private void Enable()
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
