using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugHole : MonoBehaviour, IBugControl
{
    public static event Action OnBugDie;

    public bool canDeleted { get; set; }
    private int hp;
    private GameObject arrow;
    private Animator animator;

    private void Start()
    {
        hp = 5;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            OnBugDie?.Invoke();
            canDeleted = true;
            Destroy(arrow);
            Destroy(gameObject);
        }
    }

    public void DeleteBug()
    {
        hp--;
        ChangeAnimation(hp);

        if (hp <= 0)
        {
            canDeleted = true;
        }
    }

    public void SetArrow(GameObject _arrow)
    {
        arrow = _arrow;
        arrow.GetComponent<Navigation>().SetTarget(transform);
    }

    private void ChangeAnimation(int count)
    {
        if (count == 0)
            return;
        
        animator.SetTrigger(count.ToString());
    }
}