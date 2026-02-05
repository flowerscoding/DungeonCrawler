using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneSystem : MonoBehaviour
{
    public static SceneSystem instance;
    public SceneState sceneState;
    public List<ResetScript> resetScripts = new();
    public interface ResetScript
    {
        void ResetState();
    }
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Update()
    {
        if(Keyboard.current.pKey.wasPressedThisFrame)
            StateChange(SceneState.State.Reset);
    }
    public void Register(ResetScript script)
    {
        resetScripts.Add(script);
    }
    public void StateChange(SceneState.State newState)
    {
        sceneState.StateChange(newState);
        sceneState.RunState();
    }
    public void RunReset()
    {
        foreach(ResetScript script in resetScripts)
            script.ResetState();
    }
    public SceneState.SceneType GetSceneType()
    {
        return sceneState.GetSceneType();
    }
}
