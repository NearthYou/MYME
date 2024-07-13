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
    private CircleCollider2D circleCollider2D;

    private void Start()
    {
        hp = 5;
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            //animator.SetTrigger(1.ToString());
            OnBugDie?.Invoke();
            canDeleted = true;
            SoundManager.instance.PlaySFX("ErrorDead");
            Destroy(arrow);
            Destroy(gameObject, 0.5f);
        }
    }

    public void DeleteBug()
    {
        hp--;
        if(hp<=3)
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
        if (count == -1)
            return;
        
        circleCollider2D.radius -= count * 0.01f;
        animator.SetTrigger((count+1).ToString());
    }
}