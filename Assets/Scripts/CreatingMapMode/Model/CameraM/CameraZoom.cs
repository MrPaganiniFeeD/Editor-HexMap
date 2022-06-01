using System;
using System.Collections.Generic;
using DefaultNamespace;
using Model;
using Model.Rotation;
using UnityEngine;

namespace CreatingMapMode.Model.CameraM
{
    public class CameraZoom : IModel, IDisposable
    {
        private float _zoom = 1f;
        private float _minZoom = -60;
        private float _maxZoom = -10;
        private float _minAngelRotate = 90;
        private float _maxAngelRotate = 45;
        
        private CameraPlayer _cameraPlayer;
        private InputPlayer _inputPlayer;
        
        
        public CameraZoom(CameraPlayer cameraPlayer, InputPlayer inputPlayer)
        {
            _cameraPlayer = cameraPlayer;
            _inputPlayer = inputPlayer;
            _inputPlayer.ChangeScrollWheel += AdjustZoom;
        }
        private void AdjustZoom (float delta)
        {
            _zoom = Mathf.Clamp01(_zoom + delta);

            var distance = Mathf.Lerp(_minZoom, _maxZoom, _zoom);
            _cameraPlayer.StickPosition = new Vector3(0f, 0f, distance);
            
            var angle = Mathf.Lerp(_minAngelRotate, _maxAngelRotate, _zoom);
            _cameraPlayer.SwivelRotation = new RotationLocal(angle, 0f, 0f);
        }
        public void Dispose()
        {
            _inputPlayer.ChangeScrollWheel -= AdjustZoom;
        }
    }
}