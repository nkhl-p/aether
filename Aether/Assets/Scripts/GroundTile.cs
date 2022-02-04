using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    [SerializeField] GameObject obstaclePrefab;

    void Start()
    {
        groundSpawner = FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other) {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2); // this will destroy the object 2 seconds after the player hits the trigger
    }

    public void SpawnObstacles() {
        // Choose a random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Spawn the obstacle at the position
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);

    }
}
