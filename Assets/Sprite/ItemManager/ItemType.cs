using UnityEngine;

public class ItemType : MonoBehaviour
{
    public enum Type {Flashlight, Key, Note, HandleMusicBox, Other}
    public Type itemType = Type.Other;
}
