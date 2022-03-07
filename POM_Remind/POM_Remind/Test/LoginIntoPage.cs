using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POM_Remind.POM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace POM_Remind.Test
{
    public class LoginIntoPage
    {
        string email = "admin@phptravels.com";
        string passWord = "demoadmin";
        string URL = "https://www.phptravels.net/admin/";
        IWebDriver driver;
        LoginPage loginPage;
        public LoginIntoPage()
        {
        }

        [Fact]
        public void LoginWithValidAccount()
        {
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver);
            loginPage.Navigate(URL);
            loginPage.Login(email, passWord);
        }
    }
}
