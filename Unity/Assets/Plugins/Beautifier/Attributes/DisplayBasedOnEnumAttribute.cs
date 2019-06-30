using UnityEngine;

public class DisplayBasedOnEnumAttribute : PropertyAttribute
{
    public string enumName;
    public int enumIndex;
    public bool show;

    public DisplayBasedOnEnumAttribute(string boolName,int enumIndex, bool show = true)
    {
        this.enumName = boolName;
        this.show = show;
        this.enumIndex = enumIndex;
    }
}
