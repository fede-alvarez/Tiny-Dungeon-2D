using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private GameObject gameoverScreen;
    private bool isGameover = false;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }else{
            instance = this;
        }
    }

    public void OnDead()
    {
        print("Player dead!");
        gameoverScreen.SetActive(true);
        isGameover = true;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        if (instance != null)
            instance = null;
    }
    
    public static GameManager GetInstance
    {
        get { return instance;}
    }

    public bool IsGameover {
        get { return isGameover; }
    }
}
