using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDuringGame : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData.onEndGame += DisableScreenDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisableScreenDisplay() {
        gameObject.SetActive(!gameData.gameEnded);
    }
}
