using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet=10f;

    private int _playerNumber; //TODO maybe this info is useless because i have it in the playerData
    private int _shotDirection;
    private PlayerData playerData;

    private Rigidbody2D _rigidbody;

    public PlayerData PlayerData { get => playerData; set => playerData = value; }


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
        if (collision.GetComponent<EnemyController>()) {
            //TODO controllare se nemico è stato colpito dal proiettile del suo giocatore bersaglio
            if (collision.GetComponent<EnemyController>().GetPlayerTarget() == _playerNumber) {
                playerData.AddScore(collision.gameObject.GetComponent<EnemyController>().ScorePoints);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            Debug.Log("Nemico " + _playerNumber + " colpito");
        }
    }


}
