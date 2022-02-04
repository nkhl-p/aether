using UnityEngine;

public class GroundSpawner : MonoBehaviour {
    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;


    int[] arr = { 0, 0, 0, 0, 20, 20, 20, 20, 20, 0, 0, 0, 0, -20, -20, -40, -40, -40, -20, -20, 0, 0, 40 };
    int i = 0;

    public void SpawnTile(bool spawnItems) {
        int curr = (int)nextSpawnPoint.x;

        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        nextSpawnPoint.x = arr[i + 1];
        i++;

        if (curr == -20 || curr == 20) {
            nextSpawnPoint.z -= 7;
        }

        if (spawnItems) {
            temp.GetComponent<GroundTile>().SpawnObstacles();
        }
    }

    void Start() {
        for (int i = 0; i < 15; i++) {
            if (i > 3) {
                SpawnTile(true);
            } else {
                SpawnTile(false);
            }

        }
    }
}
