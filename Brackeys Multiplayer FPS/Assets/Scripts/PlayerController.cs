using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private float mouseSensitivity = 3;

    [SerializeField]
    private float speed = 5F;
    
    [SerializeField]
    private float thrusterForce = 1000F;

    [Header("Spring Settings:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;

    [SerializeField]
    private float jointSpring = 20F;

    [SerializeField]
    private float jointMaxForce = 40F;


    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
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

        float cameraRotationX = xRotation * mouseSensitivity;

        //Apply rotation
        motor.RotateCamera(cameraRotationX);

        Vector3 _thrusterForce = Vector3.zero;

        //Calculate thruster force based on player input. 
        if(Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0F);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        //Apply thruster force
        motor.ApplyThruster(_thrusterForce);

    }

    private void SetJointSettings (float _jointSpring)
    {
        //Notice how the 'new JointDrive' doesn't have regular brackets before the curly braces?
        //This is because it's possible to initialise individual values of a 'new' object without the use of a constructor,
        //or ever needing to create one. 
        joint.yDrive = new JointDrive
        {
            mode = jointMode, positionSpring = jointSpring, maximumForce = jointMaxForce
        };
    }
}