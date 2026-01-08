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
    public bool parryIntialized;
    public float initializeParryTimer;
    public bool parryActive;
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
        parryIntialized = false;
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
        if(Player.instance.playerState.state == PlayerState.State.Climb) return;
        if (okayToBlock)
        {
            holdingBlock = true;
            okayToBlock = false;
            if (okayToParry)
            {
                okayToParry = false;
                StartCoroutine(InitializeParry());
                parryIntialized = true;
            }
            else
                parryIntialized = false;

            Player.instance.StateChange(PlayerState.State.Block);
        }
        else
        {
            holdingBlock = false;
            okayToBlock = false;
            parryActive = false;
            parryIntialized = false;
        }
    }
    public void BlockOff()
    {
        okayToBlock = false;
        parryActive = false;
        parryIntialized = false;
    }
    public void BlockOn()
    {
        okayToBlock = true;
    }
    public void SuccessfulParry(EnemyController controller)
    {
        parryParticle.Play();
        StartCoroutine(ParryLights());

        Player.instance.animateMachine.ResetMachine();

        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);

        controller.TakeStagger(Player.instance.playerData.weapon.data.stagger);
    }
    IEnumerator InitializeParry()
    {
        float timer = initializeParryTimer;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (timer < 0 && parryIntialized)
        {
            parryActive = true;
            StartCoroutine(ParryCooldown());
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
        parryIntialized = false;
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
