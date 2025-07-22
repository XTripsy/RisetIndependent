using UnityEngine;
using System.Linq;

public class CodeGenerator : MonoBehaviour
{
    [SerializeField] CodeConfig codeConfig;
    string currentCode;

    public string CurrentCode => currentCode;

    void Awake()
    {
        GenerateNewCode();
    }

    public void GenerateNewCode()
    {
        currentCode = string.Concat(Enumerable.Range(0, codeConfig.codeLength)
            .Select(_ => codeConfig.allowedCharacters[Random.Range(0, codeConfig.allowedCharacters.Length)]));
        
        Debug.Log($"Generated Code: {currentCode}");
        GameEventSystem.Instance.TriggerCodeGenerated(currentCode);
    }
}