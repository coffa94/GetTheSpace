using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundsController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        //destroy bullets when they touch the screenbounds (exit from the screen view)
        if (collision.gameObject.GetComponent<Bullet>() || collision.gameObject.GetComponent<BulletEnemy>()) {
            Destroy(collision.gameObject);
        }
    }
}
