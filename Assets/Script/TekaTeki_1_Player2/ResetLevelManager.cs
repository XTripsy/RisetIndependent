using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevelManager : MonoBehaviour
{
    [SerializeField] CodeGenerator codeGenerator;
    [SerializeField] CodeLocationManager codeLocationManager;

    public void ResetLevel()
    {
        codeGenerator.GenerateNewCode();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}