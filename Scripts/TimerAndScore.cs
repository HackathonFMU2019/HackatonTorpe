using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerAndScore : MonoBehaviour
{
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private TextMeshProUGUI timerGUI, scoreGUI, endGameScoreGUI;
    [SerializeField] private Canvas canvasHUD;
    [SerializeField] private TMP_Text scoreAdd, timerAdd;
    public int timer, score;

    void Start()
    {
        StartCoroutine(SecTimer());
        scoreGUI.text = "Pontos: " + score;
    }

    IEnumerator SecTimer()
    {
        yield return new WaitForSeconds(1);

        if(timer > 0)
        {
            timer--;
            timerGUI.text = "Tempo: " + timer;
            StartCoroutine(SecTimer());
        }
        else
        {
            endGameCanvas.SetActive(true);
            endGameScoreGUI.text = "Pontos: " + score;
            PlayerPrefs.SetInt("score", score);
        }
    }

    public void AddTime(int amount)
    {
        timer += amount;
        timerGUI.text = "Tempo: " + timer;
        GameObject objTime = timerAdd.gameObject;
        timerAdd.text = amount.ToString() + "+";
        Instantiate(objTime, canvasHUD.transform);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreGUI.text = "Pontos: " + score;
        GameObject objScore = scoreAdd.gameObject;
        scoreAdd.text = amount.ToString() + "+";
        Instantiate(objScore, canvasHUD.transform);
    }
}
