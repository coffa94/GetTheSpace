using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData.asset", menuName = "Dati del giocatore")]
public class PlayerData : ScriptableObject
{
    public int PlayerNumber; //namePlayer
    public int score; //scorePlayer
    public int health; //healthPlayer

    public int maxHealth=100; //max or start health

    public bool playerHit;


    //programming pattern observer/observable
    public delegate void OnScoreChanged(int score);
    public delegate void OnLifeChanged(float newLife);

    public OnScoreChanged onScoreChanged;
    public OnLifeChanged onLifeChanged;

    //avviata all'inizio del gioco per quanto riguarda gli scriptableObjects
    private void OnEnable() {
        health = maxHealth;
        score = 0;
        playerHit = false;
    }

    public void AddScore(int points) {
        //add points earned to the player score
        score += points;
        Debug.Log("Score player " + PlayerNumber + " = " + score);
        onScoreChanged.Invoke(score);
    }

    public void DecreaseLife(int damage) {
        //decrease player's life by damage value
        health -= damage;
        Debug.Log("Health player " + PlayerNumber + " = " + health);
        float percentageLife = (float)health / maxHealth;
        onLifeChanged.Invoke(percentageLife);

        if (health <= 0) {
            GameManager.gameManager.GameEnd();
        }
        
    }

}
