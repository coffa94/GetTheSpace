using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speedMovement=0.2f;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject prefabShot;
    [SerializeField] private int playerNumber;
    [SerializeField] private float shotFrequency;

    private Rigidbody2D _rigidbody;

    private float _timerShot;

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
        if (Input.GetButton("Fire" + playerNumber) && _timerShot<=0f) {
            _timerShot = shotFrequency;
            GameObject newBullet = Instantiate(prefabShot);
            newBullet.transform.position = shootPosition.position;

            newBullet.GetComponent<Bullet>().SetPlayer(playerNumber);
            
        }
        _timerShot -= Time.deltaTime;
    }

    private void FixedUpdate() {
        //horizontal movement
        float horizontal = Input.GetAxis("Horizontal" + playerNumber);
        if (horizontal!=0) {
            _rigidbody.position = new Vector3(_rigidbody.position.x + horizontal*speedMovement, _rigidbody.position.y);
        }

        //vertical movement
        float vertical = Input.GetAxis("Vertical" + playerNumber);
        if (vertical != 0) {
            _rigidbody.position = new Vector3(_rigidbody.position.x, _rigidbody.position.y + vertical * speedMovement);
        }
    }
}
