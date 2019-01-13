using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    public float Gravity = 9.81f;
    public float MoveSpeed = 3.0f;
    private Vector3 position;
    private Vector3 rotation;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            
            position.Set(MoveSpeed * Input.GetAxis("Horizontal"), 0, MoveSpeed * Input.GetAxis("Vertical"));
            //transform.Rotate(rotation);
            //rotation.Set(0, RotateSpeed * Input.GetAxis("Horizontal"), 0);
            position = transform.TransformDirection(position);
        }
        position.y -= Gravity * Time.deltaTime;
        controller.Move(position * Time.deltaTime);
    }
}
