using System;
using Model;

namespace DefaultNamespace
{
    public class InputPlayer : IModel, IUpdateable, IFixedUpdateable
    {
        public event Action EndClick;
        public event Action ChangeClick;
        public event Action<float> ChangeRotation;
        public event Action<float> ChangeScrollWheel;
        public event Action<float, float> ChangeMoveDirection;

        public float DeltaTime { get; private set; }
        public float FixedDeltaTime { get; private set; }

        private float _sensitivityScrollWheel = 0.001f;
        
        public void SetScrollWheel(float value)
        {
            var delta = value * _sensitivityScrollWheel;
            ChangeScrollWheel?.Invoke(delta);
        }
        public void SetMoveDirection(float deltaX, float deltaY)
        {
            ChangeMoveDirection?.Invoke(deltaX, deltaY);
        }

        public void SetRotation(float delta)
        {
            ChangeRotation?.Invoke(delta);
        }
        public void Update(float delta)
        {
            DeltaTime = delta;
        }

        public void InvokeClick()
        {
            ChangeClick?.Invoke();
        }
        public void InvokeEndClick()
        {
            EndClick?.Invoke();
        }
        public void FixedUpdate(float delta)
        {
            FixedDeltaTime = delta;
        }
    }
}