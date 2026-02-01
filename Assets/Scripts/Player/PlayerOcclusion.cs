using UnityEngine;

public class PlayerOcclusion : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] skinnedMeshes;
    [SerializeField] MeshRenderer[] meshes;

    [SerializeField] SkinnedMeshRenderer[] transparentSkinnedMeshes;
    [SerializeField] MeshRenderer[] transparentMeshes;
    public void OccludePlayer(bool occlude)
    {
        if(Player.instance.playerInteract.curInteractingNode == null) return;

        if(occlude)
        {
            foreach(SkinnedMeshRenderer mesh in skinnedMeshes)
                mesh.enabled = false;
            foreach(MeshRenderer mesh in meshes)
                mesh.enabled = false;
            foreach(SkinnedMeshRenderer mesh in transparentSkinnedMeshes)
                mesh.enabled = true;
            foreach(MeshRenderer mesh in transparentMeshes)
                mesh.enabled = true;
        }
        else
        {
            foreach(SkinnedMeshRenderer mesh in skinnedMeshes)
                mesh.enabled = true;
            foreach(MeshRenderer mesh in meshes)
                mesh.enabled = true;
            foreach(SkinnedMeshRenderer mesh in transparentSkinnedMeshes)
                mesh.enabled = false;
            foreach(MeshRenderer mesh in transparentMeshes)
                mesh.enabled = false;
        }
    }
}
