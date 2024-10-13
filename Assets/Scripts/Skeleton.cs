using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, ISpeed
{
    private float moveSpeed;
    private GameObject target;
    private EDirection direction;
    private SpriteRenderer spr;
    private Animator animator;
    private float angle;
    private bool isRunning;
    private CircleCollider2D circleCollider2D;
    private StageController stageController;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        gameObject.tag = "Monster";
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        stageController = GameObject.FindGameObjectWithTag("StageController").GetComponent<StageController>();
        angle = Quaternion.FromToRotation(Vector3.up, transform.position - target.transform.position).eulerAngles.z;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            SetAngle(angle);
        }
    }

    private void SetAngle(float _angle)
    {
        if (angle < 45 || angle > 315)
            StartCoroutine(SetDirection(EDirection.North));
        else if (angle < 135)
            StartCoroutine(SetDirection(EDirection.West));

        else if (angle < 225)
            StartCoroutine(SetDirection(EDirection.South));

        else
            StartCoroutine(SetDirection(EDirection.East));

        angle = Quaternion.FromToRotation(Vector3.up, transform.position - target.transform.position).eulerAngles.z;
    }

    private IEnumerator SetDirection(EDirection _direction)
    {
        if (isRunning)
            yield break;

        isRunning = true;
        yield return new WaitForSeconds(0.5f);
        direction = _direction;

        switch (direction)
        {
            case EDirection.East:
                animator.SetTrigger("Right");
                break;
            case EDirection.West:
                animator.SetTrigger("Left");
                break;
            case EDirection.South:
                animator.SetTrigger("Behind");
                break;
            case EDirection.North:
                animator.SetTrigger("Front");
                break;
        }

        isRunning = false;
    }

    public void Dead()
    {
        animator.SetTrigger("Death");
        target = null;
        circleCollider2D.enabled = false;
        stageController.CountDeadMonster();
        Destroy(gameObject,0.2f);
    }

    public void Suicide()
    {
        stageController.CountDeadMonster();
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}