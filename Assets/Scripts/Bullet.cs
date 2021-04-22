using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet=10f;

    private int _playerNumber;
    private int _shotDirection;

    private Rigidbody2D _rigidbody;


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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<EnemyController>()) {
            //TODO controllare se nemico è stato colpito dal proiettile del suo giocatore bersaglio
            if (collision.GetComponent<EnemyController>().GetPlayerTarget() == _playerNumber) {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            Debug.Log("Nemico " + _playerNumber + " colpito");
        }
    }


}
