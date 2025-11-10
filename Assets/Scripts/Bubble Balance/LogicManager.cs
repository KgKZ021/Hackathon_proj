using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LogicManager : MonoBehaviour
{
    
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Image touchInput;
    //[SerializeField] private GameObject pauseButton;

    void Start()
    {
        GameInput.Instance.OnPaused += GameInput_OnPaused;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       

    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        touchInput.gameObject.SetActive(false);
        Time.timeScale = 0f;

    }
    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    private void GameInput_OnPaused(object sender, System.EventArgs e)
    {
        Pause();
    }
    
}
