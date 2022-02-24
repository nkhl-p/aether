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

    List<(int X_Value, int Z_Value, string Name, PowerupEnums powerupEnums)> pathCoordinates = new List<(int X_Value, int Z_Value, string Name, PowerupEnums powerupEnums)>
      {
        (0,0,"Blue",PowerupEnums.NONE),
        (0,10,"Blue",PowerupEnums.NONE),
        (0,20,"Blue",PowerupEnums.NONE),
        (0,30,"Blue",PowerupEnums.NONE),
        (0,40,"Blue",PowerupEnums.TIME),
        (0,50,"Blue",PowerupEnums.NONE),
        (0,60,"Blue",PowerupEnums.NONE),

        (0,80,"Blue",PowerupEnums.NONE),
        (0,90,"Blue",PowerupEnums.NONE),
        (0,100,"Blue",PowerupEnums.SIZE),
        (0,110,"Blue",PowerupEnums.NONE),

        (10,130,"Red",PowerupEnums.NONE),
        (-10,130,"Green",PowerupEnums.NONE),
        (10,140,"Red",PowerupEnums.NONE),
        (-10,140,"Green",PowerupEnums.NONE),
        (10,150,"Red",PowerupEnums.NONE),
        (-10,150,"Green",PowerupEnums.NONE),
        (10,160,"Red",PowerupEnums.NONE),
        (-10,160,"Green",PowerupEnums.NONE),
        (10,170,"Blue",PowerupEnums.NONE),
        (-10,170,"Green",PowerupEnums.NONE),
        (10,180,"Blue",PowerupEnums.NONE),
        (-10,180,"Green",PowerupEnums.NONE),

        (0,200,"Red",PowerupEnums.NONE),
        (0,210,"Red",PowerupEnums.NONE),
        (0,220,"Red",PowerupEnums.NONE),
        (0,230,"Blue",PowerupEnums.NONE),
        (0,240,"Blue",PowerupEnums.NONE),
        (0,250,"Blue",PowerupEnums.NONE),
        (0,260,"Blue",PowerupEnums.NONE),
        (0,270,"Blue",PowerupEnums.NONE),
        (0,280,"Blue",PowerupEnums.NONE),

        (0,300,"Red",PowerupEnums.NONE),
        (-10,300,"Green",PowerupEnums.NONE),
        (10,300,"Yellow",PowerupEnums.NONE),
        (0,310,"Red",PowerupEnums.NONE),
        (-10,310,"Green",PowerupEnums.NONE),
        (10,310,"Yellow",PowerupEnums.NONE),
        (0,320,"Red",PowerupEnums.NONE),
        (-10,320,"Green",PowerupEnums.NONE),
        (10,320,"Yellow",PowerupEnums.NONE),
        (0,330,"Red",PowerupEnums.NONE),
        (-10,330,"Green",PowerupEnums.NONE),
        (10,330,"Yellow",PowerupEnums.NONE),
        (0,340,"Red",PowerupEnums.NONE),
        (-10,340,"Green",PowerupEnums.NONE),
        (10,340,"Yellow",PowerupEnums.NONE),

        (0,360,"Yellow",PowerupEnums.NONE),
        (-10,360,"Blue",PowerupEnums.NONE),
        (10,360,"Green",PowerupEnums.NONE),
        (0,370,"Yellow",PowerupEnums.NONE),
        (-10,370,"Blue",PowerupEnums.NONE),
        (10,370,"Green",PowerupEnums.NONE),
        (0,380,"Yellow",PowerupEnums.NONE),
        (-10,380,"Blue",PowerupEnums.NONE),
        (10,380,"Green",PowerupEnums.NONE),
        (0,390,"Yellow",PowerupEnums.NONE),
        (-10,390,"Blue",PowerupEnums.NONE),
        (10,390,"Green",PowerupEnums.NONE),
        (0,400,"Yellow",PowerupEnums.NONE),
        (-10,400,"Blue",PowerupEnums.NONE),
        (10,400,"Green",PowerupEnums.NONE),
        (-10,410,"Blue",PowerupEnums.NONE),
        (10,410,"Green",PowerupEnums.NONE),
        (-10,420,"Blue",PowerupEnums.NONE),
        (10,420,"Green",PowerupEnums.NONE),

        (0,440,"Blue",PowerupEnums.NONE),
        (0,450,"Blue",PowerupEnums.NONE),
        (0,460,"Blue",PowerupEnums.NONE),
        (0,470,"Blue",PowerupEnums.NONE),
        (0,480,"Blue",PowerupEnums.NONE),
        (0,490,"Blue",PowerupEnums.NONE),
        (0,500,"Blue",PowerupEnums.NONE),
        (0,510,"Blue",PowerupEnums.NONE),
        (0,520,"Blue",PowerupEnums.NONE),
        (0,530,"Blue",PowerupEnums.NONE),

        (0,550,"Green",PowerupEnums.NONE),
        (0,550,"Blue",PowerupEnums.NONE),
        (0,560,"Green",PowerupEnums.NONE),
        (0,560,"Blue",PowerupEnums.NONE),
        (0,570,"Blue",PowerupEnums.NONE),
        (0,580,"Blue",PowerupEnums.NONE),
        (0,590,"Blue",PowerupEnums.NONE),
        (0,600,"Blue",PowerupEnums.NONE),

        (0,610,"Finish",PowerupEnums.NONE),
        (0,620,"Finish",PowerupEnums.NONE),
        (0,630,"Finish",PowerupEnums.NONE),
        (0,640,"Finish",PowerupEnums.NONE),
        (0,650,"Finish",PowerupEnums.NONE),
        (0,660,"Finish",PowerupEnums.NONE),
        (0,670,"Finish",PowerupEnums.NONE),

      };
    int i = 0;

    string powerupType = "";
    bool isPowerUpEnabled = true;
    [SerializeField] float obstacleSpawningChance = 0.3f;


    public void SpawnTile(bool spawnItems) {
        int curr = (int)nextSpawnPoint.x;

        GameObject tempGroundTileObject = null;

        //Debug.Log("Current Tile Details - " + pathCoordinates[i].X_Value + " " + pathCoordinates[i].Z_Value + " " + pathCoordinates[i].Name + " " + pathCoordinates[i].IsPowerUpEnabled);
        var color = pathCoordinates[i].Name;
        powerupType = pathCoordinates[i].powerupEnums.GetString();
        
        if (powerupType.Equals("")) {
            isPowerUpEnabled = false ;
        }
        //Debug.Log("This is the power up type: " + powerupType + " and power up is: " + isPowerUpEnabled);

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

        switch (powerupType) {
            case "Time":
                tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups(PowerupEnums.TIME.GetString());
                break;
            case "Size":
                tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups(PowerupEnums.SIZE.GetString());
                break;
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
