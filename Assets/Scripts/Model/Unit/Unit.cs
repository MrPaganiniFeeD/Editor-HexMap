using System;
using Model.HexModel;
using Model.Rotation;
using Model.Transformable;
using UnityEngine;

namespace Model.Unit
{
    public abstract class Unit : IModel, ITransformable<Vector3Int>
    {
        public event Action<Vector3Int> ChangePosition;
        public event Action<RotationLocal> ChangeRotation;
        
        public Vector3Int Position { get; set; }
        public RotationLocal Rotation { get; set; }

        public abstract void Move(Hex targetHex);

        public abstract void Attack(IDamageable damageable);

    }
}
