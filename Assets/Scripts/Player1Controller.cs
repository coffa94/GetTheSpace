using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    
    [SerializeField] private float speedMovement=0.2f;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject prefabShot;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            GameObject newBullet = Instantiate(prefabShot);
            newBullet.transform.position = shootPosition.position;

            newBullet.GetComponent<Bullet>().SetPlayer(1);
            
        }
    }

    private void FixedUpdate() {
        //horizontal movement
        float horizontal = Input.GetAxis("Horizontal1");
        if (horizontal!=0) {
            _rigidbody.position = new Vector3(_rigidbody.position.x + horizontal*speedMovement, _rigidbody.position.y);
        }

        //vertical movement
        float vertical = Input.GetAxis("Vertical1");
        if (vertical != 0) {
            _rigidbody.position = new Vector3(_rigidbody.position.x, _rigidbody.position.y + vertical * speedMovement);
        }
    }
}
