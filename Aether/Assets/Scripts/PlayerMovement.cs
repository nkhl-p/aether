using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    [SerializeField] Rigidbody rb;

    bool alive = true;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    private void FixedUpdate() {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMovement = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMovement);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        if (transform.position.y < -5) {
            Die();
        }
    }

    public void Die() {
        alive = false;
        Invoke("Restart", 2);
    }

    void Restart() {
        // Restart the game using Unity's Scene Manager
        // Depending on what is decided (restart same scene or show pause/quit menu, the following line of code will change
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump() {
        // Check whether the player is currently on the ground
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        // If the player is on the ground, then the player has the ability to jump
        if (isGrounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }
            
    }
}
