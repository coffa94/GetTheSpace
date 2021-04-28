using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float speedBullet=0.1f;
    [SerializeField] private int damageBullet;

    private Rigidbody2D _rigidbody;
    private Vector2 _destination;
    private Vector2 _direction;
    private int _playerTarget;

    public Vector2 Destination { get => _destination; set => _destination = value; }
    public int PlayerTarget { get => _playerTarget; set => _playerTarget = value; }
    public int DamageBullet { get => damageBullet; set => damageBullet = value; }

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
            //i don't check anymore if the bulletEnemy's playterTarget is the same of the collision,
            //enemies shot towards their player target position so if a player is hit by an enemy bullet is because was the target of the enemy that shot
            Debug.Log("Giocatore " + collision.GetComponent<PlayerController>().PlayerNumber + " colpito dal proiettilo nemico");
            //destroy the bullet after its collision with the player
            Destroy(gameObject);
            
            
        }
    }
}
