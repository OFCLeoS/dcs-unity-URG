public abstract class StatusEffect
{
    protected DamageableEntity target;

    public StatusEffect(DamageableEntity target)
    {
        this.target = target;
    }

    public abstract void ApplyEffect();

    public abstract void RemoveEffect();
}