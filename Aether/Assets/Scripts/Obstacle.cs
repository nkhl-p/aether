using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    public int test = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision) {
        // Kill the player
        if (collision.gameObject.name == "Player") {
            AudioManager temp = FindObjectOfType<AudioManager>();
            temp.Play("Collision");
            temp.StopPlaying("SpaceTravel");
            playerMovement.Die();
        }

    }
}
