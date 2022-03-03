using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCounter : MonoBehaviour
{
    public TMP_InputField trackable;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Character count text: " + trackable.text.Length;
    }
}
