/// <summary>
/// Interface used for all interactables, every scripted object that implements this interface can be interacted with by anything
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// What will occur when the object is interacted with by the player
    /// </summary>
    public void OnInteract(Player player);
}
