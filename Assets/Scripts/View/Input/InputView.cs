using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ViewModel.Input;

namespace View.Input
{
    public class InputView : MonoBehaviour, IView
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private string _hexLayerName;
        
        private PlayerInput _playerInput;

        private InputViewModel _inputViewModel;

        public void Init() { throw new System.NotImplementedException(); }
        public void Init(InputViewModel inputViewModel)
        {
            _inputViewModel = inputViewModel;
            _playerInput = new PlayerInput(); 
            _playerInput.Enable();
            _playerInput.Player.ScrollWheel.performed += ScrollWheelHandler;
            _playerInput.Player.ClickPosition.performed += ClickPositionHandler;
            _playerInput.Player.Click.performed += ChangeClickOnHex;
            _playerInput.Player.Click.canceled += HandlerEndOfTheClick;

        }

        private void HandlerEndOfTheClick(InputAction.CallbackContext obj) => 
            _inputViewModel.InvokeEndClick();

        private void Update()
        {
            ClampingLeftButton();
            MovementDirectionHandler();
            RotationChecker();
        }

        private void ClampingLeftButton()
        {
            if (_playerInput.Player.Click.phase == InputActionPhase.Started)
            {
                Vector2 clickPosition = Mouse.current.position.ReadValue();
                _inputViewModel.SetCurrentMousePosition(clickPosition);
            }
        }

        private void MovementDirectionHandler()
        {
            Vector2 direction = _playerInput.Player.Movement.ReadValue<Vector2>();
            _inputViewModel.SetMovementDirection(direction);
        }

        private void RotationChecker()
        {
            float delta = _playerInput.Player.Rotation.ReadValue<float>();
            _inputViewModel.SetRotation(delta);
        }

        private void ChangeClickOnHex(InputAction.CallbackContext obj)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 300))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(_hexLayerName))
                    _inputViewModel.ChangeClickOnHex();
            }
        }
        
        private void ClickPositionHandler(InputAction.CallbackContext obj)
        {
            Vector2 clickPosition = Mouse.current.position.ReadValue();
            _inputViewModel.SetClickPosition(clickPosition);
        }

        private void ScrollWheelHandler(InputAction.CallbackContext obj) => 
            _inputViewModel.SetScrollWheel(obj.ReadValue<Vector2>());

        private void OnDisable()
        {
            _playerInput.Player.ScrollWheel.performed -= ScrollWheelHandler;
            _playerInput.Player.ClickPosition.performed -= ClickPositionHandler;
            _playerInput.Player.Click.performed -= ChangeClickOnHex;
            _playerInput.Player.Click.canceled -= HandlerEndOfTheClick;
        }
    }
}