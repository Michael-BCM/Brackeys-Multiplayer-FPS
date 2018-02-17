using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;

    private Vector3 rotation = Vector3.zero;

    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody _rigidbody;

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
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
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
    }

    private void PerformRotation()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}