using UnityEngine;

namespace Model
{
    public class BossModel
    {
        private int _maxHealth;
        public int HealthPoints { get => _maxHealth; set => _maxHealth = Mathf.Clamp(value, 0, 50000); }
    }
}