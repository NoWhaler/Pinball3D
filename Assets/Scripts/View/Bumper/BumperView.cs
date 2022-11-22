using Pinball.Presenter;
using TMPro;
using UnityEngine;
using Zenject;

namespace View
{
    public class BumperView : MonoBehaviour, IBumperView
    {
        
        // [SerializeField] private GameObject _popUpTextPrefab;
        // [SerializeField] private Vector3 _popUpTextOffset;

        

        private IBumperPresenter _bumperPresenter;
        
        private TMP_Text _bumperPoints;
        // private TMP_Text _popUpPoints;
        private Canvas _canvas;

        [Inject]
        private void Constructor(IBumperPresenter bumperPresenter)
        {
            _bumperPresenter = bumperPresenter;
        }
        
        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _bumperPoints = _canvas.GetComponentInChildren<TMP_Text>();
            // _popUpPoints = GetComponentInChildren<TMP_Text>();

        }

        private void Start()
        {
            _bumperPresenter.ChangeBumperPoints();
        }

        public void SetPoints(int points)
        {
            // _popUpPoints.text = points.ToString();
            _bumperPoints.text = points.ToString();
        }
    }
}