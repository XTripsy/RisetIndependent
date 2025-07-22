using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookLockController : MonoBehaviour
{
    [SerializeField] CodeGenerator codeGenerator;
    [SerializeField] TMP_InputField codeInputField;
    [SerializeField] Button submitButton;
    [SerializeField] GameObject bookLockedUI;
    [SerializeField] GameObject bookContentUI;

    void Awake()
    {
        submitButton.onClick.AddListener(VerifyCode);
        bookLockedUI.SetActive(true);
        bookContentUI.SetActive(false);
    }

    void OnDestroy()
    {
        submitButton.onClick.RemoveListener(VerifyCode);
    }

    void VerifyCode()
    {
        string input = codeInputField.text.ToUpper();
        if (input == codeGenerator.CurrentCode)
        {
            UnlockBook();
            GameEventSystem.Instance.TriggerBookUnlocked();
        }
        else
        {
            codeInputField.text = "";
            Debug.Log("Incorrect moderatedCode incorrect!");
        }
    }

    void UnlockBook()
    {
        bookLockedUI.SetActive(false);
        bookContentUI.SetActive(true);
        Debug.Log("Buku Terbuka");
    }
}