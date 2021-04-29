using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashingHit : MonoBehaviour
{
    [SerializeField] private int timeCooldownDamage;
    [SerializeField] private PlayerData playerData;

    private int _playerNumber;
    private float _timerCooldownDamage;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _playerNumber = GetComponent<PlayerController>().PlayerNumber;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
            if (playerData.playerHit) {
                _timerCooldownDamage -= Time.deltaTime;
                if (_timerCooldownDamage <= 0) {
                    FinishPlayerCooldown();
                }
            }
        

        
    }

    private void PlayerHit() {
        playerData.playerHit = true;
        _timerCooldownDamage = timeCooldownDamage;
        StartCoroutine("FlashPlayer");
    }

    private void FinishPlayerCooldown() {
        playerData.playerHit = false;
        _spriteRenderer.enabled = true;
    }

    IEnumerator FlashPlayer() {
        while (playerData.playerHit) {
            Debug.Log("FlashPlayerCoroutine");
            if (_spriteRenderer.isVisible) {
                _spriteRenderer.enabled = false;
            } else {
                _spriteRenderer.enabled = true;
            }
            yield return new WaitForSeconds(0.25f);
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //TODO maybe it's possible to use this script as an observer of PlayerData, when playerHit change i can call this script (the observer) and make it execute its code

        //checking if player is not hit (waiting for cooldown)
        if (!playerData.playerHit) {
            //trigger with bulletEnemy
            if (collision.GetComponent<BulletEnemy>()) {
                // player è stato colpito dal proiettile del nemico che ha player come target
                if (collision.GetComponent<BulletEnemy>().PlayerTarget == _playerNumber) {
                    PlayerHit();


                }
            }

            //trigger with obstacle
            if (collision.GetComponent<MeteorController>()) {
                // player è stato colpito da un ostacolo
                PlayerHit();

            }

            //trigger with enemy's spaceship
            if (collision.GetComponent<EnemyController>()) {
                PlayerHit();

            }


            //trigger with opponent player's bullet
            if (collision.gameObject.GetComponent<Bullet>()) {
                //checking playerNumber with bullet's playerNumber (who shot the bullet)
                if (collision.GetComponent<Bullet>().GetPlayer() != _playerNumber) {
                    PlayerHit();


                }
            }
        }

           
    }
}
