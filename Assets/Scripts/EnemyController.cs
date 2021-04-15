using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Camera cameraSelected;
    [SerializeField] private float minDistanceFromPlayer = 0.2f;
    [SerializeField] private float speedMovement = 0.1f;
    [SerializeField] private float shotFrequency;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject playerObject;

    private Vector3 _destinationPosition;

    private float _timerShot;
    

    // Start is called before the first frame update
    void Start()
    {
        _timerShot = 0f;

        Debug.Log("minX: " + cameraSelected.pixelRect.x + " maxX: " + cameraSelected.pixelWidth);
        Debug.Log("miny: " + cameraSelected.pixelHeight * minDistanceFromPlayer + " maxY: " + cameraSelected.pixelHeight/2);

        float searchingPositionX = Random.Range(cameraSelected.pixelRect.x, cameraSelected.pixelWidth);
        Debug.Log("selectedX: " + searchingPositionX);
        float searchingPositionY = Random.Range(cameraSelected.pixelHeight*minDistanceFromPlayer, cameraSelected.pixelHeight/2);
        Debug.Log("selectedY: " + searchingPositionY);
        _destinationPosition = cameraSelected.ScreenToWorldPoint(new Vector3(searchingPositionX, searchingPositionY, 0f));
        _destinationPosition.z = 0f;
        Debug.Log(_destinationPosition);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _destinationPosition) < speedMovement) {
            if (_timerShot <= 0f) {
                _timerShot = shotFrequency;

                Vector3 vectorToTarget = playerObject.transform.position - transform.position; //queste due righe ci consentono di ruotare
                Quaternion q = Quaternion.LookRotation(Vector3.forward, vectorToTarget);        //il proiettile nella direzione del giocatore

                GameObject newEnemyBullet = Instantiate(enemyBullet, transform.position, q);
                //newEnemyBullet.transform.position = transform.position + new Vector3(0f,-0.25f,0f);


            }
            _timerShot -= Time.fixedDeltaTime;
            return;
        }

        //move the enemy towards the destinationPosition selected in the correct side of the player selected
        transform.position=Vector3.MoveTowards(transform.position, _destinationPosition, speedMovement);
    }
}
