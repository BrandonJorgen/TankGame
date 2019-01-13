using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Camera gameCamera;
    
    private Ray ray;
    private Plane groundPlane;
    private float rayDistance;

    private Vector3 point;
    private Vector3 correctPoint;

    void Start()
    {
        gameCamera = Camera.main;
    }

    void Update()
    {
        //Set up the raycast and create something for the ray to hit
        ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        groundPlane = new Plane(Vector3.up, 0); //Apparently this is better/quicker to do than
                                                //setting up the plane the player is already on?
        
        //if the ray hits something, report the distance from the camera to the collision
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            correctPoint = new Vector3(point.x, transform.position.y, point.z); //adjust transform for Y height
            transform.LookAt(correctPoint); //look in the direction of the ray collision
        }
    }
}
