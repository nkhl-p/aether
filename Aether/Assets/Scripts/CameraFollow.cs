using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // This identifies the initial distance between the camera and the player when the game just starts.
        // This same distance will always be maintained throughout the game
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        // The camera's position is always 'offset' distanced away from the player i.e. the camera follows the playes
        // on the z-axis of movement
        Vector3 targetPos = player.position + offset;

        // Comment the following line if you want the camera to follow the player across the x-axis as well
        //targetPos.x = 0;

        transform.position = targetPos;
    }
}
