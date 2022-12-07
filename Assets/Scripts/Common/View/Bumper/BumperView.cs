using System;
using Interfaces;
using Model.Enums;
using Pinball.Presenter;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace View
{
    public class BumperView : MonoBehaviour, IBumperView, ISetable
    {
        [Inject]
        private IBumperPresenter _bumperPresenter;

        [SerializeField] private BumperType _bumperType;
        
        public BumperType BumperType
        {
            get => _bumperType;
        }
        
        private TMP_Text _bumperPoints;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _bumperPoints = _canvas.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _bumperPresenter.SetBumperPoints(_bumperType);
            
            var reactiveProperty = _bumperType switch
            {
                BumperType.Five => _bumperPresenter.BumperFive,
                BumperType.Ten => _bumperPresenter.BumperTen,
                BumperType.MinusFive => _bumperPresenter.BumperMinusFive,
                BumperType.MinusTen => _bumperPresenter.BumperMinusTen,
                BumperType.MinusTwenty => _bumperPresenter.BumperMinusTwenty,
                _ => _bumperPresenter.BumperTwenty
            };
            
            reactiveProperty.Subscribe((points) => { SetValue(points); }).AddTo(this);
        }
        
        public void SetValue(int points)
        {
            _bumperPoints.text = points.ToString();
        }
    }
}