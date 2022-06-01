using System;
using System.Collections.Generic;
using Model.Fabrics;
using Model.HexModel;
using Other.Composite;
using UnityEngine;
using UnityEngine.Serialization;
using View.HexGridV;
using ViewModel.HexGridVM;
using Zenject;

namespace Other.Infostructure
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private List<CompositeRoot> _compositeRoots;

        public override void InstallBindings()
        {
            foreach (var root in _compositeRoots)
            {
                root.InitBindings(Container);
            }
        }

        private void Awake()
        {
            
        }
    }
}