using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager Instance;
    public GameObject damageCanvasPrefab;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public void SpawnDamage(int damage, Transform parent, Color textColor)
    {
        GameObject canvasClone = Instantiate(damageCanvasPrefab, parent);
        DamageMovement damageMovement = canvasClone.GetComponentInChildren<DamageMovement>();
        TextMeshProUGUI textMesh = canvasClone.GetComponentInChildren<TextMeshProUGUI>();

        damageMovement.enabled = true;
        textMesh.text = damage.ToString();
        textMesh.color = textColor;
        canvasClone.transform.position = parent.position;
    }
}
