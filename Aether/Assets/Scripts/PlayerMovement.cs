using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System;




public class PlayerMovement : MonoBehaviour {
    public float speed = 5;
    [SerializeField] Rigidbody rb;

    bool alive = true;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    private Transform transformCache;
    AudioManager audioManagerInstance = null;

    // variable declaration for unity analytics
    public static int levelNumber = 1;
    public static int blueCount = 0;
    public static int redCount = 0;
    public static int greenCount = 0;
    public static string prevColorTag = "";
    public static int tries = 0;
    //public static List<int> distanceArray = new List<int>();
    public static int distance = 0;
    //public static List<int> timeArray = new List<int>();
    public static int time = 0;
    public static int deathByObstacleCount = 0;
    public static int deathByYellowPathCount = 0;
    public static int deathByFreeFallCount = 0;
    public static int deathByOutOfTimeCount = 0;
    public static int powerUpsLevelCount = 0;
    bool val = false;

    private void Awake() {
        transformCache = transform;
    }

    private void Start() {
        audioManagerInstance = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate() {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMovement = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMovement);
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        if (transformCache.position.y < 0) {
            if (!val) {
                val = !val;
                tries++;
                deathByFreeFallCount++;

                SendPathSelectionAnalyticsData(levelNumber, blueCount, redCount, greenCount);
                SendDistanceAnalyticsData(levelNumber, Convert.ToInt32(transform.position.z));
                SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
                    deathByFreeFallCount, deathByOutOfTimeCount);
                SendPowerUpsAnalyticsData(levelNumber, powerUpsLevelCount);
                Die();

            }
        }
    }

    public void Die() {
        alive = false;
        Debug.Log("Number of player deaths: " + tries);
        if (transformCache.position.y < 0) {
            // audioManagerInstance.Play(SoundEnums.FALL.GetString());
            //distanceArray.Add(Convert.ToInt32(transform.position.z));
            //timeArray.Add(Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));

        }

        Invoke("Restart", 1);
    }

    void resetDeathStats() {
        deathByObstacleCount = 0;
        deathByYellowPathCount = 0;
        deathByFreeFallCount = 0;
        deathByOutOfTimeCount = 0;
    }

    void resetLevelDeathStats() {
        tries = 0;
    }

    void resetPathSelectionStats() {
        redCount = 0;
        blueCount = 0;
        greenCount = 0;
    }

    void resetDistanceStats() {
        distance = 0;
    }

    void resetTimeStats() {
        time = 0;
    }

    void resetPowerupStats() {
        powerUpsLevelCount = 0;
    }

    void Restart() {
        // Restart the game using Unity's Scene Manager
        // Depending on what is decided (restart same scene or show pause/quit menu, the following line of code will change
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioManagerInstance.Play(SoundEnums.THEME.GetString());
    }

    void Jump() {
        // Check whether the player is currently on the ground

        audioManagerInstance.Play(SoundEnums.JUMP.GetString());
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        // If the player is on the ground, then the player has the ability to jump
        if (isGrounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }

    }

    // For this to work, the Plane gameObject of the GroundTile prefab had to be assigned the different tags that were assigned to the GroundTile
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("TileRed")) {
            FindObjectOfType<PlayerMovement>().speed = 5;
            if (prevColorTag != "RED") redCount++;
            prevColorTag = "RED";
        } else if (collision.gameObject.CompareTag("TileBlue")) {
            FindObjectOfType<PlayerMovement>().speed = 11;
            if (prevColorTag != "BLUE") blueCount++;
            prevColorTag = "BLUE";

        } else if (collision.gameObject.CompareTag("TileGreen")) {
            FindObjectOfType<PlayerMovement>().speed = 15;
            if (prevColorTag != "GREEN") greenCount++;
            prevColorTag = "GREEN";
        } else if (collision.gameObject.CompareTag("TileYellow")) {
            // Manage sounds
            audioManagerInstance.Play(SoundEnums.YELLOW_LOSE.GetString());
            audioManagerInstance.StopPlaying("SpaceTravel");
            deathByYellowPathCount++;

            SendPathSelectionAnalyticsData(levelNumber, blueCount, redCount, greenCount);
            SendDistanceAnalyticsData(levelNumber, Convert.ToInt32(transform.position.z));
            SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
                deathByFreeFallCount, deathByOutOfTimeCount);
            SendPowerUpsAnalyticsData(levelNumber, powerUpsLevelCount);
            tries++;

            Die();
        } else if (collision.gameObject.CompareTag("TileFinish")) {
            // The following line will be replaced by UnityEngine.ScreenManagement to load a new scene (Intermediate Level Scene)
            audioManagerInstance.Play(SoundEnums.WIN.GetString());
            Debug.Log("Level Complete! You proceed to the next level");

            if (prevColorTag != "FINISH") {
                prevColorTag = "FINISH";
                //SendDistanceAnalyticsData(levelNumber, distanceArray);
                //SendTimeAnalyticsData(levelNumber, timeArray);
                //distanceArray = new List<int>();
                //timeArray = new List<int>();
            }

            // All analytics are called here - The idea is that until a player completes a level, all the analytic events are trigger here, so as to be under the Unity provided limits. All data is collected and sent at once.
            SendLevelDeathAnalyticsData(levelNumber, tries);
            SendPathSelectionAnalyticsData(levelNumber, blueCount, redCount, greenCount);
            SendTimeAnalyticsData(levelNumber, Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));
            SendPowerUpsAnalyticsData(levelNumber, powerUpsLevelCount);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else if (collision.gameObject.CompareTag("Obstacle")) {
            //distanceArray.Add(Convert.ToInt32(transform.position.z));
            //timeArray.Add(Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));
            Debug.Log("Player collided with an obstacle");
            deathByObstacleCount++;

            SendPathSelectionAnalyticsData(levelNumber, blueCount, redCount, greenCount);
            SendDistanceAnalyticsData(levelNumber, Convert.ToInt32(transform.position.z));
            SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
                deathByFreeFallCount, deathByOutOfTimeCount);
            SendPowerUpsAnalyticsData(levelNumber, powerUpsLevelCount);
            tries++;

        } else {
            //distanceArray.Add(Convert.ToInt32(transform.position.z));
            //timeArray.Add(Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));
            Debug.Log("This should not have been printed as there are no other tags apart from TileRed, TileGreen, TileBlue, TileYellow and TileFinish");
        }
    }

    public void PowerUpPickedUpCounterUpdate() {
        Debug.Log("Picked Up Powerup Counter Updated");
        powerUpsLevelCount++;
    }

    public void DeathByOutOfTime() {
        deathByOutOfTimeCount++;

        SendPathSelectionAnalyticsData(levelNumber, blueCount, redCount, greenCount);
        SendDistanceAnalyticsData(levelNumber, Convert.ToInt32(transform.position.z));
        SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
            deathByFreeFallCount, deathByOutOfTimeCount);
        SendPowerUpsAnalyticsData(levelNumber, powerUpsLevelCount);

    }

    // Add all analytics events below this point
    // Average number of times a player dies. Data sent at level complete (average is to be calculated on Highcharts)
    public void SendLevelDeathAnalyticsData(int level_num, int tries) {
        Dictionary<string, object> data = new Dictionary<string, object> {
            { "Level", level_num },
            { "Retries", tries }
        };
        resetLevelDeathStats();

        Debug.Log("Level Death Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analyticsResultDie = Analytics.CustomEvent("Level_Death_Analytics", data);
        Debug.Log("Analytics Die: " + analyticsResultDie);
    }

    // Average number of times a path is chosen. Data sent at each death (average is to be calculated on Highcharts)
    public void SendPathSelectionAnalyticsData(int level_num, int bCount, int rCount, int gCount) {
        Dictionary<string, object> data = new Dictionary<string, object> {
                {"Level", level_num},
                {"Blue_Path", bCount },
                {"Red_Path", rCount },
                {"Green_Path", gCount}
        };
        resetPathSelectionStats();

        Debug.Log("Path Selection Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("Path_Selection_Analytics", data);
        Debug.Log("Path Selection Analytics: " + analytics_result);
    }


    //public void SendDistanceAnalyticsData(int level_num, List<int> distance) {
    //    int sum = 0;
    //    for (var i = 0; i < distance.Count - 1; i++) {
    //        sum += distance[i];
    //    }

    //    Dictionary<string, object> data = new Dictionary<string, object> {
    //            {"Level", level_num},
    //            {"Distance", (sum/distance.Count)}
    //    };

    //    Debug.Log("Distance Analytics Array: " + string.Join(',', distance));
    //    Debug.Log("Distance Analytics Debug Data: ");
    //    data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
    //    AnalyticsResult analytics_result = Analytics.CustomEvent("Distance_Travelled_Analytics", data);
    //    Debug.Log("Distance Travelled Analytics: " + analytics_result);
    //}

    // Average distance travelled during each play. Data sent at each death (average is to be calculated on Highcharts)
    public void SendDistanceAnalyticsData(int level_num, int distance) {
        Dictionary<string, object> data = new Dictionary<string, object> {
                {"Level", level_num},
                {"Distance", distance}
        };
        resetDistanceStats();

        Debug.Log("Distance Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("Distance_Travelled_Analytics", data);
        Debug.Log("Distance Travelled Analytics: " + analytics_result);
    }

    //public void SendTimeAnalyticsData(int level_num, List<int> time) {
    //    Debug.Log("in here");
    //    int sum = 0;
    //    foreach (var t in time) {
    //        sum += t;
    //    }

    //    Dictionary<string, object> data = new Dictionary<string, object> {
    //            {"Level", level_num},
    //            {"Time", (sum/time.Count) }
    //    };

    //    Debug.Log("Time Analytics Array: " + string.Join(',', time));
    //    Debug.Log("Time Analytics Debug Data: ");
    //    data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
    //    AnalyticsResult analytics_result = Analytics.CustomEvent("Total_Time_Analytics", data);
    //    Debug.Log("Time Analytics: " + analytics_result);
    //}


    // Average time taken for each gameplay instance. Data sent at each death (average is to be calculated on Highcharts)
    public void SendTimeAnalyticsData(int level_num, int time) {
        Dictionary<string, object> data = new Dictionary<string, object> {
                {"Level", level_num},
                {"Time", time }
        };
        resetTimeStats();

        Debug.Log("Time Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("Total_Time_Analytics", data);
        Debug.Log("Time Analytics: " + analytics_result);
    }

    // Mode of death of a player. Data sent at each death (sum is to be calculated on Highcharts)
    public void SendModeOfDeathAnalyticsData(int level_num,
        int obstacleCount, int yellowPathCount, int freeFallCount, int outOfTimeCount) {
        Debug.Log("SendModeOfDeathAnalyticsData Called");

        Dictionary<string, object> data = new Dictionary<string, object> {
            {"Level", level_num},
            {"Obstacle", obstacleCount},
            {"Yellow_Path", yellowPathCount},
            {"Free_Fall", freeFallCount},
            {"Out_Of_Time", outOfTimeCount},
        };
        resetDeathStats();

        Debug.Log("SendModeOfDeathAnalyticsData Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("Mode_Of_Death_Analytics", data);
        Debug.Log("SendModeOfDeathAnalyticsData: " + analytics_result);
    }

    // Number of powerups collected during each gameplay. Data is sent at each gameplay (average is to be calculated on Highcharts)
    public void SendPowerUpsAnalyticsData(int level_num, int powerUpsLevelCount) {
        Debug.Log("SendPowerUpsAnalyticsData Called");

        Dictionary<string, object> data = new Dictionary<string, object> {
            {"Level", level_num},
            {"PowerUps_Count", powerUpsLevelCount},
        };
        resetPowerupStats();

        Debug.Log("SendPowerUpsAnalyticsData Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("PowerUps_Count_Analytics", data);
        Debug.Log("SendPowerUpsAnalyticsData: " + analytics_result);

    }
}
