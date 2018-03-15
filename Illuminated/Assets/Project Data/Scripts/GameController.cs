using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public Text restartTooltip;
    public Paddle paddle;
    public Target target;
    public Light ambientLight;
    public CanvasGroup winScreen;

    public Vector3 targetStart;
    private float paddleStartX;
    private float startLight;

    public float darkTime = 1.0f;
    public float winScreenTime = 1.0f;
    public bool playing;
    public bool won;

	// Use this for initialization
	void Start () {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        playing = true;
        won = false;

        targetStart = target.transform.position;
        paddleStartX = paddle.transform.position.x;
        startLight = ambientLight.intensity;
	}
	
	// Update is called once per frame
	void Update () {
        if (playing == false && Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
	}

    private void Restart()
    {
        paddle.transform.position = new Vector3(paddleStartX, paddle.transform.position.y, paddle.transform.position.z);
        target.transform.position = targetStart;
        target.Restart();
        playing = true;
        SetBright();
        Color textColor = restartTooltip.color;
        textColor.a = 0;
        restartTooltip.color = textColor;
    }

    public void GameOver()
    {
        playing = false;
        StartCoroutine(ShowLossScreen());
    }

    public void Win()
    {
        playing = false;
        won = true;
        StartCoroutine(ShowWinScreen());
    }

    public IEnumerator ShowWinScreen()
    {
        float time = winScreenTime;
        while (time > 0f)
        {
            yield return null;
            time -= Time.deltaTime;
            winScreen.alpha = 1 - (time / winScreenTime);
        }

        winScreen.alpha = 1;
    }

    private IEnumerator ShowLossScreen()
    {
        float time = darkTime;
        while(time > 0f)
        {
            yield return null;
            time -= Time.deltaTime;
            ambientLight.intensity = startLight * (time / darkTime);
            Color text = restartTooltip.color;
            text.a = 1 - (time / darkTime);
            restartTooltip.color = text;
        }

        Color textColor = restartTooltip.color;
        textColor.a = 1;
        restartTooltip.color = textColor;
        ambientLight.intensity = 0;
    }

    private void SetBright()
    {
        ambientLight.intensity = startLight;
    }
}
