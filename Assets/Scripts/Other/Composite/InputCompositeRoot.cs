using System;
using DefaultNamespace;
using Model;
using UniRx;
using UnityEngine;
using View.Input;
using ViewModel.Input;
using Zenject;

namespace Other.Composite
{
    public class InputCompositeRoot : CompositeRoot
    {
        [SerializeField] private InputView _inputView;

        private InputViewModel _inputViewModel;
        private InputPlayer _inputPlayer;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public override void InitBindings(DiContainer diContainer)
        {
            base.InitBindings(diContainer);
            
            BindInputPlayer();
            BindInputViewModel();
            
            _inputView.Init(_inputViewModel);
        }

        public void BindInputPlayer()
        {
            _inputPlayer = new InputPlayer();
            
            if (_inputPlayer is IUpdateable updateable)
                Observable.EveryUpdate().Subscribe(x =>
                {
                    updateable.Update(Time.deltaTime);
                }).AddTo(_disposable);
            
            if (_inputPlayer is IFixedUpdateable fixedUpdateable)
                Observable.EveryUpdate().Subscribe(x =>
                {
                    fixedUpdateable.FixedUpdate(Time.fixedDeltaTime);
                }).AddTo(_disposable);
            
            DiContainer.Bind<InputPlayer>().FromInstance(_inputPlayer).AsSingle();
        }
        public void BindInputViewModel()
        {
            _inputViewModel = new InputViewModel(_inputPlayer);
            DiContainer.Bind<InputViewModel>().FromInstance(_inputViewModel).AsSingle();
        }
    }
}