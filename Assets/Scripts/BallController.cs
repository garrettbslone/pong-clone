using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    private const float BaseSpeed = 4f, SpeedStep = 1.55f;
    private int _leftScore = 0, _rightScore = 0;
    private PaddleController _leftPaddle, _rightPaddle;
    private Rigidbody _rigidbody;
    private AudioSource _audio;

    [FormerlySerializedAs("Velocity")] public float speed = BaseSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        _leftPaddle = GameObject.FindWithTag("LeftPaddle").GetComponent<PaddleController>();
        _rightPaddle = GameObject.FindWithTag("RightPaddle").GetComponent<PaddleController>();
        _rigidbody = GetComponent<Rigidbody>();

        float vx = Random.Range(0, 2) < 1 ? 1 : -1;
        float vz = Random.Range(0, 2) < 1 ? 1 : -1;

        _rigidbody.velocity = new Vector3(speed * vx, 0f, speed * vz);

        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.name)
        {
            case "Left":
            {
                _leftScore++;
                Debug.Log($"Left player scored.\nScore: Left: {_leftScore} | Right: {_rightScore}");

                if (_leftScore == 11)
                {
                    Debug.Log("Left Player Wins!");
                    ResetGame();
                }
                else
                {
                    OnScore(-1);
                }

                break;
            }
            case "Right":
            {
                _rightScore++;
                Debug.Log($"Right player scored.\nScore: Left: {_leftScore} | Right: {_rightScore}");

                if (_rightScore == 11)
                {
                    Debug.Log("Right Player Wins!");
                    ResetGame();
                }
                else
                {
                    OnScore(1);
                }

                break;
            }
            case "Left Paddle" or "Right Paddle":
            {
                _audio.clip = transform.position.x > 0 ? _leftPaddle.hitSound : _rightPaddle.hitSound;
                _audio.Play();
                _rigidbody.velocity *= SpeedStep;
                break;
            }
        }
    }


    private void OnScore(float vx)
    {
        transform.position = Vector3.zero;
        
        float vz = Random.Range(0, 2) < 1 ? 1 : -1;

        _rigidbody.velocity = new Vector3(speed * vx, 0f, speed * vz);

        _leftPaddle.ResetPaddle();
        _rightPaddle.ResetPaddle();
    }

    private void ResetGame()
    {
        OnScore(Random.Range(0, 2) < 1 ? 1 : -1);
        _leftScore = _rightScore = 0;
    }
}
