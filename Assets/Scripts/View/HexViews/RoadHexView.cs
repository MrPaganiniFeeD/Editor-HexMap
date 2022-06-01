using System;
using System.Collections.Generic;
using System.Linq;
using Model.HexModel;
using Model.HexModel.TerrainHex;
using TMPro;
using UniRx;
using UnityEngine;
using ViewModel.HexViewModel;

namespace View.HexViews
{
    public class RoadHexView : MonoBehaviour, IView
    {
        [SerializeField] private HexTypeSwitcherView _hexTypeSwitcherView;

        [Header("Road")]
        [SerializeField] private List<HexRoadPrefabInfo> _hexRoadPrefabInfos;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private RoadTerrainViewModel _roadTerrainViewModel;
        private GameObject _activePrefab;
        private IHexViewModel _hexViewModel;
        private HexBiomeType _roadBiomeType;
        private int _countRoad;
        private bool _incomingRoad;
        private bool _outgoingRoad;
        
        public void Init() { throw new System.NotImplementedException(); }
        public void Init(IHexViewModel hexViewModel)
        {
            _hexViewModel = hexViewModel;
            _hexViewModel.GetTerrain().Skip(1).Subscribe(UpdateView).AddTo(_disposable);
        }

        private void UpdateView(ITerrainViewModel terrainViewModel)
        {
            if(terrainViewModel == null)
                return;
            
            if (terrainViewModel is RoadTerrainViewModel roadTerrainViewModel)
            {
                _roadTerrainViewModel = roadTerrainViewModel;
                _roadTerrainViewModel.RefreshedValue += OnRefreshValue;
                OnRefreshValue();
                SelectRoadPrefab();
            }
            
        }

        private void SelectRoadPrefab()
        {
            switch (_countRoad)
            {
                case 0:
                    break;
                case 1:
                    if (TryGetSearchableRoad(out HexRoadPrefabInfo hexOneRoadPrefabInfo))
                        ActivatePrefab(hexOneRoadPrefabInfo.gameObject);
                    break;
                
                case 2:
                    if (TryGetSearchableRoad(out HexRoadPrefabInfo hexTwoRoadPrefabInfo))
                        ActivatePrefab(hexTwoRoadPrefabInfo.gameObject);

                    break;
                
                case 3:
                    if (TryGetSearchableRoad(out HexRoadPrefabInfo hexThreeRoadPrefabInfo))
                        ActivatePrefab(hexThreeRoadPrefabInfo.gameObject);
                    break;
                
                case 4:
                    if (TryGetSearchableRoad(out HexRoadPrefabInfo hexFourRoadPrefabInfo))
                        ActivatePrefab(hexFourRoadPrefabInfo.gameObject);
                    break;
                
                case 5:
                    if (TryGetSearchableRoad(out HexRoadPrefabInfo hexFiveRoadPrefabInfo))
                        ActivatePrefab(hexFiveRoadPrefabInfo.gameObject);

                    break;
                
                case 6:
                    if (TryGetSearchableRoad(out HexRoadPrefabInfo hexSixRoadPrefabInfo))
                        ActivatePrefab(hexSixRoadPrefabInfo.gameObject);
                    break;
            }
        }

        private bool TryGetSearchableRoad(out HexRoadPrefabInfo roadPrefabInfo)
        {
            
            foreach (HexRoadPrefabInfo prefabInfo in _hexRoadPrefabInfos)
            {
                if (prefabInfo.HexBiomeType == _roadBiomeType && prefabInfo.NumberOfRoad == _countRoad) 
                {
                    roadPrefabInfo = prefabInfo;
                    return true;
                }
            }

            roadPrefabInfo = null; 
            return false;
        }

        private void ActivatePrefab(GameObject model)
        {
            _activePrefab?.SetActive(false);
            _activePrefab = model;
            _hexTypeSwitcherView.DisableModel();
            _activePrefab.SetActive(true);
        }
        private void OnRefreshValue()
        {
            _countRoad = _roadTerrainViewModel.NumberOfNeighbourRoad;
            _roadBiomeType = _roadTerrainViewModel.BiomeTypeRoad;

            SelectRoadPrefab();
        }

        private void OnDestroy()
        {
            if(_roadTerrainViewModel != null)
                _roadTerrainViewModel.RefreshedValue += OnRefreshValue;
        }
    }
}