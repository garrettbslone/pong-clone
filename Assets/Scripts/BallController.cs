using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [FormerlySerializedAs("Velocity")] public float speed = 1f;

    private int _leftScore = 0, _rightScore = 0;
    private PaddleController _leftPaddle, _rightPaddle;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _leftPaddle = GameObject.FindWithTag("LeftPaddle").GetComponent<PaddleController>();
        _rightPaddle = GameObject.FindWithTag("RightPaddle").GetComponent<PaddleController>();
        _rigidbody = GetComponent<Rigidbody>();

        float vx = Random.Range(0, 2) < 1 ? 1 : -1;
        float vz = Random.Range(0, 2) < 1 ? 1 : -1;

        _rigidbody.velocity = new Vector3(speed * vx, 0f, speed * vz);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Left")
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
                OnScore();
            }
        }
        else if (collision.gameObject.name == "Right")
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
                OnScore();
            }
        }
        else if (collision.gameObject.name is "Left Paddle" or "Right Paddle")
        {
            _rigidbody.velocity *= 2f;
        }

}


    private void OnScore()
    {
        transform.position = Vector3.zero;
        
        float vx = Random.Range(0, 2) < 1 ? 1 : -1;
        float vz = Random.Range(0, 2) < 1 ? 1 : -1;

        _rigidbody.velocity = new Vector3(speed * vx, 0f, speed * vz);

        _leftPaddle.ResetPaddle();
        _rightPaddle.ResetPaddle();
    }

    private void ResetGame()
    {
        OnScore();
        _leftScore = _rightScore = 0;
    }
}
