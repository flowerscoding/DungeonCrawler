using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerData playerData;
    public PlayerMovement playerMovement;
    public PlayerGridAgent gridAgent;
    public PlayerAttack playerAttack;
    public AnimateMachine animateMachine;
    public PlayerState playerState;
    public PlayerInteract playerInteract;
    public PlayerBlock playerBlock;
    public  PlayerDeath playerDeath;
    public PlayerOcclusion playerOcclusion;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }
    public void TakeDamage(int damage)
    {
        playerData.curHealth -= damage;
        if(playerData.curHealth > 0)
            StateChange(PlayerState.State.Hurt);
        else if(playerData.curHealth <= 0)
            StateChange(PlayerState.State.Dead);
    }
    public void StateChange(PlayerState.State newState)
    {
        playerState.NewState(newState);
    }
    public void MoveBoulder(NodeClass goalNode)
    {
        playerMovement.MoveBoulder(goalNode);
    }
    public void Climb(NodeClass ladderNode)
    {
        playerMovement.Climb(ladderNode);
    }
    public void ParryBlockOn()
    {
        playerBlock.ParryBlockOn();
    }
    public void InitializeStartNode(NodeClass playerNode)
    {
        gridAgent.InitializeStartNode(playerNode);
    }
    public void PlayerDied()
    {
        playerDeath.DeathScreen();
    }
    public void SuccessfulParry(EnemyController controller)
    {
        playerBlock.SuccessfulParry(controller);
    }
    public void OccludePlayer(bool occlude)
    {
        playerOcclusion.OccludePlayer(occlude);
    }
}
