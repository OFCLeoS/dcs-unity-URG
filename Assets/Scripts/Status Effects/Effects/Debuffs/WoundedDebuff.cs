public class WoundedDebuff : DebuffStatusEffect
{
    readonly float _healthPenalty;
    public WoundedDebuff(DamageableEntity target, float healthPenalty) : base(target)
    {
        _healthPenalty = healthPenalty;
        Apply();
    }

    public override void Apply()
    {

    }

    public override void Remove()
    {

    }
}