using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterState CharacterStateMachine { get { return stateMachine; } }
    public Animator Animator { get { return animator; } }
    public bool IsGrounded { get; private set; }
    public bool CanMove { get; private set; }
    public float MoveDirection { get; private set; }
    public bool CanAttack { get; private set; }
    public bool CanThrowKunai;//{ get; private set; }
    public float KunaiFireRate { get { return kunaiFireRate; } }

    public bool IsBot;
    [SerializeField] CharacterState stateMachine;
    [Header("Kunai Settings")]
    [SerializeField] Kunai kunaiPrefab;
    [SerializeField] Transform kunaiSpawnPoint;
    [SerializeField] float kunaiFireRate = .5f;
    [SerializeField] float kunaiSpawnDelay = 0.1f;
    [SerializeField] float kunaiMoveSpeed = 5f;
    [SerializeField] int kunaiDamage = 20;

    [Header("Attack Settings")]
    public MeleeAttackDetector meleeAttackDetector;
    public float enableDetecterDelay = 0.2f;        // delay duration before enabling the meleeDetector
    public float attackDuration = 0.5f;             // how long the melee detector will be appear
    public float attackCoolDown = 1f;               // cooldown before attacking
    public int attackDamage = 10;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;

    [Header("Hurt Flashes Settings")]
    [SerializeField] float flashDuration = 0.3f;
    [SerializeField] float flashInterval = 0.05f;

    private List<CharacterStateBase> registeredState;
    private Animator animator;
    private HealthSystem healthSystem;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float elapsed;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        registeredState = GetComponents<CharacterStateBase>().ToList();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.RegisterCharacterState(registeredState);
        ChangeState(typeof(NinjaIdleState));

        EnableCanMove();
        EnableCanThrowKunai();

        healthSystem.OnTakeDamage.AddListener(OnTakeDamageEvent);
        healthSystem.OnDie.AddListener(OnDieEvent);

        meleeAttackDetector.gameObject.SetActive(false);
    }

    void Update()
    {
        MoveDirection = Input.GetAxisRaw("Horizontal");
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        animator.SetBool("IsGrounded", IsGrounded);

        if (CanMove == false)
            FreezeCharacter();

        stateMachine.currentState.OnStateRunning();

    }

    public void ChangeState(Type stateType)
    {
        if (!typeof(CharacterStateBase).IsAssignableFrom(stateType))
        {
            Debug.LogError($"Invalid state type: {stateType}. It must inherit from CharacterState.");
            return;
        }

        if (stateMachine.statesDict.ContainsKey(stateType))
        {
            stateMachine.ChangeState(stateMachine.statesDict[stateType]);
        }
        else
        {
            Debug.LogError($"State type {stateType} is not registered.");
        }
    }

    public void FreezeCharacter()
    {
        rb.linearVelocity = Vector3.zero;
    }

    private void OnTakeDamageEvent()
    {
        ChangeState(typeof(NinjaHurtState));
    }

    private void OnDieEvent()
    {
        ChangeState(typeof(NinjaDieState));
        enabled = false;
    }

    public void FlipCharacter()
    {
        if (MoveDirection != 0)
            transform.localScale = new Vector3(MoveDirection, 1, 1);
    }

    public void ThrowKunai()
    {
        animator.SetTrigger("ThrowKunai");
        if (IsGrounded)
            DisableCanMove();
        DisableCanThrowKunai();
        StartCoroutine(ThrowKunaiCor());
    }

    private IEnumerator ThrowKunaiCor()
    {
        yield return new WaitForSeconds(kunaiSpawnDelay);
        // Pool System later
        var kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, Quaternion.identity);
        kunai.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        kunai.ThrowKunai(transform.localScale.x, kunaiMoveSpeed, kunaiDamage, transform);
    }

    #region State Related
    public void Move()
    {
        rb.linearVelocity = new Vector2(MoveDirection * moveSpeed, rb.linearVelocityY);
    }

    public void MeleeAttack()
    {
        DisableCanMove();
        DisableCanAttack();
        animator.SetTrigger("Attack");
        StartCoroutine(EnableMeleeDetector());

        IEnumerator EnableMeleeDetector()
        {
            yield return new WaitForSeconds(enableDetecterDelay);
            meleeAttackDetector.gameObject.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            meleeAttackDetector.gameObject.SetActive(false);
        }
    }
    public void EnableCanMove()
    {
        CanMove = true;
    }

    public void DisableCanMove()
    {
        CanMove = false;
    }

    public void EnableCanAttack()
    {
        CanAttack = true;
    }

    public void DisableCanAttack()
    {
        CanAttack = false;
    }

    public void EnableCanThrowKunai()
    {
        CanThrowKunai = true;
    }

    public void DisableCanThrowKunai()
    {
        CanThrowKunai = false;
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    public void Hurt()
    {
        StartCoroutine(HurtCor());
    }

    private IEnumerator HurtCor()
    {
        DisableCanMove();
        elapsed = 0;

        while (elapsed < flashDuration)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashInterval);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashInterval);

            elapsed += flashInterval * 2;
        }

        EnableCanMove();
        spriteRenderer.color = Color.white; // pastikan kembali ke putih

        ChangeState(typeof(NinjaIdleState));
    }
    #endregion

    #region Input
    public bool GetJumpKey()
    {
        return Input.GetKeyDown(KeyCode.Space) && IsGrounded;
    }

    public bool GetAttackKey()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public bool GetThrowKunaiKey()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }
    #endregion

}
