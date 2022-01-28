using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score
    { 
        get { return _score; }
        set { ScoreUpdate(value); }
    }
    private int _score;

    private void Start()
    {
        ScoreReset();
    }

    public void ScoreReset()
    {
        _score = 0;
        scoreText.text = "0";
    }

    public void ScoreUpdate(int value)
    {
        _score += value;
        scoreText.text = _score.ToString();
    }
}
