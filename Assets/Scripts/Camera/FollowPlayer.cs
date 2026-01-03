using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform _playerTransform;

    public float yOffset;
    public float zOffset;
    void Awake()
    {
        _playerTransform = Player.instance.playerMovement.playerRB.transform;
    }
    public void LateUpdate()
    {
        Vector3 pos = new Vector3(_playerTransform.position.x, _playerTransform.position.y + yOffset, _playerTransform.position.z + zOffset);
        transform.position = pos;
    }
}
