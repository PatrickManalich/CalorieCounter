using UnityEngine;

public class DisplayBasedOnBoolAttribute : PropertyAttribute
{
    public string boolName;
    public bool show;

    public DisplayBasedOnBoolAttribute(string boolName, bool show = true)
    {
        this.boolName = boolName;
        this.show = show;
    }
}
