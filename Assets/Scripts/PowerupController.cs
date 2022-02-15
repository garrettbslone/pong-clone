using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public float movementSpeed = 8f;
    
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = new Vector3(0f, 0f, transform.position.z < 0 ? movementSpeed : -movementSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rigidbody.position.z) >= 5.4)
        {
            _rigidbody.velocity *= -1;
        }
    }
}
