using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    public float parallaxEffectMultiplierX = .5f;
    public float parallaxEffectMultiplierY = .2f;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - lastCameraPosition.x) * parallaxEffectMultiplierX;
        float deltaY = (cameraTransform.position.y - lastCameraPosition.y) * parallaxEffectMultiplierY;
        Vector3 deltaMovement = new Vector3(deltaX,deltaY,0);
        transform.position += deltaMovement;
        lastCameraPosition = cameraTransform.position;
    }
}
