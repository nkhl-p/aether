using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawnner;

    void Start()
    {
        groundSpawnner = FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other) {
        groundSpawnner.SpawnTile(true);
        Destroy(gameObject, 2); // this will destroy the object 2 seconds after the player hits the trigger
    }

}
