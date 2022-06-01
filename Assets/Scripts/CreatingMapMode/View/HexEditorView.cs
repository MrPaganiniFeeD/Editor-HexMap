using CreatingMapMode.View;
using CreatingMapMode.ViewModel;
using Model.HexModel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using View;
using ViewModel.Input;
using Zenject;

namespace DefaultNamespace
{
    public class HexEditorView : MonoBehaviour, IView
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        private Camera _mainCamera;
        private InputViewModel _inputViewModel;
        private HexEditorViewModel _hexEditorViewModel;

        public void Init() { throw new System.NotImplementedException(); }

        [Inject]
        public void Construct(InputViewModel inputViewModel)
        {
            _inputViewModel = inputViewModel;
            _mainCamera = Camera.main;
        }

        public void Init(HexEditorViewModel hexEditorViewModel)
        {
            _hexEditorViewModel = hexEditorViewModel;
            _inputViewModel.ChangeMousePosition += FindHexView;
        }
        
        public void RewindAction() => 
            _hexEditorViewModel.DeleteLastAction();

        public void SetBrushSize(Slider slider) => 
            _hexEditorViewModel.SetBrushSize((int)slider.value);

        public void SetTypeHex(int hexBiomeIndex) =>
            _hexEditorViewModel.SetBiomeType((HexBiomeType)hexBiomeIndex);
        

        public void SetRiverMode(int riverModeIndex) => 
            _hexEditorViewModel.SetRiverMode((OptionalToggle) riverModeIndex);

        private void FindHexView(Vector2 mousePosition)
        {
            var ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, 300))
            {
                if (hit.collider.gameObject.TryGetComponent(out HexView hex))
                {
                    _hexEditorViewModel.EditSelectionHex(hex.Position);
                    return;
                }
                _hexEditorViewModel.EditSelectionHex(Vector3.zero);
            }
        }
        private void OnDisable() => 
            _inputViewModel.ChangeMousePosition -= FindHexView;
    }
}