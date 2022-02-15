using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PaddleController : MonoBehaviour
{
    [FormerlySerializedAs("IsLeftBumper")] public bool isLeftBumper = true;
    [FormerlySerializedAs("MovementSpeed")] public float movementSpeed = 3f;

    public AudioClip hitSound, hitSoundSlow;
    
    // Start is called before the first frame update
    void Start()
    {
        if (hitSound)
            hitSound.LoadAudioData();
        if (hitSoundSlow)
            hitSoundSlow.LoadAudioData();
    }

    // Update is called once per frame
    void Update()
    {
        float displacement = isLeftBumper ? Input.GetAxis("VerticalLeft") : Input.GetAxis("VerticalRight");
        this.transform.Translate(0f, 0f, displacement * 2f * movementSpeed * Time.deltaTime);   
    }
    public void ResetPaddle()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, 0f);
        GetComponent<Renderer>().material.color = Color.gray;
    }
}
