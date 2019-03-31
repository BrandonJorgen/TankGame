using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial04Script : MonoBehaviour
{
    void LoadScene(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
