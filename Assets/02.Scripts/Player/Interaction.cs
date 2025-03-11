using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private Camera _camera;

    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;

    public bool canInteract;
    private void Awake()
    {
        _camera = Camera.main;
        canInteract = true;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            if(hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponentInParent<IInteractable>(); // 본인 포함
                SetPromptText();
            }
        }
        else
        {
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractInfo();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable!= null && canInteract)
        {
            curInteractable.OnInteract();
            curInteractable = null;
            curInteractGameObject = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
