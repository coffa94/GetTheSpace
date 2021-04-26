using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [SerializeField] private float speedMovement = 0.1f;
    [SerializeField] private int scorePoints;
    [SerializeField] private int life;


    private int _playerNumberTarget;
    private int _movementDirection;

    private Rigidbody2D _rigidbody;

    public int PlayerNumberTarget { get => _playerNumberTarget; set => _playerNumberTarget = value; }
    public int ScorePoints { get => scorePoints; set => scorePoints = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        //move the bullet in the scene
        _rigidbody.MovePosition(new Vector2(_rigidbody.position.x, _rigidbody.position.y + _movementDirection * speedMovement));
    }

    public void SetPlayer(int playerNumber) {
        PlayerNumberTarget = playerNumber;

        //set the direction of the bullet
        if (PlayerNumberTarget == 1) {
            _movementDirection = -1;
        } else {
            _movementDirection = 1;
        }
    }

    public int DecreaseLife(int damage) {
        life -= damage;
        Debug.Log("Obstacle player " + PlayerNumberTarget + " HitByShot, remaining life= " + life);
        return life;
    }


}
