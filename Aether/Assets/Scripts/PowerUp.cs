using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public float turnSpeed = 90f;
    public GameObject pickupEffect;

    float powerUpApplicableDuration = 7f;
    PlayerMovement playerMovement;
    
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("This is the name of game object: " + gameObject);

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
            playerMovement.PowerUpPickedUpCounterUpdate();

            switch(gameObject.tag) {
                case "PowerupTime": Pickup(other, PowerupEnums.TIME); break;
                case "PowerupSize": Pickup(other, PowerupEnums.SIZE); break;
                case "PowerupPermeability": Pickup(other, PowerupEnums.PERMEABILITY); break;
            }
        }
    }

    void Pickup(Collider player, PowerupEnums powerUpType) {
        // Play audio effect for power-up collection
        FindObjectOfType<AudioManager>().Play(SoundEnums.POWERUP.GetString());

        // Spawn a cool effect using Unity's Particle System
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // perform some action based on the power-up
        PerformPowerUpAction(player, powerUpType);

        // destroy the power-up once it has been collected
        //Destroy(gameObject);
    }

    private void PerformPowerUpAction(Collider player, PowerupEnums powerUpType) {
        switch (powerUpType) {
            case PowerupEnums.TIME:
                IncreaseTime();
                break;

            case PowerupEnums.PERMEABILITY:
                StartCoroutine(ApplyPermeability(player));
                break;

            case PowerupEnums.SIZE:
                StartCoroutine(ChangePlayerScale(player));
                break;
        }
    }

    private void IncreaseTime() {
        FindObjectOfType<ScoreTimer>().currentTime += 5;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        return;
    }

    /// <summary>
    /// This method applies permeability power-up to the player. As of right now, it is only in testing phase. Default state = disabled
    /// </summary>
    /// <param name="player"></param>
    /// <returns>IEnumerator</returns>
    IEnumerator ApplyPermeability(Collider player) {
        // implementing the power-up action
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
    }

    /// <summary>
    /// This method applies change in player scale power-up to the player. Default state = disabled
    /// </summary>
    /// <param name="player"></param>
    /// <returns>IEnumerator</returns>
    IEnumerator ChangePlayerScale(Collider player) {
        // increase the player size as part of the power-up action
        player.transform.localScale *= 1.5f;

        // once the power-up has been grabbed, we disable the MeshRenderer and the CapsuleCollider so that the player is not able to interact with that powerup again.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        Debug.Log("Before scale: " + player.transform.localScale);

        // this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
        yield return new WaitForSeconds(powerUpApplicableDuration);

        // reverting the changes made by the power-up to its original state
        Debug.Log("Before scale: " + player.transform.localScale);
        player.transform.localScale /= 1.5f;
        Debug.Log("After scale: " + player.transform.localScale);

        // breaking out of the case.
    }

    // The following function ensures that the powerup will always turn by 90 degrees every second regardless of the framerate
    private void Update() {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

}