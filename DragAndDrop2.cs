using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using UnityEngine.UI; // UIコンポーネント用
using TMPro; // TextMeshPro用

public class DragAndDrop2 : MonoBehaviour
{
    [SerializeField] private float maxGrabDistance = 1000f, throwForce = 2000f, lerpSpeed = 10000f;
    [SerializeField] private Transform objectHolder;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    private Rigidbody grabbedRB;
    private Collider grabbedCollider;
    private bool isButtonPressed = false;

    public Text buttonText; // 通常のテキストUI
    public TextMeshProUGUI textMeshProComponent; // TextMeshPro UI

    void Update()
    {
        if (isButtonPressed)
        {
            if (grabbedRB)
            {
                Vector3 targetPosition = new Vector3(
                    objectHolder.transform.position.x, 
                    objectHolder.transform.position.y, 
                    grabbedRB.position.z);

                grabbedRB.MovePosition(Vector3.SmoothDamp(grabbedRB.position, targetPosition, ref velocity, lerpSpeed));
            }
            else
            {
                var handControllerAnchor = NRInput.DomainHand == ControllerHandEnum.Left ? ControllerAnchorEnum.LeftLaserAnchor : ControllerAnchorEnum.RightLaserAnchor;
                Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : handControllerAnchor);
                RaycastHit hitResult;
                if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, maxGrabDistance))
                {
                    grabbedRB = hitResult.collider.gameObject.GetComponent<Rigidbody>();
                    grabbedCollider = hitResult.collider;

                    if (grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                        if (grabbedCollider) grabbedCollider.enabled = false; // Disable collider while moving
                    }
                }
            }

            UpdateTextAndColor("動いてる", Color.cyan);
        }
        else if (grabbedRB)
        {
            grabbedRB.isKinematic = false;
            grabbedRB.velocity = Vector3.zero;
            grabbedRB.angularVelocity = Vector3.zero;
            if (grabbedCollider) grabbedCollider.enabled = true; // Re-enable collider
            grabbedRB = null;
            grabbedCollider = null;

            UpdateTextAndColor("とまってる", Color.white);
        }
    }

    public void HandleButtonPress()
    {
        isButtonPressed = !isButtonPressed;
    }

    private void UpdateTextAndColor(string text, Color color)
    {
        if (buttonText != null)
        {
            buttonText.text = text;
        }

        if (textMeshProComponent != null)
        {
            textMeshProComponent.color = color;
        }
    }
}
