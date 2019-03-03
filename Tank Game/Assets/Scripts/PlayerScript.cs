using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //TODO ADD ANIMATION SECTION
    
    public FloatData MoveSpeed;
    private float gravity = 9.81f;
    private CharacterController controller;
    private string currentDirection;
    private Vector3 position;
    private Vector3 rotation;
    public FloatData PlayerHealth;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        position.Set(MoveSpeed.Value * Input.GetAxisRaw("Horizontal"), 0, MoveSpeed.Value * Input.GetAxisRaw("Vertical"));
        position = transform.TransformDirection(position);
        position.y -= gravity * Time.deltaTime;
        if (controller.enabled)
        {
            controller.Move(position * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("EnemyShell"))
        {
            PlayerHealth.Value -= other.gameObject.GetComponent<DestroyObject>().ShellDamage.Value;
        }
    }
}
