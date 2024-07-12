using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool canMove;
    
    [Header("Attack Prefab")]
    [SerializeField] private GameObject attackHorizontal;
    [SerializeField] private GameObject attackVertical;
    
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private EDirection direction;
    
    private bool isMoving;
    private bool isStandingOnBug;

    private float hp = 3f;
    private GameObject attackObject;
    private IBugControl bug;

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
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();

        moveDirection = inputVector;
        SetDirection(moveDirection);
    }

    void OnAttack()
    {
        if (bug != null)
        {
            bug.DeleteBug();
        }
        else
        {
            if(attackObject != null)
                Destroy(attackObject);
        
            switch (direction)
            {
                case EDirection.East:
                    attackObject = Instantiate(attackHorizontal, transform.position + Vector3.right, Quaternion.identity);
                    break;
                case EDirection.West:
                    attackObject=Instantiate(attackHorizontal, transform.position + Vector3.left, Quaternion.identity);
                    break;
                case EDirection.North:
                    attackObject= Instantiate(attackVertical, transform.position + Vector3.up, Quaternion.identity);
                    break;
                case EDirection.South:
                    attackObject= Instantiate(attackVertical, transform.position + Vector3.down, Quaternion.identity);
                    break;
            }
        }
    }

    void OnFixation(InputValue value)
    {
        canMove = !canMove;
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

    public void SetBug(IBugControl bugControl)
    {
        bug = bugControl;
    }
}