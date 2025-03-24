using UnityEngine;

public class ItemType : MonoBehaviour
{
    public enum Type {Flashlight, Key, Other}
    public Type itemType = Type.Other;
}
