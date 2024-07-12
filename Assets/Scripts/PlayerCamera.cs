using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera defenseCam;
    [SerializeField] private CinemachineVirtualCamera bugFixcam;
    
    public void SetCamera(bool isDefense)
    {
        defenseCam.gameObject.SetActive(isDefense);
        bugFixcam.gameObject.SetActive(!isDefense);
    }
}
