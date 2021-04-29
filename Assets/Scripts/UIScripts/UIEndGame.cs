using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndGame : MonoBehaviour
{
    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData.onEndGame += EnableWinningDisplay;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableWinningDisplay() {
        gameObject.SetActive(gameData.gameEnded);
    }
}
