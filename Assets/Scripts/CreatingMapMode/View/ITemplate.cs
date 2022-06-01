using UnityEngine;

namespace DefaultNamespace
{
    public interface ITemplate
    {
        void Place(Vector3 position);
        void Delete();

        void Transparent();
        void Opaque();
    }
}