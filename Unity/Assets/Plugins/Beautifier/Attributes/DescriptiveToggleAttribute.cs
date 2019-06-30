using UnityEngine;

public class DescriptiveToggleAttribute : PropertyAttribute
{
    public string onString, offString;

    public DescriptiveToggleAttribute(string on,string off)
    {
        this.onString = on;
        this.offString = off;
    }
    
    public string GetString(bool isOn)
    {
        return (isOn) ? onString : offString;
    }

    public string[] GetStrings()
    {
        return new string[] { offString, onString };
    }
}
