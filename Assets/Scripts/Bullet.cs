using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet=0.1f;

    private int _playerNumber;
    private int _shotDirection;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //destroy player1's bullet when they exit the top screen
        if (_playerNumber==1 && transform.position.y > 12f) {
            Destroy(gameObject);
            return;
        }

        //destroy player2's bullet when they exit the bottom screen
        if (_playerNumber == 2 && transform.position.y < -12f) {
            Destroy(gameObject);
            return;
        }



        transform.Translate(new Vector3(0f, _shotDirection*speedBullet*Time.deltaTime, 0f));
    }

    public void SetPlayer(int playerNumber) {
        _playerNumber = playerNumber;
        Debug.Log("bullet " + _playerNumber + " shooted");
        if (_playerNumber == 1) {
            _shotDirection = 1;
        } else {
            _shotDirection = -1;
        }
    }

    
}
