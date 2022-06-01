using System;
using DefaultNamespace;
using Model;
using Model.HexModel;
using Model.Rotation;
using UnityEngine;

namespace CreatingMapMode.Model.CameraM
{
    public class CameraMovement : IModel, IDisposable, IUpdateable
    {
        private readonly CameraPlayer _cameraPlayer;
        private readonly InputPlayer _input;
        private readonly HexGrid _hexGrid;
        private float _deltaX;
        private float _deltaY;
        private float _speed = 10;

        public CameraMovement(CameraPlayer cameraPlayer, InputPlayer input, HexGrid hexGrid)
        {
            _cameraPlayer = cameraPlayer;
            _input = input;
            _hexGrid = hexGrid;
            _input.ChangeMoveDirection += OnChangeMoveDirection;
        }

        private void OnChangeMoveDirection(float deltaX, float deltaY)
        {
            _deltaX = deltaX;
            _deltaY = deltaY;
        }

        private void Move(float deltaTime)
        {
            var direction = new Vector3(_deltaX, 0f, _deltaY).normalized;
            var nextPosition = 
                (_cameraPlayer.ForwardDirection * direction.z + _cameraPlayer.RightDirection * direction.x) * _speed;
            var damping = Mathf.Max(Mathf.Abs(_deltaX), Mathf.Abs(_deltaY));
            var distance = _speed * damping * deltaTime;

            var position = _cameraPlayer.Position;
            position += nextPosition * distance;
            _cameraPlayer.Position = position;
        }

        public void Update(float delta)
        {
            Move(delta);
        }

        public void Dispose()
        {
            _input.ChangeMoveDirection -= OnChangeMoveDirection;
        }
    }
}