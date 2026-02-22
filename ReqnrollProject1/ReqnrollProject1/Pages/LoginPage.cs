using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAutomation.Pages;

public class LoginPage
{

    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
    }
    //Function that allows user to login to the website

    private By SignInButton = By.XPath("//*[@id='home']/div/div/div[1]/div/a");
    private By EmailTextbox = By.Name("email");
    private By PasswordTextbox = By.Name("password");
    private By LoginButton = By.XPath("//button[normalize-space()='Login']");
    private By LoginErrorMessage = By.XPath(
        "//*[contains(text(),'Invalid') or contains(text(),'incorrect') or contains(text(),'Confirm') or contains(text(),'email')]"
    );

    public void NavigateToPortal()
    {
        driver.Navigate().GoToUrl("http://localhost:5003/");
        driver.Manage().Window.Maximize();
    }

    public void ClickSignIn()
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(SignInButton)).Click();
    }

    public void EnterUsername(string username)
    {
        IWebElement email = wait.Until(ExpectedConditions.ElementIsVisible(EmailTextbox));
        email.Clear();
        email.SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        IWebElement pwd = wait.Until(ExpectedConditions.ElementIsVisible(PasswordTextbox));
        pwd.Clear();
        pwd.SendKeys(password);
    }

    public void ClickLogin()
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton)).Click();
    }
        public void Login(string username, string password)
    {
        NavigateToPortal();
        ClickSignIn();
        EnterUsername(username);
        EnterPassword(password);
        ClickLogin();
    }

    public string GetLoginErrorMessage()
    {
        try
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(LoginErrorMessage)).Text;
        }
        catch (WebDriverTimeoutException)
        {
            return string.Empty;
        }
    }
}


    
