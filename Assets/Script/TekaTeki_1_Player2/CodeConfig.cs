using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CodeConfig", menuName = "Puzzle/CodeConfig", order = 1)]
public class CodeConfig : ScriptableObject
{
    public int codeLength = 3;
    public string allowedCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public int maxCodeLocations = 3;
}
