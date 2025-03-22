using System.Collections;
using TMPro;
using UnityEngine;

namespace KeySystem
{
    public class KeyDoorController : MonoBehaviour
    {
        private bool doorOpen = false;

        [Header("Animation")]
        [SerializeField] private Animator doorAnim;
        [SerializeField] private Animator doorAnim2;

        [SerializeField] private int timeShowUI = 1;
        [SerializeField] private TextMeshProUGUI showDoorLockedUI = null;

        [SerializeField] private KeyInventory _keyInventory = null;

        [SerializeField] private int waitTime = 1;
        [SerializeField] private bool pauseInteraction = false;

        private IEnumerator PasueDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTime);
            pauseInteraction = false;
        }

        public void PlayAnimation()
        {
            if(_keyInventory.hasRedKey)
            {
                Debug.Log("Open Door");
                OpenDoor();
            }
            else
            {
                StartCoroutine(ShowDoorLocked());
            }
        }

        void OpenDoor()
        {
            if (!doorOpen && !pauseInteraction)
            {
                doorAnim.SetTrigger("Handle");
                doorOpen = true;
                StartCoroutine(PasueDoorInteraction());
                doorAnim2.SetTrigger("Open");  
            }
        }
        IEnumerator ShowDoorLocked()
        {
            showDoorLockedUI.gameObject.SetActive(true);
            showDoorLockedUI.text = "Cửa đang khóa";
            yield return new WaitForSeconds(timeShowUI);
            showDoorLockedUI.gameObject.SetActive(false);
        }
    }
}
