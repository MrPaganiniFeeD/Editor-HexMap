using System;
using DefaultNamespace;
using UniRx;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SearchService;

namespace ViewModel.Input
{
    public class InputViewModel : IViewModel
    {
        public event Action<Vector2> ClickedPosition;
        public event Action<Vector2> ChangeMousePosition;
        
        private InputPlayer _inputPlayer;
        
        public InputViewModel(InputPlayer inputPlayer)
        {
            _inputPlayer = inputPlayer;
        }
        public void SetScrollWheel(Vector2 delta) => 
            _inputPlayer.SetScrollWheel(delta.y);

        public void SetMovementDirection(Vector2 direction) => 
            _inputPlayer.SetMoveDirection(direction.x, direction.y);

        public void SetClickPosition(Vector2 position) => 
            ClickedPosition?.Invoke(position);

        public void InvokeEndClick() => 
            _inputPlayer.InvokeEndClick();

        public void SetCurrentMousePosition(Vector2 position) => 
            ChangeMousePosition?.Invoke(position);

        public void SetRotation(float delta) => 
            _inputPlayer.SetRotation(delta);

        public void ChangeClickOnHex() => 
            _inputPlayer.InvokeClick();
    }
}