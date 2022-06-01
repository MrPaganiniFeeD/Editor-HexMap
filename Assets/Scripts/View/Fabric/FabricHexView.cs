using System;
using Model.HexModel;
using Other.Convertible;
using TMPro;
using UnityEngine;
using View.HexViews;
using ViewModel.HexViewModel;

namespace View.Fabric
{
    public class FabricHexView : MonoBehaviour, IFabricHexPrefabs
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private HexView _hexView;
        
        [Header("Coordinates Info")]
        [SerializeField] private bool _isCoordinatesInfo;
        [SerializeField] private TMP_Text _coordinatesInfo;
        [SerializeField] private Canvas _gridCanvas;
        
        public IHexView CreationHexView(IHexViewModel hexViewModel)
        {
            var creationPosition = hexViewModel.Position;
            if (_isCoordinatesInfo)
                CreateCoordinatesLabel(creationPosition, hexViewModel);
            var creationPrefab = InstantiatePrefab(_hexView, creationPosition, hexViewModel);
            return creationPrefab;
        }

        private void CreateCoordinatesLabel(Vector3 creationPosition, IHexViewModel hexViewModel)
        {
            var label = Instantiate(_coordinatesInfo, _gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(creationPosition.x, creationPosition.z);
            var coordinates = creationPosition.GetConvertibleCoordinates();
            label.text = coordinates.x + "\n" + coordinates.z;
        }
        
        private HexView InstantiatePrefab(HexView hexPrefab, Vector3 position, IHexViewModel hexViewModel)
        {
            var hexView = Instantiate(hexPrefab, position, Quaternion.identity, _parent);
            hexView.Init(hexViewModel);
            return hexView;
        }
    }
}