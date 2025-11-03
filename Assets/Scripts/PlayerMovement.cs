using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    private Vector2 movement; // We want to save the "Vector 2" from the user pressing WSAD.
    private Rigidbody2D myBody; // The rigidbody we want to move.
    private Animator myAnimator; // Animator variable we can adjust in the code

    [SerializeField] private int speed = 5; // Speed for the human

    void Awake() // Only runs once on awake
    {
        myBody = GetComponent<Rigidbody2D>(); // Sets myBody rigidbody to the rigidbody on the gameobject this script is attached to.
        myAnimator = GetComponent<Animator>(); // We want to get the animator attached to our gameobject
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>(); // Movement is set to Vector2 from Input Action when the user presses WSAD.

        if (movement.x != 0 || movement.y != 0)
        { // set value.vector2 to [0,0] when WSAD isn't pressed
          // if we didnt do this, the player would constantly look up
          // so we only change animation if at least one of x and y isnt = 0
            myAnimator.SetFloat("x", movement.x); // Sets x in animator to movement.x from unity input.
            myAnimator.SetFloat("y", movement.y); // Sets y in animator to movement.y from unity input.

            myAnimator.SetBool("isWalking", true); // When either movement.x or y isnt 0, it means we are walking
        }
        else
        {
            myAnimator.SetBool("isWalking", false); // else, we are not walking
        }
    }

    // Fixed update is more reliable at certain things, such as moving.
    void FixedUpdate()
    {
        myBody.linearVelocity = movement * speed; // We set velocity of our rigidbody2D to the speed we defined.
    }
}
