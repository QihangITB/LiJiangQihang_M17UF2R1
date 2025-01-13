using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)] private string[] _lines;
    public string[] Lines { get => _lines; }
}
