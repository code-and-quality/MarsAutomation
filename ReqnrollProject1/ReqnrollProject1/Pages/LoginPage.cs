using MarsAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAutomation.Pages;

public class LoginPage
{
    //Function that allows user to login to the website
    public void SignIn(IWebDriver driver) 
    {
        //Launch the portal
        driver.Navigate().GoToUrl("http://localhost:5003/");
        driver.Manage().Window.Maximize();
        //Identify the sign in button and click on it
        //IWebElement Signinbutton=driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/form/div[3]/input[1]")); 
        IWebElement Signinbutton = driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));

        Signinbutton.Click();
        Thread.Sleep(2000);

    }

  
     public void LoginActions(IWebDriver driver)
     {

         SignIn(driver);

          try
          {
          IWebElement usernameTextbox = driver.FindElement(By.Name("email"));
          usernameTextbox.SendKeys("susmitha.pinki@gmail.com");

          }
          catch (Exception ex)
          {
           Assert.Fail("username textbox not located");
           }

       //Identify the username textbox and enter a valid username    
       //Explicit Wait we need wait helpers selenium nuget pkg for this
          Wait.WaitToBeVisible(driver, "Name", "email", 2);


      //Identify the password textbox and enter a valid password

        IWebElement passwordTextbox = driver.FindElement(By.Name("password"));
        passwordTextbox.SendKeys(("123123#"));
        Wait.WaitToBeVisible(driver, "Name", "password", 2);

      //Identify the login button and click on it

         IWebElement Loginbutton = driver.FindElement(By.XPath("//button[normalize-space()='Login']\n"));
         Loginbutton.Click();
         Wait.WaitToBeClickable(driver, "XPath", locatorValue: "//button[normalize-space()='Login']\n", 2);
         Thread.Sleep(2000);
     }

    public void EnterCredentials(IWebDriver driver, string username, string password)
    {
        SignIn(driver);
        
        driver.FindElement(By.Name("email")).Clear();
        driver.FindElement(By.Name("email")).SendKeys(username);

        driver.FindElement(By.Name("password")).Clear();
        driver.FindElement(By.Name("password")).SendKeys(password);
    }

    public void ClickLogin(IWebDriver driver)
    {
        driver.FindElement(By.XPath("//button[text()='Login']")).Click();
        Thread.Sleep(3000);
    }


    public string GetLoginErrorMessage(IWebDriver driver)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        try
        {
            IWebElement messageBox = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//*[contains(text(),'Invalid') or contains(text(),'incorrect') or contains(text(),'Confirm') or contains(text(),'email')]")
                )
            );
            return messageBox.Text;
        }
        catch
        {
            return "";
        }
    }

}
