using System;
using System.Collections.Generic;
using Model;
using Model.Enums;
using UnityEngine;
using Usecase;
using UniRx;

namespace Pinball.Presenter
{
    public class BumperPresenter : MonoBehaviour, IBumperPresenter
    {
        public IReadOnlyReactiveProperty<int> BumperFive => _five;
        private readonly ReactiveProperty<int> _five = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BumperTen => _ten;
        private readonly ReactiveProperty<int> _ten = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BumperTwenty => _twenty;
        private readonly ReactiveProperty<int> _twenty = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BumperMinusFive => _minusFive;
        private readonly ReactiveProperty<int> _minusFive = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BumperMinusTen => _minusTen;
        private readonly ReactiveProperty<int> _minusTen = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BumperMinusTwenty => _minusTwenty;
        private readonly ReactiveProperty<int> _minusTwenty = new ReactiveProperty<int>();

        private IBumperUsecase _bumperUsecase;
        
        public void Initialize(IBumperUsecase usecase)
        {
            _bumperUsecase = usecase;
            UpdateCount(_bumperUsecase.Points.Value);
        }

        private void UpdateCount(Dictionary<BumperType, BumperModel> dict)
        {
            foreach (var (key, value) in dict)
            {
                switch (key)
                {
                    case BumperType.Five:
                        _five.Value = value.Points;
                        break;
                    case BumperType.Ten:
                        _ten.Value = value.Points;
                        break;
                    case BumperType.Twenty:
                        _twenty.Value = value.Points;
                        break;
                    case BumperType.MinusFive:
                        _minusFive.Value = value.Points;
                        break;
                    case BumperType.MinusTen:
                        _minusTen.Value = value.Points;
                        break;
                    case BumperType.MinusTwenty:
                        _minusTwenty.Value = value.Points;
                        break;
                }
            }
        }

        public void SetBumperPoints(BumperType bumperType)
        {
            _bumperUsecase.SetPoints(bumperType);
        }
    }
}