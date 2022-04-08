using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    #region Enums
    public enum MovementType {
        MoveTowards,
        LerpTowards
    }
    #endregion // Enums

    #region Public Variables
    public MovementType Type = MovementType.MoveTowards; // Movement type used
    public MovementPath MyPath; // Reference to the movement path used
    public float Speed = 1; // Speed with which the object is moving
    public float MaxDistanceToGoal = .1f; // How close does it have to be to the point to be considered the point
    #endregion Public Variables

    #region Private Variables
    private IEnumerator<Transform> pointInPath; // Used to reference points returned from MyPath.GetNextPathPoint
    #endregion Private Variables

    #region Main Methods
    public void Start() {
        // Make sure a path is assigned
        if (MyPath == null) {
            Debug.LogError("Movement path cannot be null. Must have a path to follow", gameObject);
            return;
        }

        // Sets up reference to an instance of the coroutine GetNextPathPoint
        pointInPath = MyPath.GetNextPathPoint();

        // Get the next point in the path to move to
        pointInPath.MoveNext();

        // Make sure there is a point to move to
        if (pointInPath.Current == null) {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return; // Exit start if there is no point to move to
        }

        // Set the position of this object to the position of our starting point
        transform.position = pointInPath.Current.position;
    }
    #endregion Main Methods

    #region Utility Methods
    public void Update() {
        // Validate if there is a path
        if (pointInPath == null || pointInPath.Current == null) {
            return; // Exit if no path is found.
        }

        if (Type == MovementType.MoveTowards) {
            // Move towards the next point in path using MoveTowards
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
        } else if (Type == MovementType.LerpTowards) {
            // Move towards the next point in path using Lerp
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) {
            pointInPath.MoveNext();
        }
    }
    #endregion

    #region Coroutines

    #endregion
}
