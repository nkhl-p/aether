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

    List<(int X_Value, int Z_Value, string Name)> pathCoordinates = new List<(int X_Value, int Z_Value, string Name)>
      {
          (0,0,"Blue"),(0,10,"Blue"),(0,20,"Blue"),(0,30,"Blue"),(0,40,"Blue"),(0,50,"Blue"),(0,60,"Blue"),(10,60,"Red"),(0,70,"Blue"),(10,70, "Red"),
          (10,80, "Red"),(10,90, "Red"),
          (-10,80,"Green"),(-10,90,"Green"),(-10,100,"Green"),(-10,110,"Green"),(-10,120,"Green"),(-10,130,"Green"),(-10,140,"Green"),(-10,150,"Green"),
          (0,170, "Red"),(0,180, "Red"),(0,190, "Red"),(0,200, "Green"),
          (0,220,"Blue"),(10,220,"Yellow"),(0,230,"Blue"),(10,230,"Yellow"),(0,240,"Blue"),(10,240,"Yellow"),(0,250,"Blue"),(0,260,"Blue"),(0,270,"Blue"),
          (0,290,"Green"),(0,300,"Green"),(0,310,"Green"),(0,320,"Finish"),
      };
    int i = 0;


    public void SpawnTile(bool spawnItems) {
        int curr = (int)nextSpawnPoint.x;

        GameObject tempGroundTileObject = null;

        switch (curr) {

            case 0:
                var color1 = pathCoordinates[i].Name;
                switch (color1) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                break;
            case 10:
                var color2 = pathCoordinates[i].Name;
                switch (color2) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                break;
            case -10:
                var color3 = pathCoordinates[i].Name;
                switch (color3) {
                    case "Red": tempGroundTileObject = Instantiate(groundTileRed, nextSpawnPoint, Quaternion.identity); break;
                    case "Blue": tempGroundTileObject = Instantiate(groundTileBlue, nextSpawnPoint, Quaternion.identity); break;
                    case "Green": tempGroundTileObject = Instantiate(groundTileGreen, nextSpawnPoint, Quaternion.identity); break;
                    case "Yellow": tempGroundTileObject = Instantiate(groundTileYellow, nextSpawnPoint, Quaternion.identity); break;
                    case "Finish": tempGroundTileObject = Instantiate(groundTileFinish, nextSpawnPoint, Quaternion.identity); break;
                }
                break;
        }

        nextSpawnPoint = tempGroundTileObject.transform.GetChild(1).transform.position;
        nextSpawnPoint.x = pathCoordinates[i + 1].X_Value;
        nextSpawnPoint.z = pathCoordinates[i + 1].Z_Value;
        i++;

        if (spawnItems) {
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
