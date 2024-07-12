using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugHole : MonoBehaviour, IBugControl
{
    private int hp;

    private void Start()
    {
        hp = 5;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DeleteBug()
    {
        hp--;
        transform.localScale = new Vector3(transform.localScale.x - 0.3f, transform.localScale.y - 0.3f, transform.localScale.z);
    }
}
