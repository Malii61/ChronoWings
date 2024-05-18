using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    public bool IsWin = false;
    public static bool isGameEnded = false;
    public EventHandler OnDied;

    private void Awake()
    {
        Instance = this;
        isGameEnded = false;
    }

    private void Start() {
        Score.Start();
    }

    public void Died()
    {
        OnDied?.Invoke(this,EventArgs.Empty);
        isGameEnded = true;
    }
}
