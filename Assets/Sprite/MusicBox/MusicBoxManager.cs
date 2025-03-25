using KeySystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MusicBoxManager : MonoBehaviour
{
    [SerializeField] Animator _musicBox;
    [SerializeField] GameObject _handle;
    public bool isLooked = true;

    public void TryOpenMusicBox()
    {
        if(isLooked)
        {
            KeyInventory keyInventory = FindObjectOfType<KeyInventory>();
            if(keyInventory != null && keyInventory.hasHanldeMusicBox)
            {
                _handle.SetActive(true);
                isLooked = false;
                _musicBox.SetTrigger("Open");
                _musicBox.SetTrigger("Handle");
                Debug.Log("Music Box is opened");
            }
            else
            {
                Debug.Log("Music Box is locked");
            }
        }
    }

    private IEnumerator OpenMusicBox()
    {
        yield return new WaitForSeconds(3f);
        _musicBox.SetTrigger("Open");
        _musicBox.SetTrigger("Handle");
        Debug.Log("Music Box is opened");
    }
}
