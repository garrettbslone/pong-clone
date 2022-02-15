using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    private const float BaseSpeed = 4f, SpeedStep = 1.55f;
    private int _leftScore = 0, _rightScore = 0;
    private PaddleController _leftPaddle, _rightPaddle;
    private Rigidbody _rigidbody;
    private AudioSource _audio;
    private TMP_Text _leftScoreTxt, _rightScoreTxt;

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

        _leftScoreTxt = GameObject.Find("LeftScoreText").GetComponent<TMP_Text>();
        _rightScoreTxt = GameObject.Find("RightScoreText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void setHitSound()
    {
        
        PaddleController p = transform.position.x > 0 ? _leftPaddle : _rightPaddle;
        _audio.clip = (_rigidbody.velocity.x + _rigidbody.velocity.y) / 2 > 5f ? p.hitSound : p.hitSoundSlow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.name)
        {
            case "Right":
            {
                _leftScore++;
                Debug.Log($"Left player scored.\nScore: Left: {_leftScore} | Right: {_rightScore}");
                _leftScoreTxt.text = $"{_leftScore}";

                if (_leftScore > _rightScore)
                {
                    _leftScoreTxt.color = Color.green;
                    _rightScoreTxt.color = Color.red;
                } 
                else if (_leftScore < _rightScore)
                {
                    _leftScoreTxt.color = Color.red;
                    _rightScoreTxt.color = Color.green;
                }
                else
                {
                    _leftScoreTxt.color = Color.white;
                    _rightScoreTxt.color = Color.white;
                }

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
            case "Left":
            {
                _rightScore++;
                Debug.Log($"Right player scored.\nScore: Left: {_leftScore} | Right: {_rightScore}");
                _rightScoreTxt.text = $"{_rightScore}";

                if (_leftScore > _rightScore)
                {
                    _leftScoreTxt.color = Color.green;
                    _rightScoreTxt.color = Color.red;
                } 
                else if (_leftScore < _rightScore)
                {
                    _leftScoreTxt.color = Color.red;
                    _rightScoreTxt.color = Color.green;
                }
                else
                {
                    _leftScoreTxt.color = Color.white;
                    _rightScoreTxt.color = Color.white;
                }
                
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
                setHitSound();
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
