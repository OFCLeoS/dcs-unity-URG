public class WoundedDebuff : DebuffStatusEffect
{
    readonly float _healthPenalty;
    public WoundedDebuff(DamageableEntity target, float healthPenalty) : base(target)
    {
        _healthPenalty = healthPenalty;
    }

    public override void ApplyEffect()
    {
        target.SubtractMaxHealth(_healthPenalty);
    }

    /// <summary>
    /// Must be called upon removal!
    /// </summary>
    public override void RemoveEffect()
    {
        target.AddTakeMaxHealth(_healthPenalty);
    }
}