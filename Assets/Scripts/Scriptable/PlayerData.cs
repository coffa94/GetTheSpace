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


    //programming pattern observer/observable
    public delegate void OnScoreChanged(int score);

    public OnScoreChanged onScoreChanged;

    //avviata all'inizio del gioco per quanto riguarda gli scriptableObjects
    private void OnEnable() {
        health = maxHealth;
        score = 0;
    }

    public void AddScore(int points) {
        //add points earned to the player score
        score += points;
        Debug.Log("Score player " + PlayerNumber + " = " + score);
        onScoreChanged.Invoke(score);
    }

    public void DecreaseHealth(int damage) {
        //decrease health by damage points
        health -= damage;
    }


}
