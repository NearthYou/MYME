using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.SubsystemsImplementation;

public class PlayerInput : MonoBehaviour
{
    public static event Action<bool> OnMoveEvent;

    [Header("PlayerStat")] [SerializeField]
    private float moveSpeed = 5f;

    [Header("Attack Prefab")] [SerializeField]
    private GameObject attackHorizontal;

    [SerializeField] private GameObject attackVertical;

    [Header("Binding Object")] [SerializeField]
    private Transform initialPosition;


    [Header("Sprites")] [SerializeField] private Sprite[] sprites;

    //private IBugControl bug;
    private GameObject attackObject;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private EDirection direction;
    private SuspicionAddTimer suspicionAddTimer;
    private Animator animator;


    private bool isMoving;

    private bool isAttacking;
    private bool isStandingInit;
    private bool canMove;

    public bool CanMove
    {
        get => canMove;
    }

    private float hp = 3f;
    private float coolTime = 0.3f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        suspicionAddTimer = Utils.TryOrAddComponent<SuspicionAddTimer>(gameObject);
        suspicionAddTimer.SetTimer(1, 10);
        animator = GetComponent<Animator>();
        direction = EDirection.South;
    }

    void Update()
    {
        if (canMove)
        {
            isMoving = moveDirection != Vector2.zero;

            if (GameManager.instance.isComeback && isMoving)
                suspicionAddTimer.StartTimer();
            else if (GameManager.instance.isComeback && !isMoving)
                suspicionAddTimer.StopTimer();

            if (isMoving)
            {
                animator.enabled = true;
                transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * Time.deltaTime * moveSpeed;
            }
            // else
            // {
            //     animator.enabled = false;
            // }
        }

        if (isAttacking)
        {
            coolTime -= Time.deltaTime;
            if (coolTime <= 0)
            {
                isAttacking = false;
                coolTime = 0.2f;
            }
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();

        moveDirection = inputVector;
        SetDirection(moveDirection);
    }

    void OnAttack()
    {
        AttackDirection(direction);
    }

    private void AttackDirection(EDirection direction)
    {
        if (!isAttacking)
        {
            if (attackObject != null)
                Destroy(attackObject);

            switch (direction)
            {
                case EDirection.East:
                    attackObject = Instantiate(attackHorizontal, transform.position + Vector3.right,
                        Quaternion.identity, transform);
                    break;
                case EDirection.West:
                    attackObject = Instantiate(attackHorizontal, transform.position + Vector3.left, Quaternion.identity,
                        transform);
                    break;
                case EDirection.North:
                    attackObject = Instantiate(attackVertical, transform.position + Vector3.up, Quaternion.identity,
                        transform);
                    break;
                case EDirection.South:
                    attackObject = Instantiate(attackVertical, transform.position + Vector3.down, Quaternion.identity,
                        transform);
                    break;
            }

            isAttacking = true;
        }
    }

    void OnFixation(InputValue value)
    {
        if (isStandingInit)
        {
            if(canMove)
                SetDirection(EDirection.South);
            
            canMove = !canMove;
            transform.position = initialPosition.position;
            animator.enabled = false;
            OnMoveEvent?.Invoke(!canMove);
        }
    }

    public void SetIsStandingInit(bool value)
    {
        isStandingInit = value;
    }

    private void SetDirection(Vector2 value)
    {
        if (value == Vector2.left)
        {
            direction = EDirection.West;
            animator.SetTrigger("Side");
            spriteRenderer.sprite = sprites[0];
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        else if (value == Vector2.right)
        {
            direction = EDirection.East;
            spriteRenderer.sprite = sprites[0];
            animator.SetTrigger("Side");
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
        }
        else if (value == Vector2.up)
        {
            direction = EDirection.North;
            spriteRenderer.sprite = sprites[2];
            animator.SetTrigger("Behind");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        else if (value == Vector2.down)
        {
            direction = EDirection.South;
            spriteRenderer.sprite = sprites[1];
            animator.SetTrigger("Front");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
    }

    private void SetDirection(EDirection direction)
    {
        if (direction == EDirection.West)
        {
            direction = EDirection.West;
            animator.SetTrigger("Side");
            spriteRenderer.sprite = sprites[0];
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        else if (direction == EDirection.East)
        {
            direction = EDirection.East;
            spriteRenderer.sprite = sprites[0];
            animator.SetTrigger("Side");
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
        }
        else if (direction == EDirection.North)
        {
            direction = EDirection.North;
            spriteRenderer.sprite = sprites[2];
            animator.SetTrigger("Behind");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        else if (direction == EDirection.South)
        {
            direction = EDirection.South;
            spriteRenderer.sprite = sprites[1];
            animator.SetTrigger("Front");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
    }


    // public void SetBug(IBugControl bugControl)
    // {
    //     bug = bugControl;
    // }
}