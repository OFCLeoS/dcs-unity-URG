public abstract class StatusEffect
{
    protected DamageableEntity target;

    public StatusEffect(DamageableEntity target)
    {
        this.target = target;
    }

    public abstract void Apply();

    public abstract void Remove();
}