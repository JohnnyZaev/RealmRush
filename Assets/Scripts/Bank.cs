using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;

    private int _currentBalance;
    public int CurrentBalance
    {
        get { return _currentBalance;  }
    }

    private void Awake()
    {
        _currentBalance = startingBalance;
    }

    public void Deposit(int value)
    {
        _currentBalance += Mathf.Abs(value);
    }
    
    public void Withdraw(int value)
    {
        _currentBalance -= Mathf.Abs(value);

        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
