using System;
using Model.HexModel;
using UniRx;
using UnityEngine;
using View;
using View.HexViews;

namespace ViewModel.HexViewModel
{
    public class HexTypeSwitcherView : MonoBehaviour
    {
        [SerializeField] private GameObject _forestModel;
        [SerializeField] private GameObject _sandModel;
        [SerializeField] private GameObject _snowModel;
        [SerializeField] private GameObject _seaModel;
        [SerializeField] private GameObject _shallowSeaModel;
        [SerializeField] private GameObject _defaultModel;


        private CompositeDisposable _disposable = new CompositeDisposable();
        private IHexViewModel _hexViewModel;
        private GameObject _currentHexModel;
        
        
        
        public void Init() { throw new System.NotImplementedException(); }
        public void Init(IHexViewModel hexViewModel)
        {
            _hexViewModel = hexViewModel;
            _hexViewModel.GetHexType().Subscribe(SetHexType).AddTo(_disposable);
        }

        public void SetHexType(HexBiomeType hexType)
        {
            _defaultModel.SetActive(false);
            _currentHexModel?.SetActive(false);
            switch (hexType)
            {
                case HexBiomeType.Forest:
                {
                    _currentHexModel = _forestModel;
                    _currentHexModel.SetActive(true);
                    break;
                }
                case HexBiomeType.Sand:
                {
                    _currentHexModel = _sandModel;
                    _currentHexModel.SetActive(true);
                    break;
                }
                case HexBiomeType.Sea:
                {
                    _currentHexModel = _seaModel;
                    _currentHexModel.SetActive(true);
                    break;
                }
                case HexBiomeType.ShallowSea:
                {
                    _currentHexModel = _shallowSeaModel;
                    _currentHexModel.SetActive(true);
                    break;
                }
                case HexBiomeType.Snow:
                {
                    _currentHexModel = _snowModel;
                    _currentHexModel.SetActive(true);
                    break;
                }
                default:
                    _defaultModel.SetActive(true);
                    break;
            }
        }

        public void DisableModel()
        {
            _currentHexModel.SetActive(false);
        }
    }
}