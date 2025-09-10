using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

// Componente del player Input
[RequireComponent(typeof(PlayerControls))]

public class PlayerController : MonoBehaviour
{
   [Header("Referencias")]
   [Tooltip("Referencia a la cámara del jugador")]
   [SerializeField] private Camera playerCamera;
   
   [Header("Movements")]
   [Tooltip("Velocidad del jugador")]
   [SerializeField] private float speed = 2f;
   [Tooltip("Altura máxima de salto")]
   [SerializeField] private float jumpHeight = 0.3f;
   [Tooltip("Fuerza de gravedad")]
   [SerializeField] private float gravity = -9.81f;
  
   [Header("Look")]
   [SerializeField] private float mouseSensitivity = 50f;
   [SerializeField] private float gamePadSensitivity = 200f;
   [Tooltip("Límite de inclinación de mirada arriba")]
   [SerializeField] private float maxPitch = 80f;
   [Tooltip("Límite de inclinación de mirada abajo")]
   [SerializeField] private float minPitch = -80f;
  
   // Components
   private CharacterController characterController;
   private Transform cameraTransform;

   // Estados
   private Vector2 moveInput;
   private Vector2 lookInput;
   private float verticalVelocity;
   private float pitch;
   private Vector3 camStartLocalPos;
   private InputDevice lastLookDevice;
   
  
   // Input System
   private PlayerControls playerControls;


   private void Awake()
   {
       characterController = GetComponent<CharacterController>();
       cameraTransform = (playerCamera ?? Camera.main).transform;
      
       // instancia de la clase generada (Inputs)
       playerControls = new PlayerControls();
      
       // Bindings
       playerControls.Gameplay.Move.performed += OnMove;
       playerControls.Gameplay.Move.canceled += _ => moveInput = Vector2.zero;
       
       playerControls.Gameplay.Look.performed  += OnLook;
       playerControls.Gameplay.Look.canceled   += _ => lookInput = Vector2.zero;
        
       playerControls.Gameplay.Jump.performed  += _ => TryJump();
       
   }

   private void OnEnable()
   {
       playerControls.Gameplay.Enable();
   }

   private void OnDisable()
   {
       // Desuscribimos para evitar fugas
       playerControls.Gameplay.Move.performed  -= OnMove;
       playerControls.Gameplay.Look.performed  -= OnLook;
       playerControls.Gameplay.Jump.performed  -= _ => TryJump();
       playerControls.Gameplay.Disable();
   }
   void OnDestroy()
   {
       playerControls.Dispose();
   }

   private void OnMove(InputAction.CallbackContext ctx)
   {
       moveInput = ctx.ReadValue<Vector2>();
   }

   private void OnLook(InputAction.CallbackContext ctx)
   {
       lookInput       = ctx.ReadValue<Vector2>();
       lastLookDevice  = ctx.control.device;
   }

   private void TryJump()
   {
       if (characterController.isGrounded)
           verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
   }
   

   private void Update()
   {
       UpdateLook();
       UpdateMove();
   }


   private void UpdateLook()
   {
       // Selección de sensibilidad
       float sens = lastLookDevice is Gamepad
           ? gamePadSensitivity
           : mouseSensitivity;

       // Aplicamos dt siempre
       float dx = lookInput.x * sens * Time.deltaTime;
       float dy = lookInput.y * sens * Time.deltaTime;

       // Yaw + Pitch
       transform.Rotate(Vector3.up, dx);
       pitch = Mathf.Clamp(pitch - dy, minPitch, maxPitch);
       cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
   }


   private void UpdateMove()
   {
       // Movimiento 
       Vector3 dir = transform.right * moveInput.x + transform.forward * moveInput.y;

       // Salto y gravedad
       if (characterController.isGrounded && verticalVelocity < 0f)
           verticalVelocity = -0.5f;

       verticalVelocity += gravity * Time.deltaTime;
       dir.y = verticalVelocity;

       characterController.Move(dir * (speed * Time.deltaTime));
   }
}
