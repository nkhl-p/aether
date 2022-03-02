using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision) {
        // Kill the player
        if (collision.gameObject.name == "Player") {
            AudioManager temp = FindObjectOfType<AudioManager>();
            temp.Play(SoundEnums.COLLISION.GetString());
            temp.StopPlaying(SoundEnums.THEME.GetString());
            playerMovement.Die();
        }

    }
}
