using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public float turnSpeed = 90f;
    public GameObject pickupEffect;

    int powerUpApplicableDuration = 3;

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
            //StartCoroutine(Pickup("permeability"));
            StartCoroutine(Pickup(other, "time"));
        }
    }

    IEnumerator Pickup(Collider player, string powerUpType) {
        // Play audio effect for power-up collection
        FindObjectOfType<AudioManager>().Play("PowerUp");

        // Spawn a cool effect using Unity's Particle System
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        // perform some action based on the power-up
        switch (powerUpType) {
            case "time":
                FindObjectOfType<ScoreTimer>().currentTime += 5;
                break;

            case "permeability":
                // implementingn the power-up action
                Obstacle o = FindObjectOfType<Obstacle>();
                o.GetComponent<BoxCollider>().isTrigger = true;
                o.test = 2341334;
                Debug.Log("Before" + o.GetComponent<BoxCollider>().isTrigger);
                Debug.Log("Testing the test variable: " + o.test);
                o.GetComponent<Collider>().enabled = false;

                // this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
                yield return new WaitForSeconds(powerUpApplicableDuration);

                // reverting the changes made by the power-up to its original state
                o.GetComponent<BoxCollider>().isTrigger = false;
                Debug.Log("After" + o.GetComponent<BoxCollider>().isTrigger);

                break;

            case "size":
                // increase the player size as part of the power-up action
                player.transform.localScale *= 1.5f;

                // once the power-up has been grabbed, we disable the MeshRenderer and the CapsuleCollider so that the player is not able to interact with that powerup again.
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                Debug.Log("Before" + player.transform.localScale);
                // this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
                yield return new WaitForSeconds(powerUpApplicableDuration);

                // reverting the changes made by the power-up to its original state
                player.transform.localScale /= 1.5f;
                Debug.Log("After" + player.transform.localScale);
                // breaking out of the case.
                break;
        }

        // destroy the power-up once it has been collected
        Destroy(gameObject);

    }

    // The following function ensures that the powerup will always turn by 90 degrees every second regardless of the framerate
    private void Update() {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

}