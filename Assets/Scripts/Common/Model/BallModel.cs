using UnityEngine;

namespace Model
{
    public class BallModel
    {
        private int _maxScore;
        public int Score { get => _maxScore; set => _maxScore = Mathf.Clamp(value, 0, 100000); }
        public int Combo { get; set; } = 1;

        public float DamageStrength { get; set; }
    }
}