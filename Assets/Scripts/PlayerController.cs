using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speedMovement=0.2f;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject prefabShot;
    [SerializeField] private int playerNumber;  //TODO maybe this is useless because i have this info in the playerData
    [SerializeField] private float shotFrequency;
    [SerializeField] private PlayerData playerData; //playerData to have player info
    [SerializeField] private int damagePlayer;
    [SerializeField] private int moltDamageOpponentPlayer;
    

    private Rigidbody2D _rigidbody;

    private float _timerShot;

    public int PlayerNumber { get => playerNumber; set => playerNumber = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _timerShot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //shot when fire is clicked and the timer is zero
        if (Input.GetButton("Fire" + PlayerNumber) && _timerShot<=0f) {
            _timerShot = shotFrequency;
            GameObject newBullet = Instantiate(prefabShot);
            newBullet.transform.position = shootPosition.position;

            newBullet.GetComponent<Bullet>().SetPlayer(PlayerNumber);
            newBullet.GetComponent<Bullet>().PlayerData = playerData;
            
        }
        _timerShot -= Time.deltaTime;
    }

    private void FixedUpdate() {
        //horizontal movement
        float horizontal = Input.GetAxis("Horizontal" + PlayerNumber);

        _rigidbody.velocity = new Vector2(horizontal * speedMovement, _rigidbody.velocity.y);
        

        //vertical movement
        float vertical = Input.GetAxis("Vertical" + PlayerNumber);
        
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, vertical * speedMovement);
       
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //checking if player is not hit (waiting for cooldown)
        if (!playerData.playerHit) {
            //trigger with bulletEnemy
            if (collision.GetComponent<BulletEnemy>()) {
                // player è stato colpito dal proiettile del nemico che ha player come target
                if (collision.GetComponent<BulletEnemy>().PlayerTarget == playerNumber) {

                    //decrease player's life
                    playerData.DecreaseLife(collision.GetComponent<BulletEnemy>().DamageBullet);

                }
            }

            //trigger with obstacle
            if (collision.GetComponent<MeteorController>()) {
                // player è stato colpito da un ostacolo

                //decrease player's life
                playerData.DecreaseLife(collision.GetComponent<MeteorController>().DamageMeteor);

                //decrease meteor's life and checking remaining life
                if (collision.GetComponent<MeteorController>().DecreaseLife(damagePlayer) <= 0) {
                    //destroy meteor
                    Destroy(collision.gameObject);

                }
            }

            //trigger with enemy's spaceship
            if (collision.GetComponent<EnemyController>()) {
                //no checking of enemyPlayerTarget and playerNumber
                //decrease player's life
                playerData.DecreaseLife(collision.GetComponent<EnemyController>().DamageEnemy);

                //decrease enemy's life and checking remaining life
                if (collision.GetComponent<EnemyController>().DecreaseLife(damagePlayer) <= 0) {
                    //destroy enemy
                    Destroy(collision.gameObject);

                    //use of singleton to decrease number enemies (for player _playerNumber) in the game
                    GameManager.gameManager.DecreaseEnemies(playerNumber);
                }
            }


            //trigger with opponent player's bullet
            if (collision.gameObject.GetComponent<Bullet>()) {
                //checking playerNumber with bullet's playerNumber (who shot the bullet)
                if (collision.GetComponent<Bullet>().GetPlayer() != playerNumber) {

                    //decrease player's life
                    playerData.DecreaseLife(collision.GetComponent<Bullet>().DamageBullet * moltDamageOpponentPlayer);

                    //destroy bullet
                    Destroy(collision.gameObject);

                }
            }
        }
    }

        
}
