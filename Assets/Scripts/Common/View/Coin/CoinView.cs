using UI;
using UnityEngine;
using Zenject;

namespace View.Coin
{
    public class CoinView : MonoBehaviour, ICoinView
    {
        private CoinsHolder _coinsHolder;

        [Inject]
        private void Constructor(CoinsHolder coinsHolder)
        {
            _coinsHolder = coinsHolder;
        }

        private void OnTriggerEnter(Collider other)
        {
            var ballView = other.GetComponent<BallView>();
            if (ballView != null)
            {
                _coinsHolder.CoinsAmount += Random.Range(1, 6);
                gameObject.SetActive(false);
            }
        }
    }
}