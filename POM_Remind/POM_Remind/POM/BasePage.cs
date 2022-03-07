
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POM_Remind.KeyWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Remind.POM
{
    public class BasePage
    {
        public IWebDriver driver;
        public WebKeyWordsDriver keyWords;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.keyWords = new WebKeyWordsDriver(driver);
        }

        public virtual void Navigate(string url)
        {

        }

        public void ImplicitWait(int time)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }
    }
}
