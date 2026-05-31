/// <summary>
/// Interface for objects that can be damaged
/// </summary>
public interface IDamageable {
    public void TakeDamage(float damageAmount, Team attackingTeam);
    bool IsDestroyed { get; }
}