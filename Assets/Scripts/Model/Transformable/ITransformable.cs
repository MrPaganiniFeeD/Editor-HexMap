using System;
using Model.Rotation;
using UnityEngine;

namespace Model.Transformable
{
    public interface ITransformable<T>
    {
        event Action<T> ChangePosition;
        event Action<RotationLocal> ChangeRotation;

        T Position { get; set; }
        RotationLocal Rotation { get; set; }
        
    }
}