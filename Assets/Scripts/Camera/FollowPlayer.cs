using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    private Transform _playerTransform;

    public float zoomMin;
    public float zoomMax;
    public float yOffset;
    public float zOffset;
    private InputAction _zoomAction;
    private Vector2 _zoomValue;
    void Awake()
    {
        _playerTransform = Player.instance.playerMovement.playerRB.transform;
        _zoomAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Zoom");
    }
    void OnEnable()
    {
        _zoomAction.performed += ZoomExecuted;
    }
    public void LateUpdate()
    {
        Vector3 offset =
        transform.rotation * new Vector3(0f, yOffset, zOffset);
        transform.position = _playerTransform.position + offset;
    }
    public void ZoomExecuted(InputAction.CallbackContext ctx)
    {
        _zoomValue = ctx.ReadValue<Vector2>();

        zOffset = Mathf.Clamp(_zoomValue.y + zOffset, zoomMin, zoomMax);
        
    }
}
