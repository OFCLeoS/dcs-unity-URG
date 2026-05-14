public abstract class RecurrentStatusEffect : StatusEffect
{
    readonly float _applyTime;

    float timeLeftToApply;

    protected RecurrentStatusEffect(DamageableEntity target, float applyRate) : base(target)
    {
        _applyTime = applyRate;
        timeLeftToApply = _applyTime;
    }

    public void Tick(float deltaTime)
    {
        timeLeftToApply -= deltaTime;
        if(timeLeftToApply <= 0)
        {
            Apply();
            timeLeftToApply = _applyTime;
        }
    }
}