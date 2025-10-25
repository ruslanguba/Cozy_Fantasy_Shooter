using System;
using UnityEngine;

public class Activator : InterractableBase
{
    public event Action OnActivate;
    public event Action OnDeactivate;
    [SerializeField] private Material _activeMaterial;
    [SerializeField] private Material _inactiveMaterial;
    [SerializeField] private MeshRenderer _buttonRender;
    private bool _active;
    public override void Interract(Inventory inventory)
    {
        if (_active)
            Diactivate();
        else
            Activate();
    }

    private void Activate()
    {
        if (!_active)
        {
            _active = true;
            _buttonRender.material = _activeMaterial;
            OnActivate?.Invoke();
        }
        
    }

    public void Diactivate()
    {
        _active = false;
        _buttonRender.material = _inactiveMaterial;
        OnDeactivate?.Invoke();
    }
}
