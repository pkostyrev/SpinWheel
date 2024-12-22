public interface IModel
{
    int[] Rewards { get; }
    RewardType RewardType { get; }

    RewardData SelectNewReward();
    void GenerateNewRewards();
}
