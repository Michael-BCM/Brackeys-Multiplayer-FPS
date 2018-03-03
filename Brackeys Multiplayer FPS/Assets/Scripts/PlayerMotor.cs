using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;

    private Vector3 rotation = Vector3.zero;

    private float cameraRotationX = 0;

    private float currentCameraRotationX = 0;

    private Rigidbody _rigidbody;

    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 89.9F;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //Gets a movement vector.
    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets a rotational vector.
    public void Rotate (Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets a rotational vector for the camera.
    public void RotateCamera(float _cameraRotation)
    {
        cameraRotationX = _cameraRotation;
    }

    //Get a force vector for our thruster. 
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    //Runs every physics iteration.
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable.
    private void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce != Vector3.zero)
        {
            _rigidbody.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    //Perform rotation
    private void PerformRotation()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            //Set our rotation and clamp it.
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply our rotation to the transform of the camera. 
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
        }
    }
}