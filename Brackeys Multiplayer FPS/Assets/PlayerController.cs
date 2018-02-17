using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5F;

    private PlayerMotor motor;

    private float mouseSensitivity = 3;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        //calculate movement velocity as a 3D vector.

        //'GetAxis' as standard applies a smoothing filter. 'GetAxisRaw' doesn't.
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMovement;
        Vector3 moveVertical = transform.forward * zMovement;

        //Final movement vector
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        //Apply movement
        motor.Move(velocity);

        //Calculate rotation as a 3D vector (turning around);
        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0F, yRotation, 0F) * mouseSensitivity;

        //Apply rotation
        motor.Rotate(rotation);

        //Calculate camera rotation as a 3D vector (looking up and down);
        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRotation, 0F, 0F) * mouseSensitivity;

        //Apply rotation
        motor.RotateCamera(cameraRotation);
    }
}