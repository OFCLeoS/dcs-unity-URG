[System.Serializable]
public class BlackboardVariableNotFoundException : System.Exception
{
    public BlackboardVariableNotFoundException() { }
    public BlackboardVariableNotFoundException(string variableName) : base("The \"" + variableName + "\" Blackboard variable was expected but not found.") { }
    public BlackboardVariableNotFoundException(string variableName, System.Exception inner) : base("The \"" + variableName + "\" Blackboard variable was expected but not found.", inner) { }
    protected BlackboardVariableNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}