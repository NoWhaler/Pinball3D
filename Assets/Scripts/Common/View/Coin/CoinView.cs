using UI;
using UnityEngine;

namespace View.Coin
{
    public class CoinView : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var ballView = other.GetComponent<BallView>();
            if (ballView != null)
            {
                CoinsHolder.CoinsAmount += Random.Range(1, 6);
                gameObject.SetActive(false);
            }
        }
    }
}