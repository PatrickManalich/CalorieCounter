using UnityEngine;


public class PopUpAttribute : PropertyAttribute
{
    public string[] items;

    public PopUpAttribute(params string[] args)
    {
        items = args;
    }
}