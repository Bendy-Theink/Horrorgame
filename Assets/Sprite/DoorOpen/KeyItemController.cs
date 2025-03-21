using KeySystem;
using UnityEngine;

namespace KeySystem
{
    public class KeyItemController : MonoBehaviour
    {
        [SerializeField] private bool redDoor = false;
        [SerializeField] private bool redKey = false;

        [SerializeField] private KeyInventory _keyInventory = null;

        private KeyDoorController doorObject;

        private void Start()
        {
            if (redDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }
        }
        public void ObjectInteration()
        {
            if (redDoor)
            {
                //doorObject.PlayAnimation;
            }
            else if (redKey)
            {
                _keyInventory.hasRedKey = true;
                Destroy(gameObject);
            }
        }
    }
}
