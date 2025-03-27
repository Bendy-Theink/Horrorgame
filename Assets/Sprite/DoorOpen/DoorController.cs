using KeySystem;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _hanleDoor;
    [SerializeField] private Animator _openDoor;

    [SerializeField] private AudioSource _doorSound;
    [SerializeField] private AudioClip _doorClip;

    public bool isLooked = true; //cua bi khoa mac dinh

    public void TryOpenDoor()
    {
        KeyInventory keyInventory = FindObjectOfType<KeyInventory>();
        if (keyInventory != null && keyInventory.hasRedKey)
        {
            isLooked = false;
            StartCoroutine(OpenDoorSequnce());
        }
    }
    public void TryOpenDoor2()
    {
        KeyInventory keyInventory = FindObjectOfType<KeyInventory>();
        if (keyInventory != null && keyInventory.hasGreenKey)
        {
            isLooked = false;
            StartCoroutine(OpenDoorSequnce());
        }
    }
    public void TryOpenDoor3()
    {
        KeyInventory keyInventory = FindObjectOfType<KeyInventory>();
        if (keyInventory != null && keyInventory.hasEndKey)
        {
            isLooked = false;
            StartCoroutine(OpenDoorSequnce());
        }
    }
    public void TryOpenTreasure()
    {
        KeyInventory keyInventory = FindObjectOfType<KeyInventory>();
        if (keyInventory != null && keyInventory.hasSecretKey)
        {
            isLooked = false;
            StartCoroutine(OpenTreasureSequnce());
        }
    }

    private IEnumerator OpenDoorSequnce()
    {
        isLooked = false;
        if(_hanleDoor != null)
        {
            _hanleDoor.SetTrigger("Open");
            yield return new WaitForSeconds(1f);
        }
        if(_openDoor != null)
        {
            _openDoor.SetTrigger("Open");
        }
    }
    private IEnumerator OpenTreasureSequnce()
    {
        isLooked = false;
        if (_hanleDoor != null)
        {
            yield return new WaitForSeconds(1f);
            _hanleDoor.SetTrigger("Open");
        }
    }
    public void PlayDoorSound()
    {
        _doorSound.clip = _doorClip;
        _doorSound.Play();
    }
    public void enddd()
    {
        
        SceneManager.LoadScene(4);
    }
}
