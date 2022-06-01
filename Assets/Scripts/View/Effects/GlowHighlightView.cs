using System.Collections.Generic;
using UnityEngine;
using View;

public class GlowHighlightView : MonoBehaviour, IView
{
    [SerializeField] private Material _glowMaterial;

    private Dictionary<Renderer, Material[]> _glowMaterials = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> _originalMaterials = new Dictionary<Renderer, Material[]>();

    private Dictionary<Color, Material> _cachedGlowMaterials = new Dictionary<Color, Material>();

    private bool _isGlowing = false;
    
    public void Init()
    {
        PrepareMaterialDictionaries();
    }
    
    private void PrepareMaterialDictionaries()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            var originalMaterials = renderer.materials;
            _originalMaterials.Add(renderer, originalMaterials);
            
            var newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material material;
                if (_cachedGlowMaterials.TryGetValue(originalMaterials[i].color, out material) == false)
                {
                    material = new Material(_glowMaterial)
                    {
                        color = originalMaterials[i].color
                    };
                    _cachedGlowMaterials[material.color] = material;
                }

                newMaterials[i] = material;
            }
            _glowMaterials.Add(renderer, newMaterials);
        }
    }

    public void ActivateGlow()
    { 
        if(_isGlowing)
            return;
        
        SetMaterials(_glowMaterials);
        _isGlowing = true;
    }

    public void DeactivateGlow()
    {
        if(_isGlowing == false)
            return;
        
        SetMaterials(_originalMaterials);
        _isGlowing = false;
    }

    private void SetMaterials(Dictionary<Renderer, Material[]> newMaterials)
    {
        foreach (var renderer in newMaterials.Keys)
        {
            renderer.materials = newMaterials[renderer];
        }
    }
}
