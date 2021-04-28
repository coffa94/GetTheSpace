using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private void Awake() {
        playerData.onLifeChanged += UpdateLife;
    }

    private void UpdateLife(float newLife) {
        gameObject.GetComponent<Image>().fillAmount = newLife;
        //inserimento del riferimento al componente text all'inizio per evitare di cercarlo ad ogni aggiornamento dello score
        //scoreText.text= "Score: " + score.ToString();
    }
}
