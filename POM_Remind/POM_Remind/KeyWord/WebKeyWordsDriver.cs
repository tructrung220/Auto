using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Remind.KeyWord
{
    public class WebKeyWordsDriver
    {
        IWebDriver driver;
        public WebKeyWordsDriver(IWebDriver driverContructor)
        {
            this.driver = driverContructor;
        }

        /// <summary>
        /// This method will naviagte to URL 
        /// It require param with exactly in URL format 
        /// </summary>
        /// <param name="url"></param>
        public void OpenURL(string url)
        {
            if (!(url.StartsWith("https://") || url.StartsWith("http://")))
                throw new Exception("URL is invalid format and cannot open page");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// This method is use for
        /// select option from dropdown list or combobox
        /// </summary>
        /// <param name="element"></param>
        /// <param name="type"></param>
        /// <param name="options"></param>
        public void Select(WebElement element, SelectType type, string option)
        {
            SelectElement select = new SelectElement(element);
            switch(type)
            {
                case SelectType.SelectByIndex:
                    try
                    {
                        select.SelectByIndex(Int32.Parse(option));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().ToString());
                        throw new ArgumentException("Please input numberic on selectOption for SelectType.SelectByIndex");
                    }
                    break;

                case SelectType.SelectByText:
                    select.SelectByText(option);
                    break;

                case SelectType.SelectByValue:
                    select.SelectByValue(option);
                    break;
            } 
        }
        
        public enum SelectType
        {
            SelectByIndex,
            SelectByText,
            SelectByValue
        }

        /// <summary>
        /// This method use for 
        /// click 
        /// </summary>
        /// <param name="element"></param>
        public void Click(IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Build().Perform();
            action.Click();
        }

        /// <summary>
        /// This method user for 
        /// enter text 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public void SetText(IWebElement element, string text)
        {
            try
            {
                element.Clear();
                element.SendKeys(text);
            }
            catch (WebDriverException e)
            {
                throw new Exception("Element is not enable for set text" + "\r\n" + "error: " + e.Message);
            }
        }

        /// <summary>
        /// This method use for 
        /// wait element ready to click 
        /// </summary>
        /// <param name="locatorValue"></param>
        /// <param name="timeOut"></param>
        public void WaitElementToBeClickable(By locatorValue, int timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(ExpectedConditions.ElementToBeClickable(locatorValue));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new OperationCanceledException("Get " + e.Message + ", " + locatorValue + " is not ready for clickable");
            }
        }

        /// <summary>
        /// This method use for 
        /// wait element visible on DOM
        /// </summary>
        /// <param name="locatorValue"></param>
        /// <param name="timeOut"></param>
        public void WaitElementVisible(By locatorValue, int timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(ExpectedConditions.ElementIsVisible(locatorValue));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new OperationCanceledException("Get " + e.Message + ", " + locatorValue + " is not visible");
            }
        }

        /// <summary>
        /// This method use for 
        /// wait title of page contain string user want
        /// </summary>
        /// <param name="title"></param>
        /// <param name="timeOut"></param>
        public void WaitTitleContains(string title, int timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(ExpectedConditions.TitleContains(title));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new OperationCanceledException("Get " + e.Message + ", [" + title + "] is not displayed in WebPage title [" + driver.Title + "]");
            }
        }

        /// <summary>
        /// This method use for 
        /// get attribute of element in DOM
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string GetAttribute(IWebElement element, string attribute)
        {
            return element.GetAttribute(attribute);
        }

        /// <summary>
        /// This method use for Driver title of page
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return driver.Title;
        }

        /// <summary>
        /// This method is use for
        /// return value of css
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetCssValue(IWebElement element, string value)
        {
            return element.GetCssValue(value);
        }

        /// <summary>
        /// This method is use for
        /// return source code of current page
        /// </summary>
        /// <returns></returns>
        public string GetPageSource()
        {
            return driver.PageSource;
        }

        /// <summary>
        /// This method use for 
        /// wait page load completed
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="time"></param>
        public void WaitForPageToLoad(int time)
        {
            TimeSpan timeout = new TimeSpan(0, 0, time);
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            if (!(driver is IJavaScriptExecutor javascript))
                throw new ArgumentException("driver", "Driver must support javascript execution");
            wait.Until((d) =>
            {
                try
                {
                    return javascript.ExecuteScript("return document.readyState").Equals("complete");
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    return e.Message.ToLower().Contains("unable to Driver browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// This method use for
        /// set attribute of element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public void SetAttribute(IWebElement element, string attributeName, string value)
        {
            IWrapsDriver wrappedElement = element as IWrapsDriver;
            if (wrappedElement == null)
                throw new ArgumentException("element", "Element must wrap a web driver");

            IWebDriver driver = wrappedElement.WrappedDriver;
            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("element", "Element must wrap a web driver that supports javascript execution");
            javascript.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", element, attributeName, value);
        }

        /// <summary>
        /// This method use for 
        /// clear any text on text field
        /// </summary>
        /// <param name="element"></param>
        public void ClearText(IWebElement element)
        {
            element.Clear();
        }

        /// <summary>
        /// This method is use for
        /// Execute javascript
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public IJavaScriptExecutor JavaScript(IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }

        public IWebElement FindElement(string locatorType, string locatorValue)
        {
            IWebElement element = null;
            switch (locatorType.ToUpper())
            {
                case "id":
                    element = driver.FindElement(By.Id(locatorValue));
                    break;
                case "name":
                    element = driver.FindElement(By.Name(locatorValue));
                    break;
                case "xpath":
                    element = driver.FindElement(By.XPath(locatorValue));
                    break;
                case "tag":
                    element = driver.FindElement(By.TagName(locatorValue));
                    break;
                case "link":
                    element = driver.FindElement(By.LinkText(locatorValue));
                    break;
                case "css":
                    element = driver.FindElement(By.CssSelector(locatorValue));
                    break;
                case "class":
                    element = driver.FindElement(By.ClassName(locatorValue));
                    break;
                default:
                    throw new ArgumentException("Support FindElement with 'id' 'name' 'xpath' 'tag' 'link' 'css' 'class'");
            }
            return element;
        }

        /// <summary>
        /// This method is use for
        /// return elements in list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement> FindElements(string LocatorType, string LocatorValue)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            switch (LocatorType.ToLower())
            {
                case "id":
                    elements = driver.FindElements(By.Id(LocatorValue));
                    break;
                case "name":
                    elements = driver.FindElements(By.Name(LocatorValue));
                    break;
                case "xpath":
                    elements = driver.FindElements(By.XPath(LocatorValue));
                    break;
                case "tag":
                    elements = driver.FindElements(By.TagName(LocatorValue));
                    break;
                case "link":
                    elements = driver.FindElements(By.LinkText(LocatorValue));
                    break;
                case "css":
                    elements = driver.FindElements(By.CssSelector(LocatorValue));
                    break;
                case "class":
                    elements = driver.FindElements(By.ClassName(LocatorValue));
                    break;
                default:
                    throw new ArgumentException("Support FindElement with 'id' 'name' 'xpath' 'tag' 'link' 'css' 'class'");
            }
            return elements;
        }
    }
}
