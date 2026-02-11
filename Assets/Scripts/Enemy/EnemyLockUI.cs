using UnityEngine;
public class EnemyLockUI : MonoBehaviour
{
    public RectTransform healthCanvas;
    public RectTransform staggerCanvas;
    public RectTransform chargeCanvas;
    void LateUpdate()
    {
        healthCanvas.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        staggerCanvas.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        chargeCanvas.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
