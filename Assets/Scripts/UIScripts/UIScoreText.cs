using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIScoreText : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    //[SerializeField] private Text scoreText;

    private void Awake() {
        playerData.onScoreChanged += UpdateScoreText;
    }

    private void UpdateScoreText(int score) {
        gameObject.GetComponent<Text>().text= "Score: " + score.ToString();
        //inserimento del riferimento al componente text all'inizio per evitare di cercarlo ad ogni aggiornamento dello score
        //scoreText.text= "Score: " + score.ToString();
    }
}
