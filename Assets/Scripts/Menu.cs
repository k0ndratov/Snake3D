using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public void OnStartHendler()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnExitHandler()
    {
        Application.Quit();
    }
}
