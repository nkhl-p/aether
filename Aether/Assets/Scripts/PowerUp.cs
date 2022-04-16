using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public float turnSpeed = 90f;
    public GameObject pickupEffect;

    float powerUpApplicableDuration = 7f;
	int powerUpSpeedBoost = 20;
    int powerUpLeviationSpeed = 15;
    PlayerMovement playerMovement;
    public static bool immunityFlag = false;
    
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

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
            playerMovement.PowerUpPickedUpCounterUpdate();

            switch(gameObject.tag) {
                case "PowerupTime": Pickup(other, PowerupEnums.TIME); break;
                case "PowerupSize": Pickup(other, PowerupEnums.SIZE); break;
                case "PowerupPermeate": Pickup(other, PowerupEnums.PERMEATE); break;
                case "PowerupLevitate": Pickup(other, PowerupEnums.LEVITATE); break;
				case "PowerupSpeed": Pickup(other, PowerupEnums.SPEED); break;
                case "PowerupShoot": Pickup(other, PowerupEnums.SHOOT); break;
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

            case PowerupEnums.PERMEATE:
                StartCoroutine(ApplyPermeability(player));
                break;

            case PowerupEnums.SIZE:
                StartCoroutine(ChangePlayerScale(player));
                break;

            case PowerupEnums.LEVITATE:
                StartCoroutine(LevitatePlayer(player));
                break;

			case PowerupEnums.SPEED:
                StartCoroutine(SpeedupPlayer(player));
                break;

            case PowerupEnums.SHOOT:
                StartCoroutine(EnableGun(player));
                break;
        }
    }

    private void IncreaseTime() {
        FindObjectOfType<ScoreTimer>().currentTime += 5;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        return;
    }

    // This method applies permeability power-up to the player.As of right now, it is only in testing phase. Default state = disabled
    IEnumerator ApplyPermeability(Collider player) {
        // implementing the power-up action
        // player.GetComponent<Rigidbody>().useGravity = false;
        // player.GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        // FindObjectOfType<ScoreTimer>().startLevitationTimer(powerUpApplicableDuration);

        immunityFlag = true;

		FindObjectOfType<ScoreTimer>().startLevitationTimer(powerUpApplicableDuration);
		// this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
        yield return new WaitForSeconds(powerUpApplicableDuration);

        // reverting the changes made by the power-up to its original state
        // player.GetComponent<CapsuleCollider>().isTrigger = false;
        // player.GetComponent<Rigidbody>().useGravity = true;

        immunityFlag = false;
        
    }

    // This method applies change in player scale power-up to the player. Default state = disabled
    IEnumerator ChangePlayerScale(Collider player) {
        // increase the player size as part of the power-up action
        player.transform.localScale *= 1.5f;
        Obstacle.IsSizePowerUpEnabled = true;

        // once the power-up has been grabbed, we disable the MeshRenderer and the CapsuleCollider so that the player is not able to interact with that powerup again.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        FindObjectOfType<ScoreTimer>().startLevitationTimer(powerUpApplicableDuration);
		// this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
        yield return new WaitForSeconds(powerUpApplicableDuration);

        // reverting the changes made by the power-up to its original state
        player.transform.localScale /= 1.5f;
        Obstacle.IsSizePowerUpEnabled = false ;

        // breaking out of the case.
    }

    // This method applies change in player position along the y-axis to make it resemble like it is levitating . Default state = disabled
    IEnumerator LevitatePlayer(Collider player) {
        // increase the player size as part of the power-up action
        Vector3 currentPos = player.transform.position;
        Vector3 playerNewPos = new Vector3(currentPos.x, 3, currentPos.z);
        player.transform.position = playerNewPos;
        player.GetComponent<Rigidbody>().useGravity = false;
        playerMovement.powerUpSpeed = powerUpLeviationSpeed;

        // once the power-up has been grabbed, we disable the MeshRenderer and the CapsuleCollider so that the player is not able to interact with that powerup again.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        FindObjectOfType<ScoreTimer>().startLevitationTimer(powerUpApplicableDuration);
		// this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
        yield return new WaitForSeconds(powerUpApplicableDuration);

        // reverting the changes made by the power-up to its original state
        Debug.Log("Before position: " + player.transform.position);
        Vector3 currentPosAfterLevitation = player.transform.position;
        Vector3 playerNewPosAfter = new Vector3(currentPosAfterLevitation.x, 1, currentPosAfterLevitation.z);
        player.transform.position = playerNewPosAfter;
        player.GetComponent<Rigidbody>().useGravity = true;
        playerMovement.powerUpSpeed = 0;
        Debug.Log("After position: " + player.transform.position);

        // breaking out of the case.
    }

	// This method applies change in player's speed along the z-axis to make it resemble like it is has gained speed
    IEnumerator SpeedupPlayer(Collider player) {
        // increase the player size as part of the power-up action
		playerMovement.powerUpSpeed = powerUpSpeedBoost;
        
        // once the power-up has been grabbed, we disable the MeshRenderer and the CapsuleCollider so that the player is not able to interact with that powerup again.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        FindObjectOfType<ScoreTimer>().startLevitationTimer(powerUpApplicableDuration);
        // this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
        yield return new WaitForSeconds(powerUpApplicableDuration);

        // reverting the changes made by the power-up to its original state
        playerMovement.powerUpSpeed = 0;
        // breaking out of the case.
    }

    // This method enables the gun functionality
    IEnumerator EnableGun(Collider player) {
        Debug.Log("Enable gun powerup called!");
        // increase the player size as part of the power-up action
        Gun.IsGunEnabled = true;

        // once the power-up has been grabbed, we disable the MeshRenderer and the CapsuleCollider so that the player is not able to interact with that powerup again.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        FindObjectOfType<ScoreTimer>().startLevitationTimer(powerUpApplicableDuration);
        // this allows the coroutine to be applicable for 'powerUpApplicableDuration' time duration only
        yield return new WaitForSeconds(powerUpApplicableDuration);

        // reverting the changes made by the power-up to its original state
        Gun.IsGunEnabled = false;
        // breaking out of the case.
    }

    // The following function ensures that the powerup will always turn by 90 degrees every second regardless of the framerate
    private void Update() {
        switch (gameObject.tag) {
            case "PowerupTime": transform.Rotate(0, turnSpeed * Time.deltaTime, 0); break;
            case "PowerupSize": transform.Rotate(0, 0, turnSpeed * Time.deltaTime); break;
            case "PowerupPermeate": transform.Rotate(0, 0, turnSpeed * Time.deltaTime); break;
            case "PowerupLevitate": transform.Rotate(0, turnSpeed * Time.deltaTime, 0); break;
            case "PowerupSpeed": transform.Rotate(0, 0, turnSpeed * Time.deltaTime); break;
            case "PowerupShoot": transform.Rotate(0, turnSpeed * Time.deltaTime, 0); break;
        }
    }

}