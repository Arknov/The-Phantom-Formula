using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes {
        linear,
        loop
    }

    public PathTypes PathType;
    public int movementDirection = 1; //1 = forward, 0 = backward
    public int movingTo = 0; //point in PathSequence we are moving to
    public Transform[] PathSequence;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //runs when sprites are drawn type shit
    public void OnDrawGizmos() {

        //make sure path has > 2 points in it
        if (PathSequence == null || PathSequence.Length < 2)
        {
            return;
        }

        //draws lines between points on path in editor
        for (var i = 1; i < PathSequence.Length; i++)
        {
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);
        }
        if (PathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathSequence[0].position, PathSequence[PathSequence.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        //make sure path has > 1 point on it
        if (PathSequence == null || PathSequence.Length < 1)
        {
            yield break;
        }

        while (true)
        {
            //return current destination & wait for next call of enumerator
            //(prevents infinite loop)
            yield return PathSequence[movingTo];

            //pauses here until GetNextPathPoint is called again by FollowPath.cs

            //if there is only one point
            if (PathSequence.Length == 1)
            {
                continue;
            }

            //flips direction if reaches end of linear path
            if (PathType == PathTypes.linear)
            {
                if (movingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if (movingTo >= PathSequence.Length - 1)
                {
                    movementDirection = -1;
                }
            }

            movingTo = movingTo + movementDirection;

            //loops if reaches end of looping path
            if (PathType == PathTypes.loop)
            {
                if (movingTo >= PathSequence.Length)
                {
                    movingTo = 0;
                }

                if (movingTo < 0)
                {
                    movingTo = PathSequence.Length - 1;
                }
            }
        }
    }
}
