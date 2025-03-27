using KeySystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MusicBoxManager : MonoBehaviour
{
    [SerializeField] Animator _musicBox;
    [SerializeField] GameObject _handle;
    [SerializeField] GameObject _key;

    [SerializeField] AudioSource _musicBoxSound;
    [SerializeField] AudioClip _musicBoxClip;
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
                StartCoroutine(OpenMusicBox());
                Debug.Log("Music Box is opened");
            }
            else
            {
                Debug.Log("Music Box is locked");
            }
        }
    }
    public void TryGetKey()
    {
            _key.SetActive(true);
    }

    private IEnumerator OpenMusicBox()
    {
        _musicBox.SetTrigger("Handle");
        yield return new WaitForSeconds(3f);
        _musicBox.SetTrigger("Open");
        Debug.Log("Music Box is opened");
    }

    public void PlayMusicBoxSound()
    {
        _musicBoxSound.clip = _musicBoxClip;
        _musicBoxSound.Play();
    }
    public void StopMusicBoxSound()
    {
        _musicBoxSound.Stop();
    }
}
