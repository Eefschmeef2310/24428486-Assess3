using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomTagline : MonoBehaviour
{
    public List<string> taglines;
    public TextMeshProUGUI text;
    void Start()
    {
        text.text = taglines[Random.Range(0,taglines.Count)];
    }
}
