using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Skeleton>().Dead();
        }

        if (other.CompareTag("Fairy"))
        {
            other.GetComponent<Fairy>().Suicide();
        }

        if (other.TryGetComponent(out BugHole bugHole))
        {
            bugHole.DeleteBug();
        }
    }
}