using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Update is called once per frame
    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        if (transformCache.position.y < 0) {
            Die();
        }
    }

    public void Die() {
        alive = false;
        if (transformCache.position.y < 0) {
            Debug.Log("The player is falling");
            // temp.Play("Fall");
        }
        Invoke("Restart", 1);
    }

    void Restart() {
        // Restart the game using Unity's Scene Manager
        // Depending on what is decided (restart same scene or show pause/quit menu, the following line of code will change
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioManagerInstance.Play("SpaceTravel");
    }

    void Jump() {
        // Check whether the player is currently on the ground
        
        audioManagerInstance.Play("Jump");
        // temp.StopPlaying("SpaceTravel");
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        // If the player is on the ground, then the player has the ability to jump
        if (isGrounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }

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

    // For this to work, the Plane gameObject of the GroundTile prefab had to be assigned the different tags that were assigned to the GroundTile
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("TileRed")) {
            FindObjectOfType<PlayerMovement>().speed = 5;
        } else if (collision.gameObject.CompareTag("TileBlue")) {
            FindObjectOfType<PlayerMovement>().speed = 11;
        } else if (collision.gameObject.CompareTag("TileGreen")) {
            FindObjectOfType<PlayerMovement>().speed = 15;
        } else if (collision.gameObject.CompareTag("TileYellow")) {
            audioManagerInstance.Play("YellowLose");
            audioManagerInstance.StopPlaying("SpaceTravel");
            Die();
        } else if (collision.gameObject.CompareTag("TileFinish")) {
            // The following line will be replaced by UnityEngine.ScreenManagement to load a new scene (Intermediate Level Scene)
            AudioManager temp = FindObjectOfType<AudioManager>();
            temp.Play("Win");
            Debug.Log("Game Over! You proceed to the next level");
            SceneManager.LoadScene(3);
        } else {
            Debug.Log("This should not have been printed as there are no other tags apart from TileRed, TileGreen, TileBlue, TileYellow and TileFinish");
            Die();
        }
    }
}
