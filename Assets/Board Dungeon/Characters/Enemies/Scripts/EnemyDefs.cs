public enum BehaviourType
{
    RunningAway,
    Dodging,
    UsingAbilities,
    UsingAbilitiesWhileMoving,
    FollowingPlayer,
    Patroling,
}
public enum AbilityConditionType
{
    DistanceCondition,
    AngleCondition,
    PlayerSpeedCondition,

}
public enum PlayerWantedState
{
    PlayerNotWanted,
    PlayerWanted,
}
public enum PlayerVisibility
{
    PlayeIsVisible,
    PlayerIsNotVisible,
}