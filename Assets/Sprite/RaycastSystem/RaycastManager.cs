using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastManager : MonoBehaviour
{
    private Transform highLight; //doi tuong dang duoc highlight
    private Transform aim; //doi tuong dang nhin
    private RaycastHit hit;

    //public LayerMask layerMask;//lop chi dinh co the nhat
    public InventoryManager inventoryManager;// Them refence InventoryManager de quan ly

    // Update is called once per frame
    void Update()
    {
        //Tat outline truoc do (Kiem tra outline co ton tai khong)
        if(highLight != null)
        {
            Outline previousOutline = highLight.GetComponent<Outline>();
            if(previousOutline != null) 
            {
                previousOutline.enabled = false;
            }

            highLight = null;
        }
        
        Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(centerScreen);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 3f))
        {
            highLight = hit.transform;
            
            //neu toi tuong co tag Lighted va khong phai la vat the dang nham
            if(highLight.CompareTag("Lighted") && highLight != aim)
            {
                Outline outline = highLight.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 5f;
                }
            }
            else
            {
                Debug.Log("Khong co outline");
            }
        }
        //nhat do lkhi nhan E
        if(Input.GetKeyDown(KeyCode.E))
        {
            //kiem tra vat co the nhat
            if (highLight != null && highLight.CompareTag("Lighted") && highLight != aim)
            {
                //them vat vao inventory
                if(inventoryManager != null)
                {
                    inventoryManager.AddItem(highLight.gameObject);
                }
                else
                {
                    Debug.Log("Khong co InventoryManager");
                }

                Destroy(highLight.gameObject);
                highLight = null;
            }
            else
            {
                Debug.Log("Khong co gi de nhat");
            }
        }
    }
}
