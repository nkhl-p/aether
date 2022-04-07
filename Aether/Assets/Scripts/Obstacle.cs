using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    #region Obstacle destroy specific variables
    bool isSizePowerUpEnabled = true;
    public int cubesInRow = 2;
    public float cubeSize = 0.2f;
    public Material particleMaterialRef;
    public int explosionRadius = 50;
    public int explosionForce = 20;
    public float explosionUpwards = 0.4f;
    Vector3 cubesPivot;
    float cubesPivotDistance;
    #endregion

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        // calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;

        // use this value to create a pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.name == "Player" && isSizePowerUpEnabled) {
            // loop 3 times to create 5x5x5 pieces in x, y, z coordinates
            for (int x = 0; x < cubesInRow; x++) {
                createPiece(x, 1, 1);
            }

            // get explosion point
            Vector3 explosionPos = transform.position;
            // get colliders in that point and radius
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            // add explosion force to all colliders in that explosion sphere
            foreach (Collider hit in colliders) {
                // get rigidbody for that collider object
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null) {
                    // add explosion force to this body with given parameters
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwards);
                }
            }
        }

        // Kill the player
        if (collision.gameObject.name == "Player" && !isSizePowerUpEnabled) {
            AudioManager temp = FindObjectOfType<AudioManager>();
            temp.Play(SoundEnums.COLLISION.GetString());
            temp.StopPlaying(SoundEnums.THEME.GetString());
            playerMovement.Die();
        }

    }

    public void createPiece(int x, int y, int z) {
        // Create a piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (piece.GetComponent<MeshRenderer>() == null) {
            piece.AddComponent<UnityEngine.MeshRenderer>();
        }
        Vector3 temp = piece.transform.position;
        temp.y = 0.5f;

        // Set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        // Add rigid body and set mass
        piece.AddComponent<Rigidbody>().mass = cubeSize;
        piece.GetComponent<MeshRenderer>().material = particleMaterialRef; // making the material of each generated particle same as that of the initial obstacle
        Destroy(piece, 0.5f); // destroying each particle created as a result of the shooting
        Destroy(gameObject); // destroying the original game object that was replaced with the small particles
    }
}
