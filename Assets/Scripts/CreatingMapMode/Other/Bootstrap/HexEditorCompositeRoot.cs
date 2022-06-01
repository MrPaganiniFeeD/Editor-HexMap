using CreatingMapMode.Model;
using CreatingMapMode.ViewModel;
using DefaultNamespace;
using Model.HexModel;
using Other.Composite;
using UnityEngine;
using View.HexGridV;
using Zenject;

namespace CreatingMapMode.Other.Bootstrap
{
    public class HexEditorCompositeRoot : CompositeRoot
    {
        [SerializeField] private HexEditorView hexEditorView;

        private HexEditor _hexEditor;
        private HexEditorViewModel _hexEditorViewModel;
        private HexGrid _hexGrid;
        private InputPlayer _inputPlayer;

        [Inject]
        public void Construct(HexGrid hexGrid, InputPlayer inputPlayer)
        {
            _inputPlayer = inputPlayer;
            _hexGrid = hexGrid;
        }
        
        private void Awake()
        {
            _hexEditor = new HexEditor(_hexGrid, _inputPlayer);
            
            _hexEditorViewModel = new HexEditorViewModel(_hexEditor, _hexGrid);

            hexEditorView.Init(_hexEditorViewModel);
        }
    }
}