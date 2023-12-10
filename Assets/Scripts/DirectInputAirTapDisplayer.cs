using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DirectInputAirTapDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private InputActionReference leftHandReference;
    [SerializeField]
    private InputActionReference rightHandReference;

    public TextMeshProUGUI debugDialog;
    
    private void Start()
    {
        debugDialog.text += "start function run for airtap displayer";
        //leftHand.SetActive(false);
        //rightHand.SetActive(false);
        leftHandReference.action.performed += ProcessLeftHand;
        rightHandReference.action.performed += ProcessRightHand;
    }

    private void ProcessRightHand(InputAction.CallbackContext ctx)
    {
        debugDialog.text += "right hand event trigger";
        ProcessHand(ctx, rightHand);
    }

    private void ProcessLeftHand(InputAction.CallbackContext ctx)
    {
        debugDialog.text += "left hand event trigger";
        ProcessHand(ctx, leftHand);
    }

    private void ProcessHand(InputAction.CallbackContext ctx, GameObject g)
    {
        g.SetActive(ctx.ReadValue<float>() > 0.95f);
        debugDialog.text += "ProcessHand code block";
    }

    private void OnDestroy()
    {
        leftHandReference.action.performed -= ProcessLeftHand;
        rightHandReference.action.performed -= ProcessRightHand;
    }
}