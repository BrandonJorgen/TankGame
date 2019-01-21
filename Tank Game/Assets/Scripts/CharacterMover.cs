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

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentDirection = "Left";
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentDirection = "Right";
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            currentDirection = "Up";
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentDirection = "Down";
        }

        switch (currentDirection)
        {
                case "Right":
                    position.Set(MoveSpeed * Input.GetAxisRaw("Horizontal"), 0, 0);
                    position = transform.TransformDirection(position);
                    break;
                case "Left":
                    position.Set(MoveSpeed * Input.GetAxisRaw("Horizontal"), 0, 0);
                    position = transform.TransformDirection(position);
                    break;
                case "Up":
                    position.Set(0, 0, MoveSpeed * Input.GetAxisRaw("Vertical"));
                    position = transform.TransformDirection(position);
                    break;
                case "Down":
                    position.Set(0, 0, MoveSpeed * Input.GetAxisRaw("Vertical"));
                    position = transform.TransformDirection(position);
                    break;
        }
        position.y -= Gravity * Time.deltaTime;
        controller.Move(position * Time.deltaTime);
    }
}
