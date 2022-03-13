using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Remind.POM
{
    public class LoginPage: BasePage
    {
        //element login page
        private string Email = "//label//input[@name='email']";
        private string PassWord = "//input[@type='password']";
        private string SubmitBtn = "//button[@type='submit']";
        public LoginPage(IWebDriver driver): base(driver)
        {
            
        }

        public override void Navigate(string url)
        {
           
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void Login(string email, string passWord)
        {
            keyWords.SetText(keyWords.FindElement("xpath", Email), email);
            keyWords.SetText(keyWords.FindElement("xpath", PassWord), passWord);
            driver.FindElement(By.XPath(SubmitBtn)).Click();
            //keyWords.Click(keyWords.FindElement("xpath",SubmitBtn));
        }
    }
}
