using System;
using UnityEngine;
using View.HexViews;
using View.Selection;
using ViewModel.HexViewModel;

[SelectionBase]
public class HexView : MonoBehaviour, IHexView, ISelectable
{
    [SerializeField] private GlowHighlightView _glowHighlight;
    [SerializeField] private HexTypeSwitcherView _hexTypeSwitcherView;
    [SerializeField] private RoadHexView _roadHexView; 
    
    public Vector3 Position => _transform.position;
    
    private IHexViewModel _hexViewModel;
    private Transform _transform;

    public void Init() { throw new NotImplementedException(); }
    public void Init(IHexViewModel hexViewModel)
    {
        _transform = GetComponent<Transform>();
        _hexViewModel = hexViewModel;
        _hexViewModel.Delete += Delete;
        
        _roadHexView.Init(_hexViewModel);
        _glowHighlight.Init();
        _hexTypeSwitcherView.Init(_hexViewModel);
    }
    
    public void EnableSelection() => 
        _glowHighlight.ActivateGlow();

    public void DisableSelection() => 
        _glowHighlight.DeactivateGlow();

    public void Delete()
    {
        _hexViewModel.Delete -= Delete;
        _hexViewModel.Dispose();
        Destroy(gameObject);
    }
}
