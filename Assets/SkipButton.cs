using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public bool isSkip = false;
    float time = 0f;
    void Update()
    {
        if(!isSkip)
        {
            time += Time.deltaTime;
            if(time >= 1f)
            {
                isSkip = true;
                time = 0f;
            }
        }
        
        if(Input.GetKey(KeyCode.Z) && isSkip)
        {
            isSkip = false;
            gameObject.SetActive(false);
            GameManager.instance.SkipTutorial();
        }
    }
}
