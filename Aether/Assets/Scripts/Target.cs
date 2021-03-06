using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    public int explosionRadius = 50;
    public int explosionForce = 20;
    public float explosionUpwards = 0.4f;
    public Material particleMaterialRef;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public void Start() {

        // calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;

        // use this value to create a pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            //Destroy(gameObject);
            gameObject.SetActive(false);


            // loop 3 times to create 5x5x5 pieces in x, y, z coordinates
            for (int x = 0; x < cubesInRow; x++) {
                for (int y = 0; y < cubesInRow; y++) {
                    for (int z = 0; z < cubesInRow; z++) {
                        createPiece(x, y, z);
                    }
                }
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
    }

    public void createPiece(int x, int y, int z) {
        // Create a piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (piece.GetComponent<MeshRenderer>() == null) {
            piece.AddComponent<UnityEngine.MeshRenderer>();
        }

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
