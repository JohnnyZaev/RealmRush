using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;

    [SerializeField] private TextMeshProUGUI displayBalance;

    private int _currentBalance;
    public int CurrentBalance
    {
        get { return _currentBalance;  }
    }

    private void Awake()
    {
        _currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int value)
    {
        _currentBalance += Mathf.Abs(value);
        UpdateDisplay();
    }
    
    public void Withdraw(int value)
    {
        _currentBalance -= Mathf.Abs(value);
        UpdateDisplay();

        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private void UpdateDisplay()
    {
        displayBalance.text = $"Gold: {_currentBalance}";
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
