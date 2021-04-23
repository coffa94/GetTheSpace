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
}
