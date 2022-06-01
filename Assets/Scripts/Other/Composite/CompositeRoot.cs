using UnityEngine;
using Zenject;

namespace Other.Composite
{
    public class CompositeRoot : MonoBehaviour
    {
        protected DiContainer DiContainer;
        
        public virtual void InitBindings(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }
    }
}