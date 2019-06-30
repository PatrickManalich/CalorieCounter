using UnityEngine;


public class ColorizeAttribute : PropertyAttribute
{
    private int r, g, b;

    public Color GetColor()
    {
        return new Color(r, g, b);
    }

    public ColorizeAttribute(int r, int g, int b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}
