using UnityEngine;


public class ReplaceCharacterAttribute : PropertyAttribute
{

    public char oldChar;
    public char newChar;

    public ReplaceCharacterAttribute(char character, char replacement)
    {
        oldChar = character;
        newChar = replacement;
    }
}
