using UnityEngine;

public class ScriptableObjectSelectorAttribute : PropertyAttribute
{
    public System.Type type;

    public ScriptableObjectSelectorAttribute(System.Type type)
    {
        this.type = type;
    }
}
