using Common.Gateway.Bonus;
using Common.Gateway.DamageBall;
using Common.Gateway.Gate;
using Gateway;
using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public class BallUsecase: IBallUsecase
    {
        public IReadOnlyReactiveProperty<BallModel> Score => _score;
        private readonly ReactiveProperty<BallModel> _score;
        
        public IReadOnlyReactiveProperty<BallModel> Combo => _combo;
        private readonly ReactiveProperty<BallModel> _combo;
        
        public IReadOnlyReactiveProperty<BallModel> Strength => _strength;
        private readonly ReactiveProperty<BallModel> _strength;

        private readonly IBallGateway _ballGateway;
        private readonly IBossGateway _bossGateway;
        private readonly IBumperGateway _bumperGateway;
        private readonly IBonusWallGateway _bonusWallGateway;
        private readonly IDamageBallGateway _damageBallGateway;
        private readonly IGateGateway _gateGateway;
        private readonly IBonusGateway _bonusGateway;

        public BallUsecase(IBallGateway ballGateway, IBossGateway bossGateway,
            IBumperGateway bumperGateway, IBonusWallGateway bonusWallGateway,
            IDamageBallGateway damageBallGateway, IGateGateway gateGateway,
            IBonusGateway bonusGateway)
        {
            _ballGateway = ballGateway;
            _bossGateway = bossGateway;
            _bumperGateway = bumperGateway;
            _bonusWallGateway = bonusWallGateway;
            _damageBallGateway = damageBallGateway;
            _gateGateway = gateGateway;
            _bonusGateway = bonusGateway;
            _score = new ReactiveProperty<BallModel>(new BallModel());
            _combo = new ReactiveProperty<BallModel>(new BallModel());
            _strength = new ReactiveProperty<BallModel>(new BallModel());
            InitScore();
        }

        public void SetScoreViaWall(BonusWallType bonusWallType)
        {
            var score = _ballGateway.GetBallValue();

            switch (bonusWallType)
            {
                case BonusWallType.Addition:
                    score += _bonusWallGateway.GetBonusWallValue(BonusWallType.Addition);
                    break;
                case BonusWallType.Subtraction:
                    score -= _bonusWallGateway.GetBonusWallValue(BonusWallType.Subtraction);
                    break;
                case BonusWallType.Division:
                    score /= _bonusWallGateway.GetBonusWallValue(BonusWallType.Division);
                    break;
                case BonusWallType.Multiplication:
                    score *= _bonusWallGateway.GetBonusWallValue(BonusWallType.Multiplication);
                    break;
            }
            
            var newValue = score;
            
            _ballGateway.SetBallValue(newValue);

            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
        }

        public void SetComboValue()
        {
            var comboValue = _ballGateway.GetComboValue();
            var newValue = comboValue + 1;
            _ballGateway.SetComboValue(newValue);
            
            var ballModel = _combo.Value;
            ballModel.Combo = newValue;
            _combo.SetValueAndForceNotify(ballModel);
        }

        public void SetComboToBaseValue()
        {
            const int comboValue = 1;
            _ballGateway.SetComboValue(comboValue);
            var ballModel = _combo.Value;
            ballModel.Combo = comboValue;
            _combo.SetValueAndForceNotify(ballModel);
        }

        public void SetValueViaDamageBall()
        {
            var ballValue = _ballGateway.GetBallValue();
            var newValue = ballValue - _damageBallGateway.GetDamageValue();
            _ballGateway.SetBallValue(newValue);
            
            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
        }
        
        public void SetScoreViaBumper(BumperType bumperType, int value)
        {
            value = _ballGateway.GetBallValue();
            var comboValue = _ballGateway.GetComboValue();
            
            switch (bumperType)
            {
                case BumperType.Five:
                    value += _bumperGateway.GetBumperValue(BumperType.Five) * comboValue;
                    break;
                case BumperType.Ten:
                    value += _bumperGateway.GetBumperValue(BumperType.Ten) * comboValue;
                    break;
                case BumperType.Twenty:
                    value += _bumperGateway.GetBumperValue(BumperType.Twenty) * comboValue;
                    break;
                case BumperType.MinusFive:
                    value -= _bumperGateway.GetBumperValue(BumperType.Five);
                    break;
                case BumperType.MinusTen:
                    value -= _bumperGateway.GetBumperValue(BumperType.Ten);
                    break;
                case BumperType.MinusTwenty:
                    value -= _bumperGateway.GetBumperValue(BumperType.Twenty);
                    break;
            }
            
            var newValue = value;
            
            _ballGateway.SetBallValue(newValue);

            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
        }

        public void SetValue(int value)
        {
            _ballGateway.SetBallValue(value);
            var ballModel = _score.Value;
            value = _ballGateway.GetBallValue();
            ballModel.Score = value;
            _score.SetValueAndForceNotify(ballModel);
        }

        public void SetStrength(float value)
        {
            _ballGateway.SetStrengthValue(value);
            var ballModel = _strength.Value;
            value = _ballGateway.GetBallStrength();
            ballModel.DamageStrength = value;
            _strength.SetValueAndForceNotify(ballModel);
        }

        public void HitBoss()
        {
            var score = (int)(_ballGateway.GetBallValue() * _ballGateway.GetBallStrength());
            // var bossGetDamageValue = score + (_bossGateway.GetBossHealth() - score);

            if (score > _bossGateway.GetBossHealth())
            {
                score -= _bossGateway.GetBossHealth();
                _bossGateway.SetBossHealth(0);
            }
            else
            {
                _bossGateway.SetBossHealth(_bossGateway.GetBossHealth() - score);
                score = 0;
            }
            
            var newValue = score;

            _ballGateway.SetBallValue(newValue);
            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
        }

        public void SetValueViaBonusCost(int cost)
        {
            var score = _ballGateway.GetBallValue();
            score -= cost;
            _ballGateway.SetBallValue(score);
            var ballModel = _score.Value;
            ballModel.Score = score;
            _score.SetValueAndForceNotify(ballModel);
        }
        
        public void SetValueViaGate()
        {
            var score = _ballGateway.GetBallValue();
            var gateHealth = score + (_gateGateway.GetGateHealth() - score);

            if (score > _gateGateway.GetGateHealth())
            {
                _gateGateway.SetGateHealth(0);
                score -= gateHealth;
            }
            else
            {
                _gateGateway.SetGateHealth(_gateGateway.GetGateHealth() - score);
                score = 0;
            }
            
            var newValue = score;

            _ballGateway.SetBallValue(newValue);
            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
        }
        
        private void InitScore()
        {
            var count = new BallModel()
            {
                Score = _ballGateway.GetBallValue(),
                
            };

            var combo = new BallModel()
            {
                Combo = _ballGateway.GetComboValue()
            };
            
            var strength = new BallModel()
            {
                DamageStrength = _ballGateway.GetBallStrength() 
            };
            _score.Value = count;
            _combo.Value = combo;
            _strength.Value = strength;

        }
    }
}