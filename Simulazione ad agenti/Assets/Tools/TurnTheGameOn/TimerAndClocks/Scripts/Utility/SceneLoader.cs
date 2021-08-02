namespace TurnTheGameOn.Timer
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoader : MonoBehaviour
    {
        public string sceneName;

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}