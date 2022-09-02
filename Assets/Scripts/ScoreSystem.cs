using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultiplier;

    private bool shouldCount = true;
    public float score; 

    void Update()
    {
        if (!shouldCount) return;
        
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = ((int)score).ToString();
        
    }

    public int EndTimer()
    {
        shouldCount = false;
        scoreText.text = string.Empty;
        return (int)score;
    }

    internal void StartTimer()
    {
        shouldCount = true;
    }
}
