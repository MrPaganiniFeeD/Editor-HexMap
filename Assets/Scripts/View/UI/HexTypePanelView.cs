using System;
using CreatingMapMode.ViewModel;
using Model.HexModel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace DefaultNamespace
{
    [RequireComponent(typeof(ToggleGroup))]
    public class HexTypePanelView : MonoBehaviour, IView
    {
        [Header("BiomeType")]
        [SerializeField] private Toggle _forestTypeToggle;
        [SerializeField] private Toggle _sandTypeToggle;
        [SerializeField] private Toggle _shallowSeaTypeToggle;
        [SerializeField] private Toggle _seaTypeToggle;
        [SerializeField] private Toggle _snowTypeToggle;
        
        private ToggleGroup _toggleGroup;
        private HexEditorViewModel _hexEditorViewModel;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Init() { throw new System.NotImplementedException(); }
        public void Init(HexEditorViewModel hexEditorViewModel)
        {
            _toggleGroup = GetComponent<ToggleGroup>();
            _hexEditorViewModel = hexEditorViewModel;
            _hexEditorViewModel.GetBiomeType().Skip(1).Subscribe(UpdateBiomeType).AddTo(_disposable);
        }

        private void UpdateBiomeType(HexBiomeType hexBiomeType)
        {
            _toggleGroup.allowSwitchOff = true;
            switch (hexBiomeType)
            {
                case HexBiomeType.Forest:
                    ActivateToggle(_forestTypeToggle);
                    break;
                case HexBiomeType.Sand:
                    ActivateToggle(_sandTypeToggle);
                    break;
                case HexBiomeType.ShallowSea:
                    ActivateToggle(_shallowSeaTypeToggle);
                    break;
                case HexBiomeType.Sea:
                    ActivateToggle(_seaTypeToggle);
                    break;
                case HexBiomeType.Snow:
                    ActivateToggle(_snowTypeToggle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hexBiomeType), hexBiomeType, null);
            }
        }

        private void ActivateToggle(Toggle toggle)
        {
            toggle.isOn = true;
        }
    }
}