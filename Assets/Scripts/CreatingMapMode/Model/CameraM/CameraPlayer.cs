using System;
using Model;
using Model.Rotation;
using Model.Transformable;
using UnityEngine;

namespace CreatingMapMode.Model.CameraM
{
    public class CameraPlayer : IModel, ITransformable<Vector3>
    {
        public event Action<Vector3> ChangePosition;
        public event Action<Vector3> ChangeStickPosition;
        public event Action<RotationLocal> ChangeRotation;
        public event Action<RotationLocal> ChangeSwivelRotation;

        public Vector3 ForwardDirection
        {
            get => _forwardDirection;
            set => _forwardDirection = value;
        }

        public Vector3 RightDirection
        {
            get => _rightDirection;
            set => _rightDirection = value;
        }
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                ChangePosition?.Invoke(_position);
            }
        }
        public Vector3 StickPosition
        {
            get => _stickPosition;
            set
            {
                _stickPosition = value;
                ChangeStickPosition?.Invoke(_stickPosition);
            }
        }
        public RotationLocal Rotation { get => _rotation;
            set
            {
                _rotation = value;
                ChangeRotation?.Invoke(_rotation);
            }
        }
        public RotationLocal SwivelRotation
        {
            get => _swivelRotation;
            set
            {
                _swivelRotation = value;
                ChangeSwivelRotation?.Invoke(_swivelRotation);
            }
        }

        private Vector3 _forwardDirection;
        private Vector3 _rightDirection;
        private Vector3 _position;
        private Vector3 _stickPosition;
        private RotationLocal _rotation;
        private RotationLocal _swivelRotation;

        public void SetForwardDirection(Vector3 direction)
        {
            _forwardDirection = direction;
        }
        public void SetRightDirection(Vector3 direction)
        {
            _rightDirection = direction;
        }
    }
}