using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float JUMP_AMOUNT = 90f;

    private static Bird instance;

    public static Bird GetInstance()
    {
        return instance;
    }

    public event EventHandler OnStartedPlaying;

    private Rigidbody2D birdRigidbody2D;
    private State state;
    private bool autoStart = true;
    [SerializeField] private bool aiPlays;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    private void Awake()
    {
        instance = this;
        birdRigidbody2D = GetComponent<Rigidbody2D>();
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }

    public bool IsAI()
    {
        return aiPlays;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (PlayInput() || autoStart)
                {
                    // Start playing
                    state = State.Playing;
                    birdRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }

                break;
            case State.Playing:
                if (PlayInput())
                {
                    Jump();
                }

                // Rotate bird as it jumps and falls
                //transform.eulerAngles = new Vector3(0, 0, birdRigidbody2D.velocity.y * .15f);
                break;
            case State.Dead:
                break;
        }
    }

    private bool PlayInput()
    {
        if (aiPlays) return false;

        return
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0) ||
            Input.touchCount > 0;
    }

    public void Jump()
    {
        birdRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        if (!aiPlays)
            SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (GameHandler.isGameEnded) return;

        int checkpointLayer = 7;
        if (collider.gameObject.layer == checkpointLayer)
        {
            // Touched a checkpoint
        }
        else if (collider.CompareTag("Wall"))
        {
            // Time.timeScale = 0f;
            if (aiPlays)
            {
                GameHandler.Instance.IsWin = true;
                Debug.Log("YAPAY ZEKAYI YENDİN!!");
            }
            else
            {
                GameHandler.Instance.IsWin = false;
                SoundManager.PlaySound(SoundManager.Sound.Lose);
                Debug.Log("YAPAY ZEKA KAZANDI..");
            }

            birdRigidbody2D.bodyType = RigidbodyType2D.Static;
            GameHandler.Instance.Died();
        }
    }

    public void Reset()
    {
        birdRigidbody2D.velocity = Vector2.zero;
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
        
        if (aiPlays)
            transform.position = new Vector3(-10, 8, 0);
        else
            transform.position = Vector3.zero;
        
        state = State.WaitingToStart;
    }

    public float GetVelocityY()
    {
        return birdRigidbody2D.velocity.y;
    }
}