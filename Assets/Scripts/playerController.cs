// taken from Shark games first person movement in unity youtube guide

using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;




[RequireComponent(typeof(CharacterController))]
public class playerController : MonoBehaviour
{
    public HealthBar hp;
    public experienceBar XP;

    [SerializeField] private float maxPlayerHealth = 100.0f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float playerLevel;
    [SerializeField] private float currentXp;
    [SerializeField] private float NextLevelXp;
    [SerializeField] private float WalkSpeed = 5.5f;
    [SerializeField] private float RunningSpeed = 9.0f;
    [SerializeField] private float playerDamage = 1.0f;

    [SerializeField] private bool isInvincible = false;

    [SerializeField] private float JumpForce = 8.0f;
    [SerializeField] private float gravity = 20.0f;

    [SerializeField] private float LookSens = 0.2f;
    [SerializeField] private float LookAngleLimit = 90.0f;

    private Camera mainCamera;
    private CharacterController characterController;

    private InputAction moveInput;
    private InputAction runInput;

    private InputAction jumpInput;
    private bool jumped = false;

    private float currentMoveSpeed = 0.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float lookAngle = 0.0f;

    private void Start()
    {
        currentHealth = maxPlayerHealth;
        hp.SetMaxHealth(maxPlayerHealth);

        currentXp= 0;
        XP.setExp(currentXp);

        playerLevel = 1;

        NextLevelXp = (playerLevel / 2) * 100;
        XP.SetMaxExp(NextLevelXp);


        mainCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        moveInput = InputSystem.actions.FindAction("Move");
        runInput = InputSystem.actions.FindAction("Sprint");

        jumpInput = InputSystem.actions.FindAction("Jump");
        jumpInput.started += Jumped;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentMoveSpeed = WalkSpeed;
    }

    private void Update()
    {
        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());


        if (!characterController.isGrounded)
            jumped = false;

        currentMoveSpeed = runInput.IsPressed() ? RunningSpeed : WalkSpeed;
        HandleMovement(moveVector);
        HandleLooking(mouseDelta);

        if (currentHealth <= 0.0f)
            SceneManager.LoadScene(1);
            
    }

    private void HandleMovement(Vector2 moveVector)
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = moveDirection.y;

        Vector2 newSpeed = new Vector2(moveVector.y * currentMoveSpeed, moveVector.x * currentMoveSpeed);

        moveDirection = (forward * newSpeed.x) + (right * newSpeed.y);
        if (jumped && characterController.isGrounded)
            moveDirection.y = JumpForce;
        else
            moveDirection.y = oldY;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Jumped(InputAction.CallbackContext _)
    {
        jumped = true;
    }

    private void HandleLooking(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * LookSens;
        lookAngle = Mathf.Clamp(lookAngle, -LookAngleLimit, LookAngleLimit);

        mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * LookSens, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isInvincible == false)
        {
            if (other.CompareTag("Enemy"))
            {
                currentHealth -= 1.0f;
                hp.SetHealth(currentHealth);
            }
        }

        if (other.CompareTag("Retry"))
            SceneManager.LoadScene(0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Invincible"))
        {
            Destroy(other.gameObject);
            isInvincible = true;
            StartCoroutine(Invtimer());
            
        }

        if (other.CompareTag("DamagePowerup"))
        {
            Destroy(other.gameObject);
            playerDamage *= 3;
            StartCoroutine(DamTimer());
        }
    }
    public void updateExp(float EXP)
    {
        currentXp += EXP;
        XP.setExp(currentXp);
        if (currentXp >= NextLevelXp)
        {
            playerLevel += 1;
            WalkSpeed += 2.0f;
            RunningSpeed += 2.0f;
            JumpForce += 2.0f;
            playerDamage += 1;
            NextLevelXp = (playerLevel / 2) * 100;
            currentXp = 0;
            XP.setExp(currentXp);
            XP.SetMaxExp(NextLevelXp);
        }
    }

    public float getDamage()
    {
        return playerDamage;
    }

     IEnumerator Invtimer()
    {
        yield return new WaitForSeconds(5.0f);
        isInvincible = false;
    }

    IEnumerator DamTimer()
    {
        yield return new WaitForSeconds(5.0f);
        playerDamage /= 3;
    }
}
