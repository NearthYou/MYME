using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningSign : MonoBehaviour
{
    [SerializeField] private Image warningSign;
    private bool isWarning;

    private void Start()
    {
        warningSign.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isWarning)
        {
            warningSign.enabled = true;
            isWarning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isWarning)
        {
            warningSign.enabled = false;
            isWarning = false;
        }
    }
}
