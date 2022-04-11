using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    #region Obstacle destroy specific variables
    public static bool IsSizePowerUpEnabled = false;
    public Material particleMaterialRef;
    Vector3 cubesPivot;
    float cubesPivotDistance;

    public float cubeSize = 0.2f;
    public int cubesInRow = 4;
    public int cubesInCol = 4;
    public int cubesInDepth = 4;
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
        AudioManager temp = FindObjectOfType<AudioManager>();
        temp.Play(SoundEnums.COLLISION.GetString());
        //temp.StopPlaying(SoundEnums.THEME.GetString());

        if (collision.gameObject.name == "Player") {
            if (IsSizePowerUpEnabled) {
                cubesInCol = (gameObject.name.Equals("Obstacle(Clone)")) ? 1 : 2;
                for (int x = 0; x < cubesInRow; x++) {
                    for (int y = 0; y < cubesInCol; y++) {
                        for (int z = 0; z < 1; z++) {
                            createPiece(x, y, z);
                        }
                    }
                }
            } else {
                playerMovement.Die();
            }
        }
    }

    public void createPiece(int x, int y, int z) {
        // Create a piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (piece.GetComponent<MeshRenderer>() == null) {
            piece.AddComponent<UnityEngine.MeshRenderer>();
        }

        // Set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z);
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        // Add rigid body and set mass
        piece.AddComponent<Rigidbody>().mass = cubeSize;
        piece.GetComponent<MeshRenderer>().material = particleMaterialRef; // making the material of each generated particle same as that of the initial obstacle
        Destroy(piece, 3f); // destroying each particle created as a result of the shooting
        Destroy(gameObject); // destroying the original game object that was replaced with the small particles
    }
}
