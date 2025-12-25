using UnityEngine;

public class Node : MonoBehaviour
{
    public static Node instance;
    public NodeGrid nodeGrid;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
