using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    #region Enums
    public enum PathTypes {
        linear,
        loop
    }
    #endregion // Enums

    #region Public Variables
    public PathTypes PathType; // Indicates type of path (linear/looping)
    public int movementDirection = 1; // 1 = clockwise/forward || -1 = clockwise/backward
    public int movingTo = 0; // Used to identify the point in the path sequence we are moving to
    public Transform[] PathSequence; // array of all points in the path
    #endregion Public Variables

    #region Private Variables

    #endregion Private Variables

    #region Main Methods
    // This will allow us to draw lines between out points in the Unity Editor
    // These lines will allow us to easily see the path that our moving object will follow
    // in the game
    public void OnDrawGizmos() {

        // Make sure that your sequence has points in it and that there are at least 2 points
        // to constitute a path
        if (PathSequence == null || PathSequence.Length < 2) {
            return;
        }

        // Loop through all the points in the sequence of points
        for (var i = 1; i < PathSequence.Length; i++) {
            // Draw a line between the points
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);
        }

        // If your path loops back to the beginning when it reaches the end
        if (PathType == PathTypes.loop) {
            Gizmos.DrawLine(PathSequence[0].position, PathSequence[PathSequence.Length - 1].position);
        }
    }
    #endregion Main Methods

    #region Utility Methods
    #endregion

    // Coroutines run parallel to other functions
    #region Coroutines
    public IEnumerator<Transform> GetNextPathPoint() {
        if (PathSequence == null || PathSequence.Length < 1) {
            yield break; // exits the coroutine sequence as length check failed
        }

        while (true) {
            yield return PathSequence[movingTo];

            if (PathSequence.Length == 1) {
                continue;
            }

            if (PathType == PathTypes.linear) {
                if (movingTo <= 0) {
                    movementDirection = 1;
                } else if (movingTo >= PathSequence.Length - 1) {
                    movementDirection = -1;
                }
            }
            movingTo += movementDirection;

            if (PathType == PathTypes.loop) {
                if (movingTo >= PathSequence.Length) {
                    movingTo = 0;
                }

                if (movingTo < 0) {
                    movingTo = PathSequence.Length - 1;
                }
            }
        }
    }
    #endregion
}
