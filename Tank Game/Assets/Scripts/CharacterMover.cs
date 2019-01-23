using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    public float Gravity = 9.81f;
    public float MoveSpeed = 3.0f;
    private string currentDirection;
    private Vector3 position;
    private Vector3 rotation;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        position.Set(MoveSpeed * Input.GetAxisRaw("Horizontal"), 0, MoveSpeed * Input.GetAxisRaw("Vertical"));
        position = transform.TransformDirection(position);
        position.y -= Gravity * Time.deltaTime;
        controller.Move(position * Time.deltaTime);
    }
}
