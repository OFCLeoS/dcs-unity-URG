/// <summary>
/// AI Agent Attributes, mainly to be used with wave difficulty modifier
/// </summary>
[System.Serializable]
public struct AIAgentAttributes
{
    public float minAgentHealth;
    public float maxAgentHealth;

    public float minMovementSpeed;
    public float maxMovementSpeed;

    public float minDamage;
    public float maxDamage;
}