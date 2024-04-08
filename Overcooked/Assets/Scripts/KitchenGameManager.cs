using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
    private enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    public event EventHandler OnStateChanged;
    public static KitchenGameManager Instance { get; private set; }

    private GameState state;
    private float waitingTimer = 1f;
    private float countdownTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 180f;

    private void Awake()
    {
        Instance = this;
        state = GameState.WaitingToStart;
    }
    private void Update()
    {
        switch (state)
        {
            case GameState.WaitingToStart:
                waitingTimer -= Time.deltaTime;
                if (waitingTimer < 0f)
                {
                    state = GameState.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case GameState.CountdownToStart:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer < 0f)
                {
                    state = GameState.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case GameState.GameOver:
                break;
        }
        Debug.Log(Mathf.Ceil(gamePlayingTimer));
    }
    public bool IsGamePlaying()
    {
        return state == GameState.GamePlaying;
    }
    public bool IsCountdown()
    {
        return state == GameState.CountdownToStart;
    }
    public bool IsGameOver()
    {
        return state == GameState.GameOver;
    }
    public float GetCountdownToStartTimer()
    {
        return countdownTimer;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
}
