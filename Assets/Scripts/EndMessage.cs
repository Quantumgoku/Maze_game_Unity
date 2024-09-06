using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMessage : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
            HideText();
        }
        else
        {
            Debug.LogError("TextMeshPro component not found.");
        }
    }

    public void ShowText(string message)
    {
        if (textComponent != null)
        {
            textComponent.text = message;
            textComponent.enabled = true;
        }
        else
        {
            Debug.LogError("TextMeshPro component not found.");
        }
    }

    public void HideText()
    {
        if (textComponent != null)
        {
            textComponent.enabled = false;
        }
        else
        {
            Debug.LogError("TextMeshPro component not found.");
        }
    }
}
