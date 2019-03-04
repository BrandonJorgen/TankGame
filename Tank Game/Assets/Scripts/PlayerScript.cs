using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //TODO *OPTIONAL* ADD TANK TRACKS THAT SLOWLY DISAPPEAR AFTER A FEW SECONDS
        //Track previous position and current position
        //Check for distance between the two
        //If distance is greater than X, instantiate object
            //Issue with this may be based on direction the player is facing
            //Would need a system to determine what version of tracks to set based on said direction
    
    public FloatData MoveSpeed;
    private float gravity = 9.81f;
    private CharacterController controller;
    private Vector3 position;
    public FloatData PlayerHealth;
    private Animator animator;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        position.Set(MoveSpeed.Value * Input.GetAxisRaw("Horizontal"), 0, MoveSpeed.Value * Input.GetAxisRaw("Vertical"));
        position = transform.TransformDirection(position);
        position.y -= gravity * Time.deltaTime;
        if (controller.enabled)
        {
            controller.Move(position * Time.deltaTime);

            //Animation Block
            if (Input.GetKeyDown(KeyCode.A))//Moving Left
            {
                animator.SetBool("MovingLeft", true);
                animator.SetBool("MovingRight", false);
            }

            if (Input.GetKeyDown(KeyCode.D))//Moving Right
            {
                animator.SetBool("MovingLeft", false);
                animator.SetBool("MovingRight", true);
            }

            if (Input.GetKeyDown(KeyCode.W))//Moving Up
            {
                animator.SetBool("MovingUp", true);
                animator.SetBool("MovingDown", false);
            }

            if (Input.GetKeyDown(KeyCode.S))//Moving Down
            {
                animator.SetBool("MovingUp", false);
                animator.SetBool("MovingDown", true);
            }
            
            //Animation Buttons Released
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("MovingLeft", false);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("MovingRight", false);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("MovingUp", false);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("MovingDown", false);
            }
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
