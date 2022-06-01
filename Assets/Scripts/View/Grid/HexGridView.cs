using System.Collections.Generic;
using System.Linq;
using Model.HexModel;
using Other.Convertible;
using UniRx;
using UnityEngine;
using View.Fabric;
using View.HexViews;
using ViewModel.HexGridVM;
using ViewModel.HexViewModel;

namespace View.HexGridV
{
    public class HexGridView : MonoBehaviour, IView
    {
        [SerializeField] private FabricHexView _fabricHexPrefabs;
        
        private IHexGridViewModel _hexGridViewModel;
        private Dictionary<Vector3, IHexView> _hexGirdDict = new Dictionary<Vector3, IHexView>();

        public void Init() { throw new System.NotImplementedException(); }
        public void Init(IHexGridViewModel hexGridViewModel)
        {
            _hexGridViewModel = hexGridViewModel;
            _hexGridViewModel.CreatedHexGrid += CreateHexGrid;
            _hexGridViewModel.AddedHex += OnCreateHex;
            _hexGridViewModel.RemovedHex += OnRemovedHex;
        }

        public void Save() => 
            _hexGridViewModel.Save();

        public void Load() => 
            _hexGridViewModel.Load();

        private void OnCreateHex(IHexViewModel hexViewModel)
        {
            IHexView newHexView = _fabricHexPrefabs.CreationHexView(hexViewModel);
        }

        private void CreateHexGrid(List<IHexViewModel> hexViewModelsGrid)
        {
            foreach (IHexViewModel hex in hexViewModelsGrid)
            {
                IHexView newHexView = _fabricHexPrefabs.CreationHexView(hex);
                _hexGirdDict.Add(hex.Position, newHexView);
            }
        }

        public IHexView GetNeighbour(Vector3 hexPosition ,HexDirection direction)
        {
            Vector3 neighbourPosition = _hexGridViewModel.GetNeighbourPosition(hexPosition, direction);
            return _hexGirdDict[neighbourPosition];
        }

        public List<IHexView> GetNeighbours(Vector3 hexPosition)
        {
            IEnumerable<Vector3> neighboursPosition = _hexGridViewModel.GetNeighboursPosition(hexPosition);
            List<IHexView> neighboursHex = neighboursPosition.Select(position => _hexGirdDict[position]).ToList();
            return neighboursHex;
        }

        private void OnRemovedHex(IHexViewModel hexViewModel) => 
            _hexGirdDict[hexViewModel.Position].Delete();

        private void OnDisable()
        {
            _hexGridViewModel.CreatedHexGrid -= CreateHexGrid;
            _hexGridViewModel.AddedHex -= OnCreateHex;
            _hexGridViewModel.RemovedHex -= OnRemovedHex;
        }
    }
}