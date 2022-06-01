using System;
using UniRx;
using UnityEngine;
using View.HexGridV;
using ViewModel.Input;
using Zenject;

namespace View.Selection
{
    public class SelectionView : MonoBehaviour, IView
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _selectionMask;


        private ISelectable _selectable;
        private InputViewModel _playerInput;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Init() { throw new System.NotImplementedException(); }

        [Inject]
        private void Init(InputViewModel playerInput, HexGridView hexGridView)
        {
            _playerInput = playerInput;
            _playerInput.ClickedPosition += SelectionHex;
        }

        private void SelectionHex(Vector2 mousePosition)
        {
            if (FindGameObjectOfType(mousePosition, out ISelectable selectable))
            {   
                _selectable?.DisableSelection();
                _selectable = selectable;
                _selectable.EnableSelection();
            }
        }

        private bool FindGameObjectOfType<T>(Vector2 mousePosition, out T result)
        {
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, _selectionMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out T view))
                {
                    result = view;
                    return true;
                }
            }
            result = default(T);
            return false;
        }

        public void OnDisable()
        {
            _playerInput.ClickedPosition -= SelectionHex;
        }
    }
}