using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class SelectLevel : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction pressPositionAction;
    private RaycastHit hit;
    private Vector2 pressPosition;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pressPositionAction = playerInput.actions.FindAction("PressPosition");
    }

    public void OnScreenPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pressPosition = pressPositionAction.ReadValue<Vector2>();
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(pressPosition.x, pressPosition.y));
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }
}