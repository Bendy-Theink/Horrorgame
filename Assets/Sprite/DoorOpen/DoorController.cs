using KeySystem;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _hanleDoor;
    [SerializeField] private Animator _openDoor;


    public bool isLooked = true; //cua bi khoa mac dinh

    public void TryOpenDoor()
    {
        if (isLooked)
        {
            //kiem tra nguoi choi co chia khoa hay khong
            KeyInventory keyInventory = FindObjectOfType<KeyInventory>();
            if(keyInventory != null && keyInventory.hasRedKey)
            {
                isLooked = false;
                StartCoroutine(OpenDoor());
                Debug.Log("Cua da mo");
            }
            else
            {
                Debug.Log("Cua bi khoa");
            }
        }
    }
    private IEnumerator OpenDoor()
    {
        _hanleDoor.SetTrigger("Handle");
        yield return new WaitForSeconds(1.5f);
        _openDoor.SetTrigger("Open");
    }
}
