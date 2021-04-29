using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData.asset", menuName = "Dati della partita")]
public class GameData : ScriptableObject
{
    public bool gameEnded;

    public delegate void OnEndGame();

    public OnEndGame onEndGame;

    private void OnEnable() {
        gameEnded = false;
    }

    public void EndGame() {
        gameEnded = true;
        Debug.Log("Game ended");

        //call all the observer's function registered in the onEndGame function
        onEndGame.Invoke();
    }

}
