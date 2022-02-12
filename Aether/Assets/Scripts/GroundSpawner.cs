using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour {
    [SerializeField] GameObject groundTileBlue;
    [SerializeField] GameObject groundTileRed;
    [SerializeField] GameObject groundTileGreen;
    [SerializeField] GameObject groundTileYellow;
    [SerializeField] GameObject groundTileFinish;
    Vector3 nextSpawnPoint;

    List<(int X_Value, int Z_Value, string Name, bool IsPowerUpEnabled)> pathCoordinates = new List<(int X_Value, int Z_Value, string Name, bool IsPowerUpEnabled)>
      {
        (0,0,"Blue",false),
        (0,10,"Blue",false),
        (0,20,"Blue",false),
        (0,30,"Blue",false),
        (0,40,"Blue",false),
        (0,50,"Blue",false),
        (0,60,"Blue",false),
        (10,60,"Red",false),
        (0,70,"Blue",false),
        (10,70, "Red",false),
        (10,80, "Red",false),
        (10,90,"Red",false),
        (-10,80,"Green",false),
        (-10,90,"Green",false),
        (-10,100,"Green",true),
        (-10,110,"Green",false),
        (-10,120,"Green",false),
        (-10,130,"Green",false),
        (-10,140,"Green",false),
        (-10,150,"Green",false),
        (0,170, "Red",false),
        (0,180, "Red",true),
        (0,190, "Red",false),
        (0,200, "Green",false),
        (0,220,"Blue",false),
        (10,220,"Yellow",false),
        (0,230,"Blue",false),
        (10,230,"Yellow",true),
        (0,240,"Blue",false),
        (10,240,"Yellow",true),
        (0,250,"Blue",false),
        (0,260,"Blue",false),
        (0,270,"Blue",false),
        (0,290,"Green",false),
        (0,300,"Green",false),
        (0,310,"Green",false),
        (0,320,"Finish",false),
      };
    int i = 0;
    HashSet<float> powerUpLocationSet = new HashSet<float>();
    bool isPowerUpEnabled = false;
    [SerializeField] float obstacleSpawningChance = 0.3f;


    public void SpawnTile(bool spawnItems) {
        int curr = (int)nextSpawnPoint.x;

        GameObject tempGroundTileObject = null;

        var color = pathCoordinates[i].Name;
        isPowerUpEnabled = pathCoordinates[i].IsPowerUpEnabled;

        switch (curr) {
            case 0:
                switch (color) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                break;
            case 10:
                switch (color) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                break;
            case -10:
                switch (color) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                break;
        }

        if (isPowerUpEnabled) {
            tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups();
        }

        nextSpawnPoint = tempGroundTileObject.transform.GetChild(1).transform.position;
        nextSpawnPoint.x = pathCoordinates[i + 1].X_Value;
        nextSpawnPoint.z = pathCoordinates[i + 1].Z_Value;
        i++;

        float randomObstacleChance = Random.Range(0f, 1f);

        if (spawnItems && randomObstacleChance > obstacleSpawningChance) {
            tempGroundTileObject.GetComponent<GroundTile>().SpawnObstacles();
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
