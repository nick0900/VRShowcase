using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageBallOrientation : MonoBehaviour
{
    Transform parent;
    Quaternion parentLastRotation;
    Quaternion imageRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        parentLastRotation = parent.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 orientationDelta =  (parentLastRotation * Quaternion.Inverse(parent.rotation)) * Vector3.forward;
        orientationDelta = Vector3.ProjectOnPlane(orientationDelta, Vector3.up);
        imageRotation *= Quaternion.AngleAxis(Vector3.SignedAngle(orientationDelta, Vector3.forward, Vector3.up), Vector3.up);
        this.transform.rotation = imageRotation;
        parentLastRotation = parent.rotation;
    }
}
