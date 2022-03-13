using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace POM_Remind.Test
{
    public class Test
    {
        IWebDriver driver;
        
        [Fact]
        public void Navigate()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.phptravels.net/admin/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}
