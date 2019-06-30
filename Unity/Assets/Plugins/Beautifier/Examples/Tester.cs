using UnityEngine;

public class Tester : MonoBehaviour
{
    public enum Mode { Simple,Complex}
    public Mode mode;

    [DisplayBasedOnEnum("mode",(int)Mode.Complex,true)]
    public string enumBased;

    public bool showOrNot;

    [DisplayBasedOnBool("showOrNot",false)]
    public string toShowOrNotToShow;


    [UpperCaseString]
    public string upperCaseString;

    [LowerCaseString]
    public string lowerCaseStrin;

    [Password]
    public string passwordString;

    [PopUp("Hi", "Hello","YOHO")]
    public string dropDownStringEnum;

    [ReplaceCharacter('#','@')]
    public string characterReplacement;

    [Colorize(25,0,0)]
    public string colorizedString;

    [Colorize(255,255,0)]
    public int colorizedInt;

    [DescriptiveToggle("YES","NO")]
    public bool yesNo;

    [DescriptiveToggle("ON", "OFF VALUE")]
    public bool onOff = true;

    [TexturePreview]
    public Texture tex;

    [TimeMinutes]
    public int minutes;

    [TimeHours]
    public int hours;

    [ScriptableObjectSelector(typeof(Weapon))]
    public Weapon myWeapon;
}
