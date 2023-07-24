using System;
using UnityEngine;

namespace SpaceInvader
{
    public class PlayerShip : Ship
    {
    
        [SerializeField] private float _turnSmoothTime = 0.1f;
        [SerializeField] private Transform _MainCamera;
        [SerializeField] private GameObject gunTip;
        [SerializeField] private GameObject _projectile;
        
        private CharacterController _controller;
        private float trunSmoothVelocity;
        public Action PlayerTakeDamage;
        
        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<CharacterController>();
            _MainCamera = Camera.main.transform;
        }

        private void Start()
        {
            Debug.Log("Player health: " + Health);
        }

        // Update is called once per frame
        void Update()
        {
            move();
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                shoot();
            }
        }

        private void shoot()
        {
            // Attack stuff
            Rigidbody rb = Instantiate(_projectile, gunTip.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        }

        public void takeDamage(int amount)
        {
            PlayerTakeDamage?.Invoke();
            TakeDamage(amount);
        }
        
        private void move()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
        
            Vector3 direction = new Vector3(horizontal, vertical, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // rotate player to face the direction of travel based on Camera and inputAxis
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _MainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref trunSmoothVelocity,_turnSmoothTime);
            
                // face where you are going
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
                _controller.Move(moveDir.normalized * MovmentSpeed * Time.deltaTime);
            }
        }
    
    }

}
