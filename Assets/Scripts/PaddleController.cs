using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public bool IsLeftBumper = true;
    public float MovementSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float displacement = IsLeftBumper ? Input.GetAxis("VerticalLeft") : Input.GetAxis("VerticalRight");
        this.transform.Translate(0f, 0f, displacement * 2f * MovementSpeed * Time.deltaTime);
    }
}
