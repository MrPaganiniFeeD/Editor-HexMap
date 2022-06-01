using System;
using UniRx;
using UnityEngine;
using ViewModel.Camera;

namespace View.CameraPlayer
{
    public class CameraView : MonoBehaviour, IView
    {
        [SerializeField] private Transform _stick;
        [SerializeField] private Transform _swivel;
        [SerializeField] private Transform _hexGridCamera;

        private CameraViewModel _cameraViewModel;

        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public void Init() { throw new System.NotImplementedException(); }
        public void Init(CameraViewModel cameraViewModel)
        {
            _cameraViewModel = cameraViewModel;
            _cameraViewModel.GetPosition().Skip(1).Subscribe(UpdatePosition).AddTo(_disposable);
            _cameraViewModel.GetRotation().Skip(1).Subscribe(UpdateRotation).AddTo(_disposable);
            _cameraViewModel.GetStickPosition().Skip(1).Subscribe(UpdateStickPosition).AddTo(_disposable);
            _cameraViewModel.GetSwivelRotation().Skip(1).Subscribe(UpdateSwivelRotation).AddTo(_disposable);
        }
        public void Update()
        {
            UpdateRightDirection();
            UpdateForwardDirection();
        }
        private void UpdateRightDirection()
        {
            _cameraViewModel.SetRightDirection(_hexGridCamera.right);
        }
        private void UpdateForwardDirection()
        {
            _cameraViewModel.SetForwardDirection(_hexGridCamera.forward);
        } 
        private void UpdatePosition(Vector3 position)
        {
            _hexGridCamera.localPosition = position;
        }
        private void UpdateStickPosition(Vector3 position)
        {
            _stick.localPosition = new Vector3(0, 0, position.z);
        }
        private void UpdateSwivelRotation(Quaternion rotation)
        {
            _swivel.localRotation = rotation;
        }
        private void UpdateRotation(Quaternion rotation)
        {
            _hexGridCamera.localRotation = rotation;
        }
    }
}