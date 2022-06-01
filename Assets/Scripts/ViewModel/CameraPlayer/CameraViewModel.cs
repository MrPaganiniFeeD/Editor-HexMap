using System;
using CreatingMapMode.Model.CameraM;
using Model.Rotation;
using UniRx;
using UnityEngine;

namespace ViewModel.Camera
{
    public class CameraViewModel : IViewModel, IDisposable
    {
        private ReactiveProperty<Vector3> _changePosition = new ReactiveProperty<Vector3>();
        private ReactiveProperty<Vector3> _stickChangePosition = new ReactiveProperty<Vector3>();
        private ReactiveProperty<Quaternion> _changeSwivelRotation = new ReactiveProperty<Quaternion>();
        private ReactiveProperty<Quaternion> _changeRotation = new ReactiveProperty<Quaternion>();
        
        private CameraPlayer _cameraPlayer;

        public CameraViewModel(CameraPlayer cameraPlayer)
        {
            _cameraPlayer = cameraPlayer;
            _cameraPlayer.ChangeStickPosition += OnChangeStickPosition;
            _cameraPlayer.ChangePosition += OnChangePosition;
            _cameraPlayer.ChangeRotation += OnChangePosition;
            _cameraPlayer.ChangeSwivelRotation += OnChangeSwivelRotation;
        }

        private void OnChangeSwivelRotation(RotationLocal rotation)
        {
            Quaternion quaternion = Quaternion.Euler(rotation.X, rotation.Y, rotation.Z);
            _changeSwivelRotation.Value = quaternion;
        }

        private void OnChangeStickPosition(Vector3 position) => 
            _stickChangePosition.Value = position;

        private void OnChangePosition(Vector3 position) => 
            _changePosition.Value = position;

        private void OnChangePosition(RotationLocal rotation)
        {
            Quaternion quaternion = Quaternion.Euler(rotation.X, rotation.Y, rotation.Z);
            _changeRotation.Value = quaternion;
        }
        
        public ReactiveProperty<Vector3> GetPosition() => 
            _changePosition;

        public ReactiveProperty<Quaternion> GetSwivelRotation() => 
            _changeSwivelRotation;

        public ReactiveProperty<Quaternion> GetRotation() => 
            _changeRotation;

        public ReactiveProperty<Vector3> GetStickPosition() => 
            _stickChangePosition;

        public void SetForwardDirection(Vector3 direction) => 
            _cameraPlayer.SetForwardDirection(direction);

        public void SetRightDirection(Vector3 direction) => 
            _cameraPlayer.SetRightDirection(direction);

        public void Dispose()
        {
            _cameraPlayer.ChangeStickPosition -= OnChangeStickPosition;
            _cameraPlayer.ChangePosition -= OnChangePosition;
            _cameraPlayer.ChangeRotation -= OnChangePosition;
            _cameraPlayer.ChangeSwivelRotation -= OnChangeSwivelRotation;
            _changePosition?.Dispose();
            _changeRotation?.Dispose();
        }
    }
}