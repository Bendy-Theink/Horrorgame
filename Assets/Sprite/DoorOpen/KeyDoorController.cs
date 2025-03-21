using UnityEngine;

namespace KeySystem
{
    public class KeyDoorController : MonoBehaviour
    {
        private Animator _animator;
        private bool doorOpen = false;

        [Header("Animation")]
        [SerializeField] private string closedAnimation = "Handle";
        [SerializeField] private string openAnimation = "Open";

        [SerializeField] private int timeShowUI = 1;
        [SerializeField] private GameObject showDoorLockedUI = null;
    }
}
