using UnityEngine;

public class GroundTile : MonoBehaviour {
    GroundSpawner groundSpawner;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject tallObstaclePrefab;
    [SerializeField] float tallObstacleChance = 0.2f;

    public GameObject powerupsPrefabTime;
    public GameObject powerupsPrefabSize;
    public GameObject powerupsPrefabPermeate;
    public GameObject powerupsPrefabLevitate;
    public GameObject powerupsPrefabSpeed;
    public GameObject powerupsPrefabShoot;


    void Start() {
        groundSpawner = FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other) {
        groundSpawner.SpawnTile(true);
        //Debug.Log("Destroy Called " + gameObject);
        //Destroy(gameObject, 5); // this will destroy the object 2 seconds after the player hits the trigger
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision with: " + collision.gameObject.name);
    }

    public void SpawnObstacles() {
        // Choose which obstacle to spawn
        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if (random < tallObstacleChance) {
            obstacleToSpawn = tallObstaclePrefab;
        }
        // Choose a random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Spawn the obstacle at the position
        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);

    }

    public void SpawnPowerups(string powerupType) {

        switch(powerupType) {
            case "Time":
                GameObject temp1 = Instantiate(powerupsPrefabTime, transform);
                temp1.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
                break;

            case "Size":
                GameObject temp2 = Instantiate(powerupsPrefabSize, transform);
                temp2.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
                break;

            case "Permeate":
                GameObject temp3 = Instantiate(powerupsPrefabPermeate, transform);
                temp3.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
                break;

            case "Levitate":
                GameObject temp4 = Instantiate(powerupsPrefabLevitate, transform);
                temp4.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
                break;
            
            case "Speed":
                GameObject temp5 = Instantiate(powerupsPrefabSpeed, transform);
                temp5.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
                break;

            case "Shoot":
                GameObject temp6 = Instantiate(powerupsPrefabShoot, transform);
                temp6.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
                break;
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider) {
        Vector3 spawnPoint = new Vector3(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );

        // It checks whether the random point we created is inside the collider or not. It should never be called
        if (spawnPoint != collider.ClosestPoint(spawnPoint)) {
            Debug.LogWarning("Random point generated to spawn power-up is not in the collider");
            spawnPoint = GetRandomPointInCollider(collider);
        }

        spawnPoint.y = 2;
        return spawnPoint;
    }
}
