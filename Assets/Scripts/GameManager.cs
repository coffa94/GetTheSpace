using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private Transform player1Transform;
    [SerializeField] private Camera cameraPlayer1;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private Camera cameraPlayer2;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float minDistanceFromPlayer = 0.2f;
    [SerializeField] private float percentageScreenToAvoid;

    private float _timerNewEnemy;
    private Vector3 _positionXNewEnemy;
    private Vector3 _destinationNewEnemy;

    private float approximationPixelPositionX; //in proportion of screen size


    // Start is called before the first frame update
    void Start()
    {
        //set the approximationPixelPositionX proportionally to the screen size width, enemies won't reach this pixelPositionX
        approximationPixelPositionX = cameraPlayer1.pixelWidth * percentageScreenToAvoid / 100f;

        CreateEnemy();
        _timerNewEnemy = timeSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerNewEnemy <= 0f) {
            _timerNewEnemy = timeSpawn;
            CreateEnemy();

        }
        _timerNewEnemy -= Time.deltaTime;
    }

    void CreateEnemy() {
        //choosing a positionToMove for the new enemy
        //Debug.Log("minX: " + (cameraPlayer1.pixelRect.x + approximationPixelPositionX) + " maxX: " + (cameraPlayer1.pixelWidth - approximationPixelPositionX));
        //Debug.Log("miny: " + (cameraPlayer1.pixelHeight * minDistanceFromPlayer) + " maxY: " + (cameraPlayer1.pixelHeight / 2));

        ChooseEnemyDestination();

        ChooseEnemyPositionX();
        

        GameObject newEnemy1 = Instantiate(enemyList[0], _positionXNewEnemy, new Quaternion());
        newEnemy1.GetComponent<EnemyController>().PlayerTargetTransform = player1Transform;
        newEnemy1.GetComponent<EnemyController>().DestinationPosition = _destinationNewEnemy;

        //invertion of y component for the second enemy
        _destinationNewEnemy.y = -_destinationNewEnemy.y;

        GameObject newEnemy2 = Instantiate(newEnemy1);
        //rotate by 180 degree the second enemy to point to player2, it is possible because it has a kinematic rigidbody2D
        newEnemy2.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
        newEnemy2.GetComponent<EnemyController>().PlayerTargetTransform = player2Transform;
        newEnemy2.GetComponent<EnemyController>().DestinationPosition = _destinationNewEnemy;

    }

    private void ChooseEnemyDestination() {
        float searchingPositionX = Random.Range(cameraPlayer1.pixelRect.x + approximationPixelPositionX, cameraPlayer1.pixelWidth - approximationPixelPositionX);
        //Debug.Log("selectedX: " + searchingPositionX);
        float searchingPositionY = Random.Range(cameraPlayer1.pixelHeight * minDistanceFromPlayer, cameraPlayer1.pixelHeight / 2);
        //Debug.Log("selectedY: " + searchingPositionY);
        _destinationNewEnemy = cameraPlayer1.ScreenToWorldPoint(new Vector3(searchingPositionX, searchingPositionY, 0f));
        _destinationNewEnemy.z = 0f;

        
    }

    private void ChooseEnemyPositionX() {
        float startingPositionX = Random.Range(cameraPlayer1.pixelRect.x + approximationPixelPositionX, cameraPlayer1.pixelWidth);
        _positionXNewEnemy = cameraPlayer1.ScreenToWorldPoint(new Vector3(startingPositionX, cameraPlayer1.pixelHeight / 2, 0f));
        _positionXNewEnemy.z = 0f;
    }
}
