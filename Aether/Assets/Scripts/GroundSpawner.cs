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


    int[] arr = { 0, 0, 0, 0, 10, 10, 10, 10, 10, 0, 0, 0, 0, -10, -10, -10, -20, -20, 0, 0, 40 };
    List<(int Index, string Name)> pathCoordinates = new List<(int Index, string Name)>
      {
          (0, "Blue"),(0, "Blue"),(0, "Blue"),(0, "Blue"),(0, "Finish"),
          (10, "Red"),(10, "Red"),(10, "Red"),
          (-10,"Green"),(-10,"Green"),(-10,"Green"),(-10,"Green"),
          (0, "Blue"),(0, "Blue"),(0, "Blue"),(0, "Blue"),(0,"Blue"),
          (10,"Yellow"),(10,"Yellow"),
          (-10, "Red"),(-10, "Red"),(-10, "Red"),
          (0, "Green"),(0, "Green"),(0, "Finish")
      };
    int i = 0;


    public void SpawnTile(bool spawnItems) {
        Debug.Log("Index - " + pathCoordinates[i].Index + " name - " + pathCoordinates[i].Name);
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
        nextSpawnPoint.x = pathCoordinates[i + 1].Index;
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
