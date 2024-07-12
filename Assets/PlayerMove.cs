using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Vector2 moveDirection;
    private bool isMoving;
    
    void Update()
    {
        isMoving = moveDirection != Vector2.zero;
        if (isMoving)
        {
            transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * Time.deltaTime * moveSpeed;
        }
    }
    
    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        if (inputVector != null)
        {
            moveDirection = inputVector;
            Debug.Log($"Stick Direction: {moveDirection}");
        }
    }
}
