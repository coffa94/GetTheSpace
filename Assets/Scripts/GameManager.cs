using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //pattern di programmazione singleton
    public static GameManager gameManager;

    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private List<GameObject> meteorList;
    [SerializeField] private Transform player1Transform;
    [SerializeField] private Camera cameraPlayer1;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private Camera cameraPlayer2;
    [SerializeField] private float timeEnemySpawn;
    [SerializeField] private float timeMeteorSpawn;
    [SerializeField] private float minDistanceFromPlayer = 0.2f;
    [SerializeField] private float percentageScreenToAvoid;
    [SerializeField] private PlayerData player1Data;
    [SerializeField] private PlayerData player2Data;
    [SerializeField] private int maxEnemiesNumber;

    //enemy variables
    private float _timerNewEnemy;
    private Vector3 _positionXNewEnemy;
    private Vector3 _destinationNewEnemy;
    private int enemies1Count;
    private int enemies2Count;


    //meteor variables
    private Vector3 _positionXNewMeteor;
    private float _timerNewMeteor;


    private float approximationPixelPositionX; //in proportion of screen size


    // Start is called before the first frame update
    void Start()
    {
        //utilizzo pattern di programmazione singleton
        gameManager = this;

        //set the approximationPixelPositionX proportionally to the screen size width, enemies won't reach this pixelPositionX
        approximationPixelPositionX = cameraPlayer1.pixelWidth * percentageScreenToAvoid / 100f;

        //new enemy creation
        CreateEnemy();
        _timerNewEnemy = timeEnemySpawn;

        //new meteor creation
        CreateMeteor();
        _timerNewMeteor = timeMeteorSpawn;

    }

    // Update is called once per frame
    void Update()
    {
        //every timeEnemySpawn create new enemies for the 2 sides
        if (_timerNewEnemy <= 0f) {
            _timerNewEnemy = timeEnemySpawn;

            CreateEnemy();

        }
        _timerNewEnemy -= Time.deltaTime;

        //every timeMeteorSpawn create new meteors for the 2 sides
        if (_timerNewMeteor <= 0f) {
            _timerNewMeteor = timeMeteorSpawn;

            CreateMeteor();

        }
        _timerNewMeteor -= Time.deltaTime;
    }

    void CreateEnemy() {
        //choosing a positionToMove for the new enemy
        //Debug.Log("minX: " + (cameraPlayer1.pixelRect.x + approximationPixelPositionX) + " maxX: " + (cameraPlayer1.pixelWidth - approximationPixelPositionX));
        //Debug.Log("miny: " + (cameraPlayer1.pixelHeight * minDistanceFromPlayer) + " maxY: " + (cameraPlayer1.pixelHeight / 2));

        ChooseEnemyDestination();

        _positionXNewEnemy=ChoosePositionXToSpawn();


        //check enemies1' spaceship number in the game
        if (enemies1Count < maxEnemiesNumber) {
            //create new enemy1 (for player1)
            GameObject newEnemy1 = Instantiate(enemyList[0], _positionXNewEnemy, new Quaternion());
            newEnemy1.GetComponent<EnemyController>().PlayerTargetTransform = player1Transform;
            newEnemy1.GetComponent<EnemyController>().DestinationPosition = _destinationNewEnemy;

            //increment counter of enemies1 in the game
            enemies1Count++;
        }

        //invertion of y component for the enemy2 for player2)
        _destinationNewEnemy.y = -_destinationNewEnemy.y;

        //check enemies2' spaceship number in the game
        if (enemies2Count < maxEnemiesNumber) {
            //GameObject newEnemy2 = Instantiate(newEnemy1);
            GameObject newEnemy2 = Instantiate(enemyList[0], _positionXNewEnemy, new Quaternion(0f, 0f, 180f, 0f));
            newEnemy2.GetComponent<EnemyController>().PlayerTargetTransform = player2Transform;
            newEnemy2.GetComponent<EnemyController>().DestinationPosition = _destinationNewEnemy;

            //increment counter of enemies2 in the game
            enemies2Count++;
        }

       

    }

    void CreateMeteor() {
        //choosing a positionToMove for the new enemy
        //Debug.Log("minX: " + (cameraPlayer1.pixelRect.x + approximationPixelPositionX) + " maxX: " + (cameraPlayer1.pixelWidth - approximationPixelPositionX));
        //Debug.Log("miny: " + (cameraPlayer1.pixelHeight * minDistanceFromPlayer) + " maxY: " + (cameraPlayer1.pixelHeight / 2));

        _positionXNewMeteor=ChoosePositionXToSpawn();


        GameObject newMeteor1 = Instantiate(meteorList[0], _positionXNewMeteor, new Quaternion());
        newMeteor1.GetComponent<MeteorController>().SetPlayer(1);


        GameObject newMeteor2 = Instantiate(newMeteor1);
        //rotate by 180 degree the second enemy to point to player2, it is possible because it has a kinematic rigidbody2D
        newMeteor2.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
        newMeteor2.GetComponent<MeteorController>().SetPlayer(2);

    }

    private void ChooseEnemyDestination() {
        float searchingPositionX = Random.Range(cameraPlayer1.pixelRect.x + approximationPixelPositionX, cameraPlayer1.pixelWidth - approximationPixelPositionX);
        //Debug.Log("selectedX: " + searchingPositionX);
        float searchingPositionY = Random.Range(cameraPlayer1.pixelHeight * minDistanceFromPlayer, cameraPlayer1.pixelHeight / 2);
        //Debug.Log("selectedY: " + searchingPositionY);
        _destinationNewEnemy = cameraPlayer1.ScreenToWorldPoint(new Vector3(searchingPositionX, searchingPositionY, 0f));
        _destinationNewEnemy.z = 0f;

        
    }

    private Vector3 ChoosePositionXToSpawn() {
        float startingPositionX = Random.Range(cameraPlayer1.pixelRect.x + approximationPixelPositionX, cameraPlayer1.pixelWidth);
        Vector3 positionXToSpawn = cameraPlayer1.ScreenToWorldPoint(new Vector3(startingPositionX, cameraPlayer1.pixelHeight / 2, 0f));
        positionXToSpawn.z = 0f;

        return positionXToSpawn;
    }

    public void DecreaseEnemies(int playerNumber) {
        //decrease enemies for playerNumber player
        if (playerNumber == 1) {
            enemies1Count--;
        }
        if (playerNumber == 2) {
            enemies2Count--;
        }
    }


}
