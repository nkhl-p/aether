using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision) {
        // Kill the player
        if (collision.gameObject.name == "Player") {
            playerMovement.Die();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
