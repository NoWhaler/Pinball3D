namespace Gateway
{
    public interface IBallGateway
    {
        void SetBallValue(int value);
        int GetBallValue();

        void SetComboValue(int comboValue);
        int GetComboValue();

        void SetStrengthValue(float strength);

        float GetBallStrength();

    }
}