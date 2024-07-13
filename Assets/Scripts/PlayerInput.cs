using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    public static event Action<bool> OnMoveEvent;
    
    [Header("PlayerStat")]
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Attack Prefab")]
    [SerializeField] private GameObject attackHorizontal;
    [SerializeField] private GameObject attackVertical;
    
    [Header("Binding Object")]
    [SerializeField] private Transform initialPosition;
    
    //private IBugControl bug;
    private GameObject attackObject;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private EDirection direction;
    
    private bool isMoving;
    private bool isAttacking;
    private bool isStandingInit;
    private bool canMove;

    private float hp = 3f;
    private float coolTime = 0.3f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (canMove)
        {
            isMoving = moveDirection != Vector2.zero;
            if (isMoving)
            {
                transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * Time.deltaTime * moveSpeed;
            }
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
        // if (bug != null)
        // {
        //     bug.DeleteBug();
        //
        //     if (bug.canDeleted)
        //     {
        //         bug = null;
        //     }
        // }
        // else
        // {
            AttackDirection(direction);
        //}
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
                    attackObject = Instantiate(attackHorizontal, transform.position + Vector3.right, Quaternion.identity, transform);
                    break;
                case EDirection.West:
                    attackObject=Instantiate(attackHorizontal, transform.position + Vector3.left, Quaternion.identity, transform);
                    break;
                case EDirection.North:
                    attackObject= Instantiate(attackVertical, transform.position + Vector3.up, Quaternion.identity, transform);
                    break;
                case EDirection.South:
                    attackObject= Instantiate(attackVertical, transform.position + Vector3.down, Quaternion.identity, transform);
                    break;
            }
            isAttacking = true;
        }
    }

    void OnFixation(InputValue value)
    {
        if (isStandingInit)
        {
            canMove = !canMove;
            transform.position = initialPosition.position;
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
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        else if (value == Vector2.right)
        {
            direction = EDirection.East;
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
        }
        else if (value == Vector2.up)
        {
            direction = EDirection.North;
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = true;
        }
        else if (value == Vector2.down)
        {
            direction = EDirection.South;
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
    }

    // public void SetBug(IBugControl bugControl)
    // {
    //     bug = bugControl;
    // }
}