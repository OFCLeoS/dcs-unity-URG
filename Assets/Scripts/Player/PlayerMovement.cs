using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction movementDirection;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed;
    
    void Awake()
    {
        movementDirection = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input= movementDirection.ReadValue<Vector2>().normalized;
        Vector3 move = new Vector3(input.x*speed*Time.deltaTime, 0, input.y*speed*Time.deltaTime);
        controller.Move(move);
    }
}
