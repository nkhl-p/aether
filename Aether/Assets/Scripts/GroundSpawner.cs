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

        (0,80,"Blue",false),
        (0,90,"Blue",false),
        (0,100,"Blue",false),
        (0,110,"Blue",false),

        (10,130,"Red",false),
        (-10,130,"Green",false),
        (10,140,"Red",false),
        (-10,140,"Green",false),
        (10,150,"Red",false),
        (-10,150,"Green",false),
        (10,160,"Red",false),
        (-10,160,"Green",false),
        (10,170,"Blue",false),
        (-10,170,"Green",false),
        (10,180,"Blue",false),
        (-10,180,"Green",false),

        (0,200,"Red",false),
        (0,210,"Red",false),
        (0,220,"Red",false),
        (0,230,"Blue",false),
        (0,240,"Blue",false),
        (0,250,"Blue",false),
        (0,260,"Blue",false),
        (0,270,"Blue",false),
        (0,280,"Blue",false),

        (0,300,"Red",false),
        (-10,300,"Green",false),
        (10,300,"Yellow",false),
        (0,310,"Red",false),
        (-10,310,"Green",false),
        (10,310,"Yellow",false),
        (0,320,"Red",false),
        (-10,320,"Green",false),
        (10,320,"Yellow",false),
        (0,330,"Red",false),
        (-10,330,"Green",false),
        (10,330,"Yellow",false),
        (0,340,"Red",false),
        (-10,340,"Green",false),
        (10,340,"Yellow",false),

        (0,360,"Yellow",false),
        (-10,360,"Blue",false),
        (10,360,"Green",false),
        (0,370,"Yellow",false),
        (-10,370,"Blue",false),
        (10,370,"Green",false),
        (0,380,"Yellow",false),
        (-10,380,"Blue",false),
        (10,380,"Green",false),
        (0,390,"Yellow",false),
        (-10,390,"Blue",false),
        (10,390,"Green",false),
        (0,400,"Yellow",false),
        (-10,400,"Blue",false),
        (10,400,"Green",false),
        (-10,410,"Blue",false),
        (10,410,"Green",false),
        (-10,420,"Blue",false),
        (10,420,"Green",false),

        (0,440,"Blue",false),
        (0,450,"Blue",false),
        (0,460,"Blue",false),
        (0,470,"Blue",false),
        (0,480,"Blue",false),
        (0,490,"Blue",false),
        (0,500,"Blue",false),
        (0,510,"Blue",false),
        (0,520,"Blue",false),
        (0,530,"Blue",false),

        (0,550,"Green",false),
        (0,550,"Blue",false),
        (0,560,"Green",false),
        (0,560,"Blue",false),
        (0,570,"Blue",false),
        (0,580,"Blue",false),
        (0,590,"Blue",false),
        (0,600,"Blue",false),

        (0,610,"Finish",false),
        (0,620,"Finish",false),
        (0,630,"Finish",false),
        (0,640,"Finish",false),
        (0,650,"Finish",false),
        (0,660,"Finish",false),
        (0,670,"Finish",false),

      };
    int i = 0;

    bool isPowerUpEnabled = false;
    [SerializeField] float obstacleSpawningChance = 0.3f;


    public void SpawnTile(bool spawnItems) {
        int curr = (int)nextSpawnPoint.x;

        GameObject tempGroundTileObject = null;

        //Debug.Log("Current Tile Details - " + pathCoordinates[i].X_Value + " " + pathCoordinates[i].Z_Value + " " + pathCoordinates[i].Name + " " + pathCoordinates[i].IsPowerUpEnabled);
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
            //Debug.Log("Spawning Power at - " + pathCoordinates[i].X_Value + " " + pathCoordinates[i].Z_Value);
            tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups();
        }

        nextSpawnPoint = tempGroundTileObject.transform.GetChild(1).transform.position;
        if (i + 1 < pathCoordinates.Count) {
            nextSpawnPoint.x = pathCoordinates[i + 1].X_Value;
            nextSpawnPoint.z = pathCoordinates[i + 1].Z_Value;
        } else {
            Debug.Log("End of path");
            return;
        }
        

        float randomObstacleChance = Random.Range(0f, 1f);

        if (spawnItems &&
            !isPowerUpEnabled &&
            randomObstacleChance > obstacleSpawningChance &&
            !pathCoordinates[i].Name.Equals("Finish")) {
            tempGroundTileObject.GetComponent<GroundTile>().SpawnObstacles();
        } else {
            // enable the following log only if you wish to debug the tile disappearing issue

            //Debug.Log("Random obstacle not spawned on tile with details " +
                //pathCoordinates[i].X_Value + " " + pathCoordinates[i].Z_Value + " " +
                //pathCoordinates[i].Name + " " + pathCoordinates[i].IsPowerUpEnabled + " " +
                //" as random number is " + randomObstacleChance);
        }
        i++;
    }

    void Start() {
        for (int i = 0; i < 40; i++) {
            if (i > 3) {
                SpawnTile(true);
            } else {
                SpawnTile(false);
            }

        }
    }
}
