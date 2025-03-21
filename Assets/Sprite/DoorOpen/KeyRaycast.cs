using UnityEngine;
using UnityEngine.UI;

namespace KeySystem
{
    public class KeyRaycast : MonoBehaviour
    {
        [SerializeField] private int rayLength = 5;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string exluseLayerName = null;

        private KeyItemController raycasteObject;
        [SerializeField] private KeyCode openDoorKey = KeyCode.E;

        [SerializeField] private Image crosshair = null;
        private bool isCrosshairActive;
        private bool doOne;

        private string interactableTag = "Interactable";

        private void Update()
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            int mask = 1 << LayerMask.NameToLayer(exluseLayerName) | layerMaskInteract.value;

            if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
            {
                if (hit.collider.CompareTag(interactableTag))
                {
                    if (!doOne)
                    {
                        raycasteObject = hit.collider.GetComponent<KeyItemController>();
                        CrosshairChange(true);
                    }

                    isCrosshairActive = true;
                    doOne = true;

                    if (Input.GetKeyDown(openDoorKey))
                    {
                        raycasteObject.ObjectInteration();
                    }
                }
            }
            else
            {
                if (isCrosshairActive)
                {
                    crosshair.enabled = false;
                    isCrosshairActive = false;
                }
            }
        }
        void CrosshairChange(bool on)
        {
            if (on && !doOne)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
                isCrosshairActive = false;
            }
        }
    }
}