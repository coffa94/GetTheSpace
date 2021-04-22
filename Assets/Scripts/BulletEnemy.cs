using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float speedBullet=0.1f;

    private Rigidbody2D _rigidbody;
    private Vector2 _destination;
    private Vector2 _direction;
    private int _playerTarget;

    public Vector2 Destination { get => _destination; set => _destination = value; }
    public int PlayerTarget { get => _playerTarget; set => _playerTarget = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //set direction of the bullet so it moves towards the destination set, moving out of the screen if the player is not hit
        _direction = (Destination - _rigidbody.position)*1000f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.MovePosition(Vector2.MoveTowards(_rigidbody.position, _direction, speedBullet)); 

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerController>()) {
            if (PlayerTarget == collision.GetComponent<PlayerController>().PlayerNumber) {
                Debug.Log("Giocatore " + collision.GetComponent<PlayerController>().PlayerNumber + " colpito dal proiettilo nemico " + PlayerTarget);
                //destroy the bullet after its collision with the playerTarget of the enemy bullet
                Destroy(gameObject);
            }
            
        }
    }
}
