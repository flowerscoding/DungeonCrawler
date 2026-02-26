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
    public PlayerDeath playerDeath;
    public PlayerOcclusion playerOcclusion;
    public ItemUsage itemUsage;
    public PlayerSurrounding playerSurrounding;
    public PlayerLoad playerLoad;
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
        playerData.TakeDamage(damage);
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
    public void LoadPlayerData()
    {
        playerData.LoadData();
    }
    public void SceneUpdate(LoadSystem.Scene scene)
    {
        playerData.SceneUpdate(scene);
    }
    public void SpawnPointUpdate(string newSpawnPoint)
    {
        playerData.SpawnPointUpdate(newSpawnPoint);
    }
    public bool UseItem(ItemData itemData)
    {
        bool used = itemUsage.UseItem(itemData);
        return used;
    }
    public void HealPlayer(int amount)
    {
        playerData.Heal(amount);
    }
    public void Attack()
    {
        playerAttack.AttackPressed();
    }
    public void SuspendMovement(bool suspend)
    {
        playerMovement.SuspendMovement(suspend);
    }
    public void EnableAttackCharge(bool enableUI, bool runCharge)
    {
        playerAttack.EnableAttackCharge(enableUI, runCharge);
    }
    public bool CheckSurrounding(PlayerSurrounding.CheckType type)
    {
        return playerSurrounding.CheckFor(type);
    }
    public void LoadPlayer()
    {
        playerLoad.LoadPlayer();
    }
}
