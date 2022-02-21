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
    public static List<int> distanceArray = new List<int>();
    public static List<int> timeArray = new List<int>();
    public static int deathByObstacleCount = 0;
    public static int deathByYellowPathCount = 0;
    public static int deathByFreeFallCount = 0;
    public static int deathByOutOfTimeCount = 0;
    private bool deathByOutOfTimeTriggered = false;
    
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
                SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
                    deathByFreeFallCount, deathByOutOfTimeCount);
                Die();
            }
        }
    }

    public void Die() {
        alive = false;
        Debug.Log("Number of player deaths: " + tries);
        if (transformCache.position.y < 0) {
            // audioManagerInstance.Play(SoundEnums.FALL.GetString());
            distanceArray.Add(Convert.ToInt32(transform.position.z));
            timeArray.Add(Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));

        }

        Invoke("Restart", 1);
    }

    void resetDeathStats()
    {
        deathByObstacleCount = 0;
        deathByYellowPathCount = 0;
        deathByFreeFallCount = 0;
        deathByOutOfTimeCount = 0;
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
            SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
                deathByFreeFallCount, deathByOutOfTimeCount);
            tries++;
            Die();
        } else if (collision.gameObject.CompareTag("TileFinish")) {
            // The following line will be replaced by UnityEngine.ScreenManagement to load a new scene (Intermediate Level Scene)
            audioManagerInstance.Play(SoundEnums.WIN.GetString());
            Debug.Log("Level Complete! You proceed to the next level");

            if(prevColorTag!="FINISH"){
                prevColorTag="FINISH";
                distanceArray.Add(Convert.ToInt32(transform.position.z));
                timeArray.Add(Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));
                SendDistanceAnalyticsData(levelNumber, distanceArray);
                SendTimeAnalyticsData(levelNumber,timeArray);
                distanceArray = new List<int>();
                timeArray = new List<int>();

            }

            // All analytics are called here - The idea is that until a player completes a level, all the analytic events are trigger here, so as to be under the Unity provided limits. All data is collected and sent at once.
            SendLevelDeathAnalyticsData(levelNumber, tries);
            SendPathSelectionAnalyticsData(levelNumber, blueCount, redCount, greenCount);
            SendPowerUpsAnalyticsData(levelNumber, powerUpsLevelCount);
            SceneManager.LoadScene(3);
        } else if (collision.gameObject.CompareTag("Obstacle")) {
            
            distanceArray.Add(Convert.ToInt32(transform.position.z));
            timeArray.Add(Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));
            Debug.Log("Player collided with an obstacle");
            deathByObstacleCount++;
            SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
                deathByFreeFallCount, deathByOutOfTimeCount);
            tries++;
        } else {
            
            distanceArray.Add(Convert.ToInt32(transform.position.z));
            timeArray.Add(Convert.ToInt32(FindObjectOfType<ScoreTimer>().startingTime - FindObjectOfType<ScoreTimer>().currentTime));
            Debug.Log("This should not have been printed as there are no other tags apart from TileRed, TileGreen, TileBlue, TileYellow and TileFinish");
        }
    }

    public void PowerUpPickedUpCounterUpdate()
    {
        Debug.Log("Picked Up Powerup Counter Updated");
        powerUpsLevelCount++;
    }

    public void DeathByOutOfTime()
    {
        if (deathByOutOfTimeTriggered == false)
        {
            deathByOutOfTimeTriggered = true;
            deathByOutOfTimeCount++;
            SendModeOfDeathAnalyticsData(levelNumber, deathByObstacleCount, deathByYellowPathCount,
                deathByFreeFallCount, deathByOutOfTimeCount);
        }
    }

    // Add all analytics events below this point
    public void SendLevelDeathAnalyticsData(int level_num, int tries) {
        Dictionary<string, object> data = new Dictionary<string, object> {
            { "Level", level_num },
            { "Retries", tries }
        };
        Debug.Log("Level Death Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analyticsResultDie = Analytics.CustomEvent("Level_Death_Analytics", data);
        Debug.Log("Analytics Die: " + analyticsResultDie);
    }

    public void SendPathSelectionAnalyticsData(int level_num, int bCount, int rCount, int gCount) {
        Dictionary<string, object> data = new Dictionary<string, object> {
                {"Level", level_num},
                {"Blue_Path", bCount },
                {"Red_Path", rCount },
                {"Green_Path", gCount}
            };

        Debug.Log("Path Selection Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("Path_Selection_Analytics", data);
        Debug.Log("Path Selection Analytics: " + analytics_result);
    }

    public void SendDistanceAnalyticsData(int level_num, List<int> distance) {
        int sum = 0;
        foreach(var d in distance)
            {
                sum+=d;
            }

        Dictionary<string, object> data = new Dictionary<string, object> {
                {"Level", level_num},
                {"Distance", (sum/distance.Count)}
            };
        
        Debug.Log("Distance Analytics Array: "+ string.Join(',',distance));
        Debug.Log("Distance Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("Distance_Travelled_Analytics", data);
        Debug.Log("Distance Travelled Analytics: " + analytics_result);
    }

    public void SendTimeAnalyticsData(int level_num, List<int> time) {
        Debug.Log("in here");
        int sum = 0;
        foreach(var t in time)
                {
                    sum+=t;
                }


        Dictionary<string, object> data = new Dictionary<string, object> {
                {"Level", level_num},
                {"Time", (sum/time.Count) }
            };

        Debug.Log("Time Analytics Array: "+ string.Join(',',time));
        Debug.Log("Time Analytics Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("Total_Time_Analytics", data);
        Debug.Log("Time Analytics: " + analytics_result);
    }
    
    public void SendModeOfDeathAnalyticsData(int level_num, 
        int obstacleCount, int yellowPathCount, int freeFallCount, int outOfTimeCount)
    {
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
        AnalyticsResult analytics_result = Analytics.CustomEvent("Mode_Of_Death", data);
        Debug.Log("SendModeOfDeathAnalyticsData: " + analytics_result);
    }

    public void SendPowerUpsAnalyticsData(int level_num, int powerUpsLevelCount)
    {
        Debug.Log("SendPowerUpsAnalyticsData Called");
        
        Dictionary<string, object> data = new Dictionary<string, object> {
            {"Level", level_num},
            {"PowerUps_Count", powerUpsLevelCount},
        };
        
        Debug.Log("SendPowerUpsAnalyticsData Debug Data: ");
        data.ToList().ForEach(x => Debug.Log(x.Key + "\t" + x.Value));
        AnalyticsResult analytics_result = Analytics.CustomEvent("PowerUps_Count", data);
        Debug.Log("SendPowerUpsAnalyticsData: " + analytics_result);
        
    }

    /* Commenting the below method (OnTriggerEnter) since we want to change the speed of the player when it COLLIDES with the tile rather than when it enters the BoxCollider
     * Note that it is possible to use OnCollisionEnter because the Player has a CapsuleCollider for which isTrigger has not been enabled whereas
     * the Plane (child object of the GroundTile prefab) is enclosed within a BoxCollider for which isTrigger has been enabled.
     * 
     * Note: Both GameObjects must contain a Collider component. One must have Collider.isTrigger enabled, and contain a Rigidbody. 
     * If both GameObjects have Collider.isTrigger enabled, no collision happens. 
     * The same applies when both GameObjects do not have a Rigidbody component.
     * 
     * Source: https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html
     */

    /*
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("TileRed")) {
            FindObjectOfType<PlayerMovement>().speed = 5;
        } else if (collider.gameObject.CompareTag("TileBlue")) {
            FindObjectOfType<PlayerMovement>().speed = 15;
        } else if (collider.gameObject.CompareTag("TileGreen")) {
            FindObjectOfType<PlayerMovement>().speed = 25;
        } else if (collider.gameObject.CompareTag("TileYellow")) {
            Die();
        } else if (collider.gameObject.CompareTag("TileFinish")) {
            // The following line will be replaced by UnityEngine.ScreenManagement to load a new scene (Intermediate Level Scene)
            Debug.Log("Game Over! You proceed to the next level");
            SceneManager.LoadScene(3);
        } else {
            Debug.Log("This should not have been printed as there are no other tags apart from TileRed, TileGreen, TileBlue and TileYellow");
            Die();
        }
    }
    */
}
