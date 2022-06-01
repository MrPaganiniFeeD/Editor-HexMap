using System;
using DefaultNamespace;
using Model;
using Model.Rotation;

namespace CreatingMapMode.Model.CameraM
{
    public class CameraRotation : IModel, IUpdateable, IDisposable
    {
        private readonly CameraPlayer _camera;
        private readonly InputPlayer _input;
        private float _rotationSpeed = 180;
        private float _rotationAngel;
        private float _rotationDelta;

        public CameraRotation(CameraPlayer camera, InputPlayer input)
        {
            _camera = camera;
            _input = input;
            _input.ChangeRotation += OnChangeRotation;
        }

        private void OnChangeRotation(float value) => 
            _rotationDelta = value;

        public void Update(float delta) => 
            Rotate(delta);

        private void Rotate(float deltaTime)
        {
            _rotationAngel += deltaTime * _rotationSpeed * _rotationDelta;
            if (_rotationAngel < 0f) 
                _rotationAngel += 360f;
            else if (_rotationAngel >= 360f)
                _rotationAngel -= 360f;
        
            _camera.Rotation = new RotationLocal(0f, _rotationAngel, 0f);

            switch (_rotationAngel > 1)
            {
            
            }
        }

        public void Dispose() => 
            _input.ChangeRotation -= OnChangeRotation;
    }
}
