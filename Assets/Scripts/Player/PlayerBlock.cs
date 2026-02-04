using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    public ParticleSystem parryParticle;
    public Light[] parryLights;
    private InputAction _blockAction;
    public bool okayToBlock { get; private set; }
    public bool holdingBlock;

    public float parryCoolDown;

    public bool okayToParry;
    public bool parryInitialized;
    public float initializeParryTimer;
    public bool parryActive;
    public bool parried;
    void Awake()
    {
        _blockAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Block");
        okayToBlock = true;
        okayToParry = true;
    }
    void OnEnable()
    {
        _blockAction.performed += BlockPressed;
        _blockAction.canceled += BlockLetGo;
    }
    public void ParryBlockOn()
    {
        BlockOn();
        okayToParry = true;
    }
    void BlockLetGo(InputAction.CallbackContext ctx)
    {
        holdingBlock = false;
        parryInitialized = false;
        if (Player.instance.playerState.state == PlayerState.State.Block
        && TurnManager.instance.state == TurnManager.State.PlayerTurn)
        {
            Player.instance.StateChange(PlayerState.State.Idle);
            Player.instance.animateMachine.ResetMachine();
        }
        if (TurnManager.instance.state == TurnManager.State.PlayerTurn
        && Player.instance.playerState.state == PlayerState.State.Idle)
        {
            okayToBlock = true;
        }

    }
    void BlockPressed(InputAction.CallbackContext ctx)
    {
        InputManager.instance.ControllerTypeCheck(ctx);
        if (Player.instance.playerState.state == PlayerState.State.Climb ||
        TurnManager.instance.state == TurnManager.State.Dialogue) return;
        if (okayToBlock)
        {
            holdingBlock = true;
            okayToBlock = false;
            if (okayToParry)
            {
                okayToParry = false;
                StartCoroutine(InitializeParry());
                parryInitialized = true;
            }
            else
                parryInitialized = false;

            Player.instance.StateChange(PlayerState.State.Block);
        }
        else
        {
            holdingBlock = false;
            okayToBlock = false;
            parryActive = false;
            parryInitialized = false;
        }
    }
    public void BlockOff()
    {
        okayToBlock = false;
        parryActive = false;
        parryInitialized = false;
    }
    public void BlockOn()
    {
        okayToBlock = true;
    }
    public void SuccessfulParry(EnemyController controller)
    {
        
        Player.instance.OccludePlayer(false);

        parryParticle.Play();
        StartCoroutine(ParryLights());

        Player.instance.animateMachine.ResetMachine();

        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);

        controller.TakeStagger(Player.instance.playerData.weapon.data.stagger);
    }
    public void ResetParried()
    {
        parried = false;
    }
    IEnumerator InitializeParry()
    {
        float timer = initializeParryTimer;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (timer <= 0 && parryInitialized)
        {
            parryActive = true;
            StartCoroutine(ParryCooldown());

            EnemyController enemyController = Player.instance.playerInteract.curInteractingNode.enemyController;
            if (enemyController != null)
            {
                if (enemyController.ParryableCheck())
                {
                    parried = true;
                    Player.instance.playerBlock.SuccessfulParry(enemyController);
                    Player.instance.playerBlock.BlockOn();
                }
            }
        }
    }
    IEnumerator ParryCooldown()
    {
        float cooldown = parryCoolDown;
        while (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        okayToParry = true;
        parryInitialized = false;
        parryActive = false;
    }
    IEnumerator ParryLights()
    {
        foreach (Light light in parryLights)
            light.intensity = 4;
        float cooldown = 0.1f;
        while (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        if (cooldown <= 0)
            foreach (Light light in parryLights)
                light.intensity = 0;
    }
}
