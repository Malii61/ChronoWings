﻿

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GameOverWindow : MonoBehaviour {

    private Text scoreText;
    private Text highscoreText;
    [SerializeField] private Text gameOverText;
    private void Awake() {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        highscoreText = transform.Find("highscoreText").GetComponent<Text>();
        
        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.GameScene); };
        transform.Find("retryBtn").GetComponent<Button_UI>().AddButtonSounds();
        
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.MainMenu); };
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void Start() {
        GameHandler.Instance.OnDied += Bird_OnDied;
        Hide();
    }
    private void Bird_OnDied(object sender, System.EventArgs e) {
        scoreText.text = Level.GetInstance().GetPipesPassedCount().ToString();
        gameOverText.text = GameHandler.Instance.IsWin ? "YOU BEAT AI!" : "AI WINS..";
        if (Level.GetInstance().GetPipesPassedCount() >= Score.GetHighscore()) {
            // New Highscore!
            highscoreText.text = "NEW HIGHSCORE";
        } else {
            highscoreText.text = "HIGHSCORE: " + Score.GetHighscore();
        }

        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}
