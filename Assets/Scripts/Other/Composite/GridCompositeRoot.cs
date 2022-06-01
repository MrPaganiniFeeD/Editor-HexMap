using Model.Fabrics;
using Model.GenerationHexGrid;
using Model.HexModel;
using UnityEngine;
using View.HexGridV;
using ViewModel.HexGridVM;
using Zenject;

namespace Other.Composite
{
    public class GridCompositeRoot : CompositeRoot
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private HexGridView _hexGridView;

        private PerlinNoiseHexGridGenerator _noiseHexGridGenerator;
        private HexGrid _hexGrid;
        private HexGridViewModel _hexGridViewModel;
        private IFabricHex _fabricHex;

        public override void InitBindings(DiContainer container)
        {
            base.InitBindings(container);
            BindHexGrid();
            CreateFabricHex();
            CreateGridViewModel();
            BindHexGridView();
            CreateGrid();
        }

        private void BindHexGrid()
        {
            _hexGrid = new HexGrid(_gridSize.y, _gridSize.x);
            DiContainer.Bind<HexGrid>().FromInstance(_hexGrid).AsSingle();
        }

        private void BindHexGridView()
        {
            _hexGridView.Init(_hexGridViewModel);
            DiContainer.Bind<HexGridView>().FromInstance(_hexGridView).AsSingle();
        }

        private void CreateGridViewModel()
        {
            _hexGridViewModel = new HexGridViewModel(_hexGrid, _fabricHex, new FabricHexViewModel());
        }

        private void CreateFabricHex()
        {
            _fabricHex = new FabricHex(_hexGrid);
        }

        public void CreateGrid()
        {
            PerlinNoiseCreateGrid();
        }
        public void PerlinNoiseCreateGrid()
        {
            _noiseHexGridGenerator = new PerlinNoiseHexGridGenerator(_hexGrid);
            _noiseHexGridGenerator.GeneratorMap();   
        }
    }
}