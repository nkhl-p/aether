using UnityEngine;
using System.Text.RegularExpressions;

public class PowerUp : MonoBehaviour {

    public float turnSpeed = 90f;
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.GetComponent<Obstacle>() != null) {
            Debug.Log("Power-up spawned inside an obstacle. Destroying power-up now");
            Destroy(gameObject);
            return;
        }

        // check whether the object we collided with is the player and if not, do not execute the function/
        if (other.gameObject.name != "Player") {
            //Debug.Log("Something other than the player collided with the powerup");
            return;
        }

        if (other.CompareTag("Player")) {
            Debug.Log("Object collided with tag: " + other.gameObject.tag);
            Pickup();
        }
    }

    void Pickup() {
        // Play audio effect for power-up collection
        FindObjectOfType<AudioManager>().Play("PowerUp");

        // Spawn a cool effect using Unity's Particle System
        //Instantiate(pickupEffect, transform.position, transform.rotation);


        // perform some action based on the power-up
        FindObjectOfType<ScoreTimer>().currentTime += 5;

        // destroy the power-up once it has been collected
        Destroy(gameObject);

    }


    // The following function ensures that the powerup will always turn by 90 degrees every second regardless of the framerate
    private void Update() {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

}