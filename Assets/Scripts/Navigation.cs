using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{

    [SerializeField] float arrowSpeed;
    [SerializeField] Transform target;

    private bool isSet;
    private SpriteRenderer spr;
    private Color initColor;

    private Camera cam;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        initColor = spr.color;
    }
    
    private void Start()
    {
        cam = Camera.main;
    }
    

    void Update ()
    {
        if(isSet)
            ArrowHint();
    }

    public void SetTarget(Transform _target)
    {
        spr = GetComponent<SpriteRenderer>();
        target = _target;
        isSet = true;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
 
    private void ArrowHint()
    {
 
        Vector3 pos = cam.WorldToViewportPoint(target.position);
        
        // 타겟이 카메라 시야각 안으로 들어왔을 때
        if ((pos.x < 1.0f && pos.x > 0.0f) && (pos.y < 1.0f && pos.y > 0.0f))
        {
            spr.color = Color.clear;
            transform.position = cam.ViewportToWorldPoint(pos);
            return;
        }
        spr.color = initColor;
 
        pos *= 2.0f;
        pos = new Vector3(pos.x - 1, pos.y - 1, pos.z - 1);
 
        if (Mathf.Abs(pos.x) > Mathf.Abs(pos.y))
        {
            pos.y = pos.y / Mathf.Abs(pos.x);
            if (pos.x > 1)
                pos.x = 1;
            else if (pos.x < -1)
                pos.x = -1;
        }
        else
        {
            pos.x = pos.x / Mathf.Abs(pos.y);
            if (pos.y > 1)
                pos.y = 1;
            else if (pos.y < -1)
                pos.y = -1;
        }
        pos = new Vector3(pos.x + 1, pos.y + 1, pos.z + 1);
        pos /= 2.0f;
        transform.position = Vector3.Lerp(transform.position , cam.ViewportToWorldPoint(pos * 0.8f + Vector3.one * 0.1f), Time.deltaTime * arrowSpeed) ;
        LookAt2DLerp(target.position);
    }   
    
    public void LookAt2DLerp(Vector2 dir, float lerpPercent = 0.05f)
    {
        float rotationZ = Mathf.Acos(dir.x / dir.magnitude)
                          * 180 / Mathf.PI
                          * Mathf.Sign(dir.y) +25f;
 
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0,0,rotationZ),
            lerpPercent
        );
    }
}
