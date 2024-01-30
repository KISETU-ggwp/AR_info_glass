using NRKernal;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MoveText : MonoBehaviour, IPointerClickHandler
{
    private GameObject selectedObject;
    private Transform originalParent;
    private Vector3 offset;

    void Update()
    {
        if (selectedObject != null)
        {
            Quaternion controllerRotation = NRInput.GetRotation(NRInput.DomainHand);

            Vector3 laserBeamPosition = (controllerRotation * Vector3.forward * 5f);
            selectedObject.transform.position = laserBeamPosition + offset;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.GetComponent<TextMeshProUGUI>() != null)
        {
            if (selectedObject != null)
            {
                selectedObject.transform.SetParent(originalParent);
                selectedObject = null;
            }

            selectedObject = eventData.pointerPress;
            originalParent = selectedObject.transform.parent;
            selectedObject.transform.SetParent(null);

            Quaternion controllerRotation = NRInput.GetRotation(NRInput.DomainHand);
            Vector3 laserBeamPosition = controllerRotation * Vector3.forward * 5f;

            offset = selectedObject.transform.position - laserBeamPosition;
        }
    }
}