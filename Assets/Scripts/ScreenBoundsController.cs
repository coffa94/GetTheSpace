using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundsController : MonoBehaviour
{
    [SerializeField] private PlayerData[] playerDataArray;

    private int playerOne = 0;
    private int playerTwo = 1;


    private void OnTriggerEnter2D(Collider2D collision) {
        //destroy bullets when they touch the screenbounds (exit from the screen view)
        if (collision.gameObject.GetComponent<Bullet>() || collision.gameObject.GetComponent<BulletEnemy>()) {
            Destroy(collision.gameObject);
        }

        //destroy obstacle when they touch the screenbounds
        if (collision.gameObject.GetComponent<MeteorController>()) {
            MeteorController meteorControllerCollision = collision.gameObject.GetComponent<MeteorController>();

            //decrease player's life choosing the player by the collision's point
            if (collision.transform.position.y > 0) {
                playerDataArray[playerTwo].DecreaseLife(collision.gameObject.GetComponent<MeteorController>().DamageMeteor);
            } else {
                playerDataArray[playerOne].DecreaseLife(collision.gameObject.GetComponent<MeteorController>().DamageMeteor);
            }

            //destroy obstacle that have a collision with bounds
            Destroy(collision.gameObject);
            
        }
        

    }
}
