using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transform.position.x < other.transform.position.x)
            {
                Debug.Log("Right");
                other.transform.position += Vector3.right * 1f;
            }
            else
            {
                Debug.Log("Left");
                other.transform.position += Vector3.left * 1f;
            }
        }
    }
}
