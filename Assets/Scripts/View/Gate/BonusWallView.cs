using Model.Enums;
using Presenter;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace View
{
    public class BonusWallView : MonoBehaviour
    {
        [SerializeField] private BonusWallType _bonusWallType;
        [SerializeField] private int _value;

        [Inject]
        private IBonusWallPresenter _bonusWallPresenter;
        
        private TMP_Text _valuePoints;
        private Canvas _canvas;

        public BonusWallType BonusWallType { get => _bonusWallType; }
        
        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _valuePoints = _canvas.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
           _bonusWallPresenter.SetValueToWall(_bonusWallType);
            
            var reactiveProperty = _bonusWallType switch
            {
                BonusWallType.Addition => _bonusWallPresenter.BonusWallAddition,
                BonusWallType.Subtraction => _bonusWallPresenter.BonusWallSubtraction,
                BonusWallType.Division => _bonusWallPresenter.BonusWallDivision,
                _ => _bonusWallPresenter.BonusWallMultiplication
            };
            
            reactiveProperty.Subscribe((points) => { SetPoints(points); }).AddTo(this);
            // SetPoints(_value);
        }
        
        private void SetPoints(int points)
        {
            // _value = points;
            _valuePoints.text = _bonusWallType switch
            {
                BonusWallType.Addition => $"+{points}",
                BonusWallType.Subtraction => $"-{points}",
                BonusWallType.Division => $"%{points}",
                BonusWallType.Multiplication => $"*{points}",
                _ => ""
            };
        }
    }
}