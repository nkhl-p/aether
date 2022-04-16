using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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
        (0,40,"Blue",PowerupEnums.NONE),
        (0,50,"Blue",PowerupEnums.NONE),
        (0,60,"Blue",PowerupEnums.NONE),

        (0,75,"Blue",PowerupEnums.NONE),
        (0,85,"Blue",PowerupEnums.NONE),
        (0,95,"Blue",PowerupEnums.NONE),
        (0,105,"Blue",PowerupEnums.NONE),
        (0,115,"Blue",PowerupEnums.NONE),

        (-10,130,"Green",PowerupEnums.NONE),
        (10,130,"Yellow",PowerupEnums.NONE),
        (-10,140,"Green",PowerupEnums.NONE),
        (10,140,"Yellow",PowerupEnums.NONE),
        (-10,150,"Green",PowerupEnums.NONE),
        (10,150,"Yellow",PowerupEnums.NONE),
        (-10,160,"Green",PowerupEnums.NONE),
        (10,160,"Yellow",PowerupEnums.SIZE),
        (-10,170,"Green",PowerupEnums.NONE),
        (10,170,"Yellow",PowerupEnums.NONE),

        (0,185,"Blue",PowerupEnums.NONE),
        (0,195,"Blue",PowerupEnums.NONE),
        (0,205,"Blue",PowerupEnums.NONE),
        (10,215,"Blue",PowerupEnums.NONE),
        (10,225,"Blue",PowerupEnums.NONE),
        (10,235,"Blue",PowerupEnums.NONE),
        (10,245,"Blue",PowerupEnums.NONE),

        (0,260,"Red",PowerupEnums.NONE),
        (-10,260,"Green",PowerupEnums.NONE),
        (0,270,"Red",PowerupEnums.SIZE),
        (-10,270,"Green",PowerupEnums.NONE),
        (0,280,"Red",PowerupEnums.NONE),
        (-10,280,"Green",PowerupEnums.NONE),
        (0,290,"Red",PowerupEnums.NONE),
        (-10,290,"Green",PowerupEnums.NONE),
        (-10,300,"Green",PowerupEnums.NONE),
        (-10,310,"Green",PowerupEnums.NONE),
        (-10,320,"Green",PowerupEnums.NONE),
        (-10,330,"Green",PowerupEnums.TIME),
        (-10,340,"Green",PowerupEnums.NONE),
        (-10,350,"Green",PowerupEnums.NONE),

        (-10,365,"Red",PowerupEnums.NONE),
        (0,365,"Yellow",PowerupEnums.NONE),
        (10,365,"Blue",PowerupEnums.NONE),
        (-10,375,"Red",PowerupEnums.NONE),
        (0,375,"Yellow",PowerupEnums.NONE),
        (10,375,"Blue",PowerupEnums.NONE),
        (-10,385,"Red",PowerupEnums.NONE),
        (0,385,"Yellow",PowerupEnums.NONE),
        (10,385,"Blue",PowerupEnums.NONE),
        (-10,395,"Red",PowerupEnums.SPEED),
        (0,395,"Yellow",PowerupEnums.NONE),
        (10,395,"Blue",PowerupEnums.SIZE),
        (0,405,"Yellow",PowerupEnums.NONE),
        (10,405,"Blue",PowerupEnums.NONE),
        (0,415,"Yellow",PowerupEnums.NONE),
        (10,415,"Blue",PowerupEnums.NONE),
        (0,425,"Yellow",PowerupEnums.NONE),
        (10,425,"Blue",PowerupEnums.NONE),

        (0,440,"Green",PowerupEnums.NONE),
        (0,450,"Green",PowerupEnums.SPEED),
        (0,460,"Green",PowerupEnums.NONE),
        (0,470,"Green",PowerupEnums.NONE),
        (0,480,"Green",PowerupEnums.NONE),
        (0,490,"Green",PowerupEnums.NONE),
        (0,500,"Green",PowerupEnums.NONE),

        (-10,515,"Blue",PowerupEnums.NONE),
        (0,515,"Red",PowerupEnums.NONE),
        (-10,525,"Blue",PowerupEnums.NONE),
        (0,525,"Red",PowerupEnums.NONE),
        (-10,535,"Blue",PowerupEnums.NONE),
        (-10,545,"Blue",PowerupEnums.NONE),
        (-10,555,"Blue",PowerupEnums.NONE),
        (-10,565,"Blue",PowerupEnums.NONE),

        (0,580,"Blue",PowerupEnums.NONE),
        (0,590,"Blue",PowerupEnums.NONE),
        (0,600,"Finish",PowerupEnums.NONE),
        (0,610,"Finish",PowerupEnums.NONE),
        (0,620,"Finish",PowerupEnums.NONE),
        (0,630,"Finish",PowerupEnums.NONE),
        (0,640,"Finish",PowerupEnums.NONE),
        (0,650,"Finish",PowerupEnums.NONE),
        (0,660,"Finish",PowerupEnums.NONE),
        (0,670,"Finish",PowerupEnums.NONE),
        (0,680,"Finish",PowerupEnums.NONE),
        (0,690,"Finish",PowerupEnums.NONE),
        (0,700,"Finish",PowerupEnums.NONE),
      };

    List<(int X_Value, int Z_Value, string Name, PowerupEnums powerupEnums)> pathCoordinates2 = new List<(int X_Value, int Z_Value, string Name, PowerupEnums powerupEnums)>
      {
        (0,0,"Blue",PowerupEnums.NONE),
        (0,10,"Blue",PowerupEnums.NONE),
        (0,20,"Blue",PowerupEnums.NONE),
        (0,30,"Blue",PowerupEnums.NONE),
        (0,40,"Blue",PowerupEnums.NONE),
        (0,50,"Blue",PowerupEnums.NONE),
        (0,60,"Blue",PowerupEnums.SHOOT),

        (0,75,"Blue",PowerupEnums.NONE),
        (0,85,"Blue",PowerupEnums.NONE),
        (0,95,"Blue",PowerupEnums.NONE),
        (0,105,"Blue",PowerupEnums.NONE),

        (10,120,"Red",PowerupEnums.NONE),
        (-10,120,"Green",PowerupEnums.NONE),
        (10,130,"Red",PowerupEnums.SIZE),
        (-10,130,"Green",PowerupEnums.NONE),
        (10,140,"Red",PowerupEnums.NONE),
        (-10,140,"Green",PowerupEnums.TIME),
        (10,150,"Red",PowerupEnums.NONE),
        (-10,150,"Green",PowerupEnums.NONE),
        (10,160,"Blue",PowerupEnums.NONE),
        (-10,160,"Green",PowerupEnums.NONE),
        (10,170,"Blue",PowerupEnums.NONE),
        (-10,170,"Green",PowerupEnums.NONE),

        (0,185,"Blue",PowerupEnums.SIZE),
        (0,195,"Blue",PowerupEnums.NONE),
        (0,205,"Blue",PowerupEnums.NONE),
        (0,215,"Blue",PowerupEnums.NONE),
        (0,225,"Blue",PowerupEnums.NONE),
        (0,235,"Blue",PowerupEnums.NONE),
        (0,245,"Blue",PowerupEnums.NONE),
        (0,255,"Blue",PowerupEnums.NONE),
        (0,265,"Blue",PowerupEnums.NONE),

        (0,280,"Red",PowerupEnums.NONE),
        (-10,280,"Green",PowerupEnums.NONE),
        (10,280,"Yellow",PowerupEnums.NONE),
        (0,290,"Red",PowerupEnums.NONE),
        (-10,290,"Green",PowerupEnums.NONE),
        (10,290,"Yellow",PowerupEnums.SHOOT),
        (0,300,"Red",PowerupEnums.NONE),
        (-10,300,"Green",PowerupEnums.SIZE),
        (10,300,"Yellow",PowerupEnums.NONE),
        (0,310,"Red",PowerupEnums.NONE),
        (-10,310,"Green",PowerupEnums.NONE),
        (10,310,"Yellow",PowerupEnums.TIME),
        (0,320,"Red",PowerupEnums.NONE),
        (-10,320,"Green",PowerupEnums.NONE),
        (10,320,"Yellow",PowerupEnums.NONE),

        (0,335,"Yellow",PowerupEnums.NONE),
        (-10,335,"Blue",PowerupEnums.NONE),
        (10,335,"Green",PowerupEnums.NONE),
        (0,345,"Yellow",PowerupEnums.TIME),
        (-10,345,"Blue",PowerupEnums.NONE),
        (10,345,"Green",PowerupEnums.LEVITATE),
        (0,355,"Yellow",PowerupEnums.NONE),
        (-10,355,"Blue",PowerupEnums.NONE),
        (10,355,"Green",PowerupEnums.NONE),
        (0,365,"Yellow",PowerupEnums.NONE),
        (-10,365,"Blue",PowerupEnums.NONE),
        (10,365,"Green",PowerupEnums.NONE),
        (0,375,"Yellow",PowerupEnums.NONE),
        (-10,375,"Blue",PowerupEnums.NONE),
        (10,375,"Green",PowerupEnums.NONE),
        (-10,385,"Blue",PowerupEnums.NONE),
        (10,385,"Green",PowerupEnums.NONE),
        (-10,395,"Blue",PowerupEnums.NONE),
        (10,395,"Green",PowerupEnums.SIZE),

        (0,410,"Blue",PowerupEnums.NONE),
        (0,420,"Blue",PowerupEnums.SPEED),
        (0,430,"Blue",PowerupEnums.NONE),
        (0,440,"Blue",PowerupEnums.NONE),
        (0,450,"Blue",PowerupEnums.NONE),
        (0,460,"Blue",PowerupEnums.NONE),
        (0,470,"Blue",PowerupEnums.NONE),
        (0,480,"Blue",PowerupEnums.NONE),
        (0,490,"Blue",PowerupEnums.NONE),
        (0,500,"Blue",PowerupEnums.NONE),

        (10,515,"Green",PowerupEnums.SHOOT),
        (0,515,"Blue",PowerupEnums.NONE),
        (10,525,"Green",PowerupEnums.NONE),
        (0,525,"Blue",PowerupEnums.NONE),
        (0,535,"Blue",PowerupEnums.NONE),
        (0,545,"Blue",PowerupEnums.NONE),
        (0,555,"Blue",PowerupEnums.NONE),
        (0,565,"Blue",PowerupEnums.NONE),

        (0,575,"Finish",PowerupEnums.NONE),
        (0,585,"Finish",PowerupEnums.NONE),
        (0,595,"Finish",PowerupEnums.NONE),
        (0,605,"Finish",PowerupEnums.NONE),
        (0,615,"Finish",PowerupEnums.NONE),
        (0,625,"Finish",PowerupEnums.NONE),
        (0,635,"Finish",PowerupEnums.NONE),
        (0,645,"Finish",PowerupEnums.NONE),
        (0,655,"Finish",PowerupEnums.NONE),
        (0,665,"Finish",PowerupEnums.NONE),
        (0,675,"Finish",PowerupEnums.NONE),
        (0,685,"Finish",PowerupEnums.NONE),
        (0,695,"Finish",PowerupEnums.NONE),
      };

    int i = 0;

    string powerupType = "";
    bool isPowerUpEnabled = false;
    [SerializeField] float obstacleSpawningChance = 0.55f;


    public void SpawnTile(bool spawnItems) {
        int curr = (int)nextSpawnPoint.x;

        GameObject tempGroundTileObject = null;

        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name) {
            case "Level1":
                obstacleSpawningChance = 0.65f;
                break;
            case "Level2":
                pathCoordinates = pathCoordinates2;
                obstacleSpawningChance = 0.55f;
                break;
            default:
                Debug.Log("Code should not reach here!");
                break;
        }

        //Debug.Log("Current Tile Details - " + pathCoordinates[i].X_Value + " " + pathCoordinates[i].Z_Value + " " + pathCoordinates[i].Name + " " + pathCoordinates[i].IsPowerUpEnabled);
        var color = pathCoordinates[i].Name;
        powerupType = pathCoordinates[i].powerupEnums.GetString();
        isPowerUpEnabled = false;
        if (!powerupType.Equals("")) {
            isPowerUpEnabled = true ;
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
            case "Permeate":
                tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups(PowerupEnums.PERMEATE.GetString());
                break;
            case "Levitate":
                tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups(PowerupEnums.LEVITATE.GetString());
                break;
            case "Speed":
                tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups(PowerupEnums.SPEED.GetString());
                break;
            case "Shoot":
                tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups(PowerupEnums.SHOOT.GetString());
                break;
        }

        nextSpawnPoint = tempGroundTileObject.transform.GetChild(1).transform.position;
        if (i + 1 < pathCoordinates.Count) {
            nextSpawnPoint.x = pathCoordinates[i + 1].X_Value;
            nextSpawnPoint.z = pathCoordinates[i + 1].Z_Value;
        } else {
            //Debug.Log("End of path");
            return;
        }


        float randomObstacleChance = Random.Range(0f, 1f);
        

        if (spawnItems &&
            !isPowerUpEnabled &&
            randomObstacleChance > obstacleSpawningChance &&
            !pathCoordinates[i].Name.Equals("Finish")) {
            //Debug.Log(randomObstacleChance > obstacleSpawningChance);
            //Debug.Log("Generating an obstacle because Random Obstacle Chance - " + randomObstacleChance);
            tempGroundTileObject.GetComponent<GroundTile>().SpawnObstacles();
        }
        i++;
    }

    void Start() {
        for (int i = 0; i < 120; i++) {
            if (i > 3) {
                SpawnTile(true);
            } else {
                SpawnTile(false);
            }

        }
    }
}
