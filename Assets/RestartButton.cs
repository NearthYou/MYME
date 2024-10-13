using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            GameManager.instance.Restart();
        
        if(Input.GetKeyDown(KeyCode.X))
            GameManager.instance.ExitGame();
    }
}
