using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject prefab; // Assign your prefab in the inspector

    // Call this method when the button is pressed
    public void InstantiateAtButtonLocation()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }

        // Calculate the position relative to the button
        // For example, instantiate the prefab 1 meter in front of the button
        Vector3 instantiationPosition = transform.position + transform.forward * 0.3f;

        // Instantiate the prefab at the calculated position
        Instantiate(prefab, instantiationPosition, Quaternion.identity);
    }
}
