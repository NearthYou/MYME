using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugHole : MonoBehaviour, IBugControl
{
    public bool canDeleted { get; set; }
    private int hp;

    private void Start()
    {
        hp = 5;
    }
    
    private void Update()
    {
        if (hp <= 0)
        {
            canDeleted = true;
            Destroy(gameObject);
        }
    }

    public void DeleteBug()
    {
        hp--;
        if (transform.localScale.x > 0.2f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.2f, transform.localScale.y - 0.2f);
        }

        if (hp <= 0)
        {
            canDeleted = true;
        }
    }
}