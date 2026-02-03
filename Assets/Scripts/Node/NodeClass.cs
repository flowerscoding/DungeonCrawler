using UnityEngine;

public class NodeClass
{
    public Vector3 worldPos;
    public int nodeX;
    public int nodeY;
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;

    public enum State
    {
        Empty,
        Unwalkable,
        Player,
        Enemy,
        Boulder,
        Chest,
        Ladder,
        Destructible,
        Coffin,
        Queen,
        TransportDoor,
    }
    public State state;
    public EnemyController enemyController;
    public BoulderController boulderController;
    public LadderController ladderController;
    public DestructibleController destructibleController;
    public CoffinController coffinController;
    public NpcController npcController;
    public TransportController transportController;
    public NodeClass(Vector3 _worldPos, int _nodeX, int _nodeY)
    {
        worldPos = _worldPos;
        nodeX = _nodeX;
        nodeY = _nodeY;
    }
}
