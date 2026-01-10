using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapping : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    public InputActionMap currentMap { get; private set; }
    public enum MapType
    {
        Player,
        ActionsMenu,
        RestMenu,
    }
    void Awake()
    {
        _inputActions = InputManager.instance.inputActions;
        currentMap = _inputActions.asset.actionMaps[0];
    }
    public void MapChange(MapType newMap)
    {
        string mapName = newMap.ToString();
        StartCoroutine(EnablePause(mapName));
    }
    IEnumerator EnablePause(string mapName) //prevent the next map to press auto
    {
        _inputActions.asset.Disable(); //disables all maps
        InputActionMap nextMap = _inputActions.asset.FindActionMap(mapName);

        yield return null;

        nextMap.Enable();
        currentMap = nextMap;
    }
}
