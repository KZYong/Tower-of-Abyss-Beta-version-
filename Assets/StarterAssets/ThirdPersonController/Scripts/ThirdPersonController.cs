using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using TMPro;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;
        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;
        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;
        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

        private PlayerInput _playerInput;

        public GameObject effect1;

        public int combo;
        public bool isAttack;
        public bool isJumpAttack;
        public bool isGuard;
        public bool isHit;
        public bool isHitAnim;
        public bool isDamage;
        public bool isThirdHit;
        public bool isSkill;
        public bool isSkillHit;

        public bool CursorLocked;

        public bool GuardLock;

        public bool LockAction;

        public float CursorTimer;

        public GameObject slashE1;
        public GameObject slashE2;
        public GameObject slashE3;
        public GameObject slashE4;
        public GameObject slashE5;

        public GameObject player;
        public GameObject closeEnemy;

        public bool Skilled;
        public GameObject Skill_Ready;
        public GameObject Skill_CD;
        public TextMeshProUGUI Skill_CDText;
        public float SkillCDTimer;

        public float targetSpeed;

        PlayerStats PlayerS;
        ScanNearestEnemy NearestEnemy;

        public bool PlayerDeath;
        public bool DeathAnim;

        private CountEnemy ECounter;

        public GameObject MenuPanel;
        public GameObject MenuList;
        public GameObject StatList;
        private Animator MenuPanelAnim;
        private Animator MenuListAnim;
        private Animator StatListAnim;
        public bool MenuOpened;
        public bool MenuDone;
        public GameObject ConfirmPanel;
        public GameObject ConfirmPanel2;

        public AudioSource BGM1;
        public AudioSource BGM2;

        public bool canChest;
        public GameObject InteractUI;
        public GameObject TheChest;
        ActivateChest Chest;

        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            MenuPanelAnim = MenuPanel.GetComponent<Animator>();
            MenuListAnim = MenuList.GetComponent<Animator>();
            StatListAnim = StatList.GetComponent<Animator>();
        }

        private void Start()
        {
            ECounter = FindObjectOfType<CountEnemy>();
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            _playerInput = GetComponent<PlayerInput>();

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

            PlayerS = FindObjectOfType<PlayerStats>();
            NearestEnemy = FindObjectOfType<ScanNearestEnemy>();

            CursorLocked = true;
        }

        private void Update()
        {
            if (Skilled)
            {
                Skill_Ready.SetActive(false);
                Skill_CD.SetActive(true);
                Skill_CDText.text = SkillCDTimer.ToString("F0");
                SkillCDTimer -= Time.deltaTime;
                if (SkillCDTimer <= 0)
                {
                    Skilled = false;
                }
            }

            if (!Skilled)
            {
                Skill_Ready.SetActive(true);
                Skill_CD.SetActive(false);
                SkillCDTimer = 20;
            }



            if (!LockAction)
                Cursor.lockState = CursorLockMode.Locked;

            if (PlayerS.Health <= 0 && DeathAnim == false)
            {
                PlayerDeath = true;
                _animator.SetTrigger("Death");
                DeathAnim = true;
            }

            if (PlayerDeath)
            {
                LockAction = true;
                LockCameraPosition = true;
                Cursor.lockState = CursorLockMode.None;
            }

            CursorTimer += Time.deltaTime;

            if (_playerInput.actions["CursorLock"].ReadValue<float>() == 1f)
                if (CursorLocked && CursorTimer > 1)
                {
                    Cursor.lockState = CursorLockMode.None;
                    CursorLocked = false;
                    CursorTimer = 0;
                    
                    LockCameraPosition = true;
                    LockAction = true;
                }

            if (_playerInput.actions["CursorLock"].ReadValue<float>() == 1f)
                if (!CursorLocked && CursorTimer > 1)
                {
                    CursorLocked = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    CursorTimer = 0;
                    LockCameraPosition = false;
                    LockAction = false;
                }

            if (_playerInput.actions["Menu"].ReadValue<float>() == 1f && MenuOpened == false)
                if (CursorTimer > 0.5)
                {
                    Cursor.lockState = CursorLockMode.None;
                    CursorLocked = false;
                    CursorTimer = 0;
                    LockCameraPosition = true;
                    LockAction = true;
                    MenuPanel.SetActive(true);
                    MenuList.SetActive(true);
                    StatList.SetActive(true);
                    MenuPanelAnim.Play("MenuPanelAnim");
                    MenuListAnim.Play("MenuAnim");
                    StatListAnim.Play("UISlideDown");

                    BGM1.volume = BGM1.volume / 3;
                    BGM2.volume = BGM2.volume / 3;

                    MenuOpened = true;
                }

            if (MenuOpened)
            {
                if (CursorTimer > 0.3)
                {
                    MenuDone = true;
                    
                    Time.timeScale = 0;
                   
                }
            }

            if (_playerInput.actions["Menu"].ReadValue<float>() == 1f && MenuDone == true)
                {
                    ResumeGame();
                    MenuOpened = false;
                }


            _hasAnimator = TryGetComponent(out _animator);

            closeEnemy = NearestEnemy.closestEnemy;

            GroundedCheck();  

            if (isGuard == false && isHit == false && PlayerDeath == false)
            {
                Move();
                JumpAndGravity();
            }

            if (PlayerS.Stamina <= 0f)
            {
                GuardLock = true;
            }

            if (PlayerS.Stamina >= 15f)
            {
                GuardLock = false;
            }

            if (canChest == true)
                InteractUI.SetActive(true);
            if (canChest == false)
                InteractUI.SetActive(false);

            if (_playerInput.actions["Interact"].ReadValue<float>() == 1f && !isAttack && isHit == false && !isSkill && !LockAction)
            {
                if (canChest == true)
                {
                    Chest = TheChest.GetComponent<ActivateChest>();
                    Chest._open = true;
                }
            }

            if (_playerInput.actions["Attack"].ReadValue<float>() == 1f && !isAttack && isHit == false && !isSkill && !LockAction)
            {
                if (Grounded == true)
                {
                    isAttack = true;
                    _animator.SetTrigger("" + combo);

                    //_animator.Play("Attack");
                }

                if (Grounded == false)
                {
                    isAttack = true;
                    _animator.Play("JumpAttack");
                    //_verticalVelocity = Mathf.Sqrt(JumpHeight * -0.1f * Gravity);
                    //_verticalVelocity += 10 * Gravity * Time.deltaTime;
                    combo = 0;
                }

                //GameObject eObject = Instantiate(effect1, transform.position, Quaternion.identity) as GameObject;
                //Destroy(eObject, 3);
            }


            if (_playerInput.actions["Skill1"].ReadValue<float>() == 1f && isHit == false && !LockAction && !Skilled)
            {
                if (Grounded == true)
                {
                    isAttack = true;
                    _animator.Play("Attack3");
                    Skilled = true;
                }
            }

            if (_playerInput.actions["Parry"].ReadValue<float>() >= 1f && isHit == false && GuardLock == false && !LockAction && !isSkill)
            {
                if (Grounded == true && PlayerS.Stamina >= 0f)
                {
                    combo = 0;

                    if (isAttack == true)
                    {
                        isAttack = false;
                    }

                    isAttack = false;
                    isSkill = false;
                    isGuard = true;
                    _animator.Play("Guard");

                    PlayerS.Stamina = PlayerS.Stamina - 0.5f;
                }
            }
            else
            {
                EndGuard();
            }

            if (_playerInput.actions["Sprint"].ReadValue<float>() >= 1f)
            {
                if (isHit == false && isGuard == false && isAttack == false && !LockAction)
                    PlayerS.Stamina = PlayerS.Stamina - 0.20f;
            }

            if (isHitAnim == true)
            {
               if (Grounded == true)
                _animator.Play("PlayerGetHit");

                if (Grounded == false)
                   _animator.Play("Damage_Fly");

                isHitAnim = false;
            }


        }

        public void Start_Combo()
        {
            isAttack = false;
            if (combo < 3)
            {
                combo++;
            }
        }

        public void Finish_Ani()
        {
            isAttack = false;
            combo = 0;
        }

        public void StartJumpAttack()
        {
            isJumpAttack = true;
        }

        public void EndJumpAttack()
        {
            isJumpAttack = false;
        }

        public void EndGuard()
        {
            isGuard = false;
            _animator.SetTrigger("EndGuard");
        }

        public void EndHit()
        {
            isHit = false;
            isAttack = false;
            isSkill = false;
            combo = 0;
        }

        public void StartDamage()
        {
            isDamage = true;
        }

        public void EndDamage()
        {
            isDamage = false;
        }

        public void FaceEnemy()
        {
            //transform.LookAt(closeEnemy.gameObject.transform);
        }

        public void StartThirdHit()
        {
            isThirdHit = true;
        }

        public void StopThirdHit()
        {
            isThirdHit = false;
            isDamage = false;
        }

        private void StartSkill()
        {
            isSkill = true;
        }

        private void EndSkill()
        {
            isSkill = false;
        }

        private void StartSkillHit()
        {
            isSkillHit = true;
        }

        private void StopSkillHit()
        {
            isSkillHit = false;
        }

        private void Slash1()
        {
            GameObject seObject = Instantiate(slashE1, new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z), Quaternion.Euler(0, player.transform.eulerAngles.y, -153)) as GameObject;
            Destroy(seObject, 1);
        }

        private void Slash2()
        {
            GameObject seObject = Instantiate(slashE2, new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z), Quaternion.Euler(0, player.transform.eulerAngles.y, 25)) as GameObject;
            Destroy(seObject, 1);
        }

        private void Slash3()
        {
            GameObject seObject = Instantiate(slashE3, new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z), Quaternion.Euler(-24, (player.transform.eulerAngles.y - 90), 0)) as GameObject;
            Destroy(seObject, 1);
        }

        private void Skill1Slash()
        {
            GameObject seObject = Instantiate(slashE4, new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z), Quaternion.identity) as GameObject;
            Destroy(seObject, 1);
            GameObject se2Object = Instantiate(slashE5, new Vector3(transform.position.x, (transform.position.y), transform.position.z), Quaternion.identity) as GameObject;
            Destroy(se2Object, 1);
        }

        public void ResumeGame()
        {
                Cursor.lockState = CursorLockMode.Locked;
                CursorLocked = true;
                CursorTimer = 0;
                LockCameraPosition = false;
                LockAction = false;
                MenuPanel.SetActive(false);
                MenuList.SetActive(false);
                StatList.SetActive(false);
                MenuOpened = false;
                MenuDone = false;
            //  MenuPanelAnim.Play("MenuPanelAnim");
            //  MenuListAnim.Play("MenuAnim");

            BGM1.volume = BGM1.volume * 3;
            BGM2.volume = BGM2.volume * 3;

            ConfirmPanel.SetActive(false);
            ConfirmPanel2.SetActive(false);

            Time.timeScale = 1;
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                _cinemachineTargetYaw += _input.look.x * Time.deltaTime;
                _cinemachineTargetPitch += _input.look.y * Time.deltaTime;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            if (PlayerS.Stamina <= 0)
            {
                float targetSpeed1 = MoveSpeed;
                targetSpeed = targetSpeed1;
            }

            if (PlayerS.Stamina > 1)
            {
                float targetSpeed2 = _input.sprint ? SprintSpeed : MoveSpeed;
                targetSpeed = targetSpeed2;
            }

            if (isAttack == true)
            {
                targetSpeed = 1f;
            }

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }
    }
}