using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speedMovement = 0.1f;
    [SerializeField] private float shotFrequency;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform playerTargetTransform;

    private Vector3 _destinationPosition;

    private float _timerShot;

    private Rigidbody2D _rigidbody;

    public Transform PlayerTargetTransform { get => playerTargetTransform; set => playerTargetTransform = value; }
    public Vector3 DestinationPosition { get => _destinationPosition; set => _destinationPosition = value; }


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _timerShot = 0f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, DestinationPosition) < speedMovement) {
            if (_timerShot <= 0f) {
                _timerShot = shotFrequency;

                Vector3 vectorToTarget = PlayerTargetTransform.position - transform.position; //queste due righe ci consentono di ruotare
                Quaternion q = Quaternion.LookRotation(Vector3.forward, vectorToTarget);        //il proiettile nella direzione del giocatore

                //create a new enemy bullet
                GameObject newEnemyBullet = Instantiate(enemyBullet, shootPosition.position, q);
                //set the destination of the enemy bullet
                newEnemyBullet.GetComponent<BulletEnemy>().Destination=PlayerTargetTransform.position;
                //set the target player of the enemy bullet
                newEnemyBullet.GetComponent<BulletEnemy>().PlayerTarget = GetPlayerTarget();


            }
            _timerShot -= Time.fixedDeltaTime;
            return;
        }

        //move the enemy towards the destinationPosition selected in the correct side of the player selected
        _rigidbody.MovePosition(Vector2.MoveTowards(_rigidbody.position, DestinationPosition, speedMovement));
    }

    public int GetPlayerTarget() {
        return playerTargetTransform.GetComponent<PlayerController>().PlayerNumber;
    }

}
