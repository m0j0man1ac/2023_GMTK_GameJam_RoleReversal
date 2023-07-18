using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Text SO/DialougeGroup")]
public class DialougeGroup : ScriptableObject
{
    public string[] dialouges;
    public TextAsset _file;

    private void OnValidate()
    {
        dialouges = _file ? _file.text.Split(new string[] {"\n"}, System.StringSplitOptions.RemoveEmptyEntries) : null;
    }
}
