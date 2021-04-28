using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet=10f;
    [SerializeField] private int damageBullet = 1;

    private int _playerNumber; //TODO maybe this info is useless because i have it in the playerData
    private int _shotDirection;
    private PlayerData playerData;

    private Rigidbody2D _rigidbody;

    public PlayerData PlayerData { get => playerData; set => playerData = value; }
    public int DamageBullet { get => damageBullet; set => damageBullet = value; }


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        //move the bullet in the scene
        _rigidbody.MovePosition(new Vector2(_rigidbody.position.x, _rigidbody.position.y + _shotDirection * speedBullet));
    }

    public void SetPlayer(int playerNumber) {
        _playerNumber = playerNumber;
        Debug.Log("bullet " + _playerNumber + " shooted");

        //set the direction of the bullet
        if (_playerNumber == 1) {
            _shotDirection = 1;
        } else {
            _shotDirection = -1;
        }
    }

    public int GetPlayer() {
        return _playerNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //trigger with enemy's spaceship
        if (collision.GetComponent<EnemyController>()) {
            // nemico è stato colpito dal proiettile del suo giocatore bersaglio
            if (collision.GetComponent<EnemyController>().GetPlayerTarget() == _playerNumber) {
                //destroy bullet
                Destroy(gameObject);
                //decrease enemy's life and checking remaining life
                if (collision.GetComponent<EnemyController>().DecreaseLife(DamageBullet) <= 0) {
                    //increase scorePlayer and destroy enemy
                    playerData.AddScore(collision.gameObject.GetComponent<EnemyController>().ScorePoints);
                    Destroy(collision.gameObject);

                    //use of singleton to decrease number enemies (for player _playerNumber) in the game
                    GameManager.gameManager.DecreaseEnemies(_playerNumber);
                }
                
            }
        }

        //trigger with obstacle
        if(collision.GetComponent<MeteorController>()) {
            // ostacolo è stato colpito dal proiettile del suo giocatore bersaglio
            if (collision.GetComponent<MeteorController>().PlayerNumberTarget == _playerNumber) {
                //destroy bullet
                Destroy(gameObject);
                //decrease obstacle's life and checking remaining life
                if (collision.GetComponent<MeteorController>().DecreaseLife(DamageBullet) <= 0) {
                    //increase scorePlayer and destroy obstacle
                    playerData.AddScore(collision.gameObject.GetComponent<MeteorController>().ScorePoints);
                    Destroy(collision.gameObject);
                }

            }
        }
    }


}
