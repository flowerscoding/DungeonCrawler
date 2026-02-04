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
        float radius = 1f;
        Ray ray = new Ray(transform.position, dir);
        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, distance, _occluderMask);

        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.TryGetComponent(out OcclusionFader occluder))
            {
                occluder.OccludingActivate();
            }
        }
    }
}
