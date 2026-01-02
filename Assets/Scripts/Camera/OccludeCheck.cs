using UnityEngine;

public class OccludeCheck : MonoBehaviour
{
    public Transform playerTransform;
    private LayerMask _occluderMask;
    void Awake()
    {
        _occluderMask = LayerMask.GetMask("Occluder");
    }
    void LateUpdate()
    {
        CheckForOccluders();
    }
    void CheckForOccluders()
    {
        Vector3 dir = playerTransform.position - transform.position;
        float distance = dir.magnitude;
        float radius = 0.4f;
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
