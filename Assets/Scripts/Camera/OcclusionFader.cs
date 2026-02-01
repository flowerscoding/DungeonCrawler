using UnityEngine;

public class OcclusionFader : MonoBehaviour
{
    Renderer _renderer;
    bool _occluding;
    float _alpha;
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        if (!_occluding)
            _alpha = 1f;
            
        Color newColor = _renderer.material.color;
        newColor.a = _alpha;
        _renderer.material.color = newColor;
        
        _occluding = false;

    }

    public void OccludingActivate()
    {
        _occluding = true;
        _alpha = 0f;
    }
}
