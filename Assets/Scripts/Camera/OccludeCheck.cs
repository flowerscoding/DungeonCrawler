using UnityEngine;

public class OccludeCheck : MonoBehaviour
{
    private Transform _playerTransform;
    private LayerMask _occluderMask;
    void Awake()
    {
        _occluderMask = LayerMask.GetMask("Occluder");
        _playerTransform = Player.instance.playerMovement.playerRB.transform;
    }
    void LateUpdate()
    {
        CheckForOccluders();
    }
    void CheckForOccluders()
    {
        float cameraOffset = -0.7f; //so the occluder ray doesn't hit things in front of the player
        Vector3 playerPos = _playerTransform.position + new Vector3(0, 0, cameraOffset);
        Vector3 dir = playerPos - transform.position;
        float distance = dir.magnitude;
        Vector3 halfExtents = new Vector3(1, 2, 1); //2, 2, 2 basically
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, halfExtents, dir, Quaternion.identity, distance, _occluderMask);

        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.TryGetComponent(out OcclusionFader occluder))
            {
                occluder.OccludingActivate();
            }
        }
    }
}
