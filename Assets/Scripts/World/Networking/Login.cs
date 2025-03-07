using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField fEmailInputField;
    [SerializeField] private TMP_InputField fPasswordInputField;
    [SerializeField] private TextMeshProUGUI fErrorText;

    [DllImport("__Internal")]
    private static extern int SignInWithEmailAndPassword(string email, string password);

    public void OnSubmitLogin()
    {
        Debug.Log("Login OnSubmitLogin(): Pressed");
        string lEmail = fEmailInputField.text.Trim();
        string lPassword = fPasswordInputField.text.Trim();

        if (ValidateInput(lEmail, lPassword))
        {
            Debug.Log("Login OnSubmitLogin(): Attempting to Login");
            SignInWithEmailAndPassword(lEmail, lPassword);
        }
    }


    // Method to be called by JavaScript code
    private void AuthenticateUser(int result)
    {
        if (result == 0)
        {
            Debug.Log("Login: Authentication Failed");
            fErrorText.text = "Incorrect email or password";
            return;
        }
        Debug.Log("Login: Authentication Success");
        SceneManager.LoadScene("World");
    }

    private bool ValidateInput(string email, string password)
    {
        Debug.Log("Login ValidateInput()");
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            fErrorText.text = "Please enter all required fields.";
            return false;
        }

        if (!ValidationService.IsValidEmail(email))
        {
            fErrorText.text = "Please a enter valid email";
            return false;
        }

        if (!ValidationService.IsValidPassword(password))
        {
            fErrorText.text = "Please enter password with 8 or more characters";
            return false;
        }

        return true;
    }

    public void removeErrorText()
    {
        fErrorText.text = "";
    }
}
