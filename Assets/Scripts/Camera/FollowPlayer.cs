using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    public float yOffset;
    public float zOffset;
    public void LateUpdate()
    {
        Vector3 pos = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
        transform.position = pos;
    }
}
