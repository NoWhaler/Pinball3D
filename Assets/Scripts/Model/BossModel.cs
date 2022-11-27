using UnityEngine;

namespace Model
{
    public class BossModel
    {
        private int _maxHealth = 600;
        public int HealthPoints { get => _maxHealth; set => _maxHealth = Mathf.Clamp(value, 0, 600); }
    }
}