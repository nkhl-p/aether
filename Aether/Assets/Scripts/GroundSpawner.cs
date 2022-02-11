using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour {
    [SerializeField] GameObject groundTileBlue;
    [SerializeField] GameObject groundTileRed;
    [SerializeField] GameObject groundTileGreen;
    [SerializeField] GameObject groundTileYellow;
    [SerializeField] GameObject groundTileFinish;
    Vector3 nextSpawnPoint;

    List<(int X_Value, int Z_Value, string Name, bool isPowerUpEnabled)> pathCoordinates = new List<(int X_Value, int Z_Value, string Name, bool isPowerUpEnabled)>
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
(0,180, "Red",false),
(0,190, "Red",false),
(0,200, "Green",false),
(0,220,"Blue",false),
(10,220,"Yellow",false),
(0,230,"Blue",false),
(10,230,"Yellow",false),
(0,240,"Blue",false),
(10,240,"Yellow",false),
(0,250,"Blue",false),
(0,260,"Blue",false),
(0,270,"Blue",false),
(0,290,"Green",false),
(0,300,"Green",false),
(0,310,"Green",false),
(0,320,"Finish",false),
      };
    int i = 0;


    public void SpawnTile(bool spawnItems) {
        int curr = (int)nextSpawnPoint.x;

        GameObject tempGroundTileObject = null;

        switch (curr) {

            case 0:
                var color1 = pathCoordinates[i].Name;
                bool isPowerUpEnabled1 = pathCoordinates[i].isPowerUpEnabled;
                switch (color1) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                if (isPowerUpEnabled1) {
                    tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups();
                }
                break;
            case 10:
                var color2 = pathCoordinates[i].Name;
                bool isPowerUpEnabled2 = pathCoordinates[i].isPowerUpEnabled;
                switch (color2) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                if (isPowerUpEnabled2) {
                    tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups();
                }
                break;
            case -10:
                var color3 = pathCoordinates[i].Name;
                bool isPowerUpEnabled3 = pathCoordinates[i].isPowerUpEnabled;
                switch (color3) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                if (isPowerUpEnabled3) {
                    tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups();
                }
                break;
        }

        nextSpawnPoint = tempGroundTileObject.transform.GetChild(1).transform.position;
        nextSpawnPoint.x = pathCoordinates[i + 1].X_Value;
        nextSpawnPoint.z = pathCoordinates[i + 1].Z_Value;
        i++;

        if (spawnItems && pathCoordinates[i].Z_Value != 100) {
            tempGroundTileObject.GetComponent<GroundTile>().SpawnObstacles();
            //tempGroundTileObject.GetComponent<GroundTile>().SpawnPowerups();
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
