using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRotation : MonoBehaviour
{
   InputAction rotationDirection;
    void Awake()
    {
      
        rotationDirection = InputSystem.actions.FindAction("Point");
    }

  
    void Update()
    {
        Vector2 newValue =rotationDirection.ReadValue<Vector2>();
        Vector3 worldValue = Camera.main.ScreenToWorldPoint( new Vector3(newValue.x, newValue.y, 
        Camera.main.transform.position.y - transform.position.y));
        Vector3 direction = new Vector3(worldValue.x - transform.position.x,0,worldValue.z-transform.position.z);
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
