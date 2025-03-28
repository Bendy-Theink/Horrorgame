using UnityEngine;

public class ItemType : MonoBehaviour
{
    public enum Type {Flashlight, Key1, Key2, Key3,Key4 , Note, HandleMusicBox, Other}
    public Type itemType = Type.Other;
}
