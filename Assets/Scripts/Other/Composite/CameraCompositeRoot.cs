using System;
using CreatingMapMode.Model.CameraM;
using DefaultNamespace;
using Model;
using UniRx;
using UnityEngine;
using View.CameraPlayer;
using ViewModel.Camera;
using Zenject;

namespace Other.Composite
{
    public class CameraCompositeRoot : CompositeRoot
    {
        [SerializeField] private CameraView _cameraView;

        private CameraViewModel _cameraViewModel;

        private CameraPlayer _cameraPlayer;
        private CameraZoom _cameraZoom;
        private CameraMovement _cameraMovement;
        private CameraRotation _cameraRotation;

        private InputPlayer _inputPlayer;
        private CompositeDisposable _disposable = new CompositeDisposable();

        [Inject]
        public void Construct(InputPlayer inputPlayer)
        {
            _inputPlayer = inputPlayer;
        }
        
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _cameraPlayer = new CameraPlayer();
            _cameraZoom = new CameraZoom(_cameraPlayer, _inputPlayer);
            _cameraMovement = new CameraMovement(_cameraPlayer, _inputPlayer, null);
            _cameraRotation = new CameraRotation(_cameraPlayer, _inputPlayer);
            if (_cameraMovement is IUpdateable updateableMovement)
                Observable.EveryUpdate().Subscribe(x =>
                {
                    updateableMovement.Update(Time.deltaTime);
                }).AddTo(_disposable);
            if (_cameraRotation is IUpdateable updateableRotation)
                Observable.EveryUpdate().Subscribe(x =>
                {
                    updateableRotation.Update(Time.deltaTime);
                }).AddTo(_disposable);

            
            _cameraViewModel = new CameraViewModel(_cameraPlayer);

            _cameraView.Init(_cameraViewModel);
        }
    }
}