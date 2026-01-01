using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapping : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    public InputActionMap currentMap { get; private set; }
    void Awake()
    {
        _inputActions = InputManager.instance.inputActions;
        currentMap = _inputActions.asset.actionMaps[0];
    }
    public void MapChange(string newMap)
    {
        StartCoroutine(EnablePause(newMap));
    }
    IEnumerator EnablePause(string newMap) //prevent the next map to press auto
    {
        _inputActions.asset.Disable(); //disables all maps
        InputActionMap nextMap = _inputActions.asset.FindActionMap(newMap);

        yield return null;

        nextMap.Enable();
        currentMap = nextMap;
    }
}
