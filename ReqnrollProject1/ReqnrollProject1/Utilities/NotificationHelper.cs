using OpenQA.Selenium;

namespace MarsAutomation.Utilities
{
    public static class NotificationHelper
    {
        public static string GetToastMessage(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            string script = "return document.querySelector('.ns-box-inner')?.innerText;";
            string? message = js.ExecuteScript(script) as string;

            return message ?? string.Empty;
        }
    }

}
