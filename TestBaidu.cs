using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium;
using Selenium.Internal.SeleniumEmulation;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace SeleniumTest
{
    [TestClass]
    public class TestBaidu
    {
        ISelenium selenium;
        INavigation navigation;
        FirefoxProfile fp;
        FirefoxDriver driver;
        const string localIPAddress = "127.0.0.1";
        const int port = 4444;
        const string targetExplorer = "firefox";
        const string tagetURL = "http://www.baidu.com";
        bool usingDriver = true;

        /// <summary>
        /// Initiates the testing environment
        /// </summary>
        public TestBaidu()
        {
            if (!usingDriver)
            {
                if (selenium == null)
                {                  
                    selenium = new DefaultSelenium(localIPAddress, port, targetExplorer, tagetURL);
                }
            }
            else
            {                
                fp = new FirefoxProfile();                
                fp.SetPreference("dom.disable_open_during_load", false);
                driver = new FirefoxDriver(fp);
            }
        }

        /// <summary>
        /// Assert the got page is Baidu
        /// </summary>
        [TestMethod]
        public void GeneralTestingBaidu()
        {
            string actualTitle = string.Empty;

            if (!usingDriver)
            {
                try
                {
                    //测试主页被打开
                    selenium.Start();
                    selenium.WindowMaximize();
                    selenium.Open("/");
                    actualTitle = selenium.GetTitle();
                    StringAssert.Equals(actualTitle, "百度一下，你就知道");
                    //测试糯米链接
                    selenium.Click("link=糯米");
                    selenium.WaitForPageToLoad("30000");
                    actualTitle = selenium.GetTitle();
                    StringAssert.Equals(actualTitle, "【上海团购】上海团购网站，高品质团购网站-百度糯米");
                    selenium.Open("/");

                    //测试新闻链接
                    selenium.Click("link=新闻");
                    selenium.WaitForPageToLoad("30000");
                    actualTitle = selenium.GetTitle();
                    StringAssert.Equals(actualTitle, "百度新闻搜索——全球最大的中文新闻平台");
                    selenium.GoBack();                    
                    //测试登陆
                    selenium.Click("link=登录");
                    //if (!selenium.IsTextPresent("登录百度账号"))
                    //{
                    //    Assert.Fail();
                    //}

                    selenium.Type("id=TANGRAM__PSP_8__userName", "13764393095");
                    selenium.Type("id=TANGRAM__PSP_8__password", "edGomvS1");
                    selenium.Click("id=TANGRAM__PSP_8__submit");
                    selenium.WaitForPageToLoad("30000");
                    //if (selenium.IsTextPresent("密码错误"))
                    //{
                    //    Assert.Fail();
                    //}
                    selenium.Click("link=退出");
                    selenium.Click("link=确定");
                    selenium.WaitForPageToLoad("30000");

                    //测试搜索
                    selenium.Type("kw", "Selenium");
                    selenium.Click("su");
                    selenium.WaitForPageToLoad("30000");
                    Assert.IsTrue(selenium.IsTextPresent("硒"));


                }
                catch (Exception e)
                {

                }
                finally
                {
                    selenium.Close();
                    selenium.Stop();
                }
            }
            else
            {
                try
                {
                    //测试主页被打开              
                    driver.Manage().Window.Maximize();                       
                    navigation = driver.Navigate();
                    navigation.GoToUrl(tagetURL);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
                    actualTitle = driver.Title;
                    StringAssert.Equals(actualTitle, "百度一下，你就知道");
                    //测试糯米链接
                    driver.FindElementByLinkText("糯米").Click();
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
                    actualTitle = driver.Title;
                    StringAssert.Equals(actualTitle, "【上海团购】上海团购网站，高品质团购网站-百度糯米");
                    navigation.Back();
                    //测试新闻链接
                    driver.FindElementByLinkText("新闻").Click();
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
                    actualTitle = driver.Title;
                    StringAssert.Equals(actualTitle, "百度新闻搜索——全球最大的中文新闻平台");
                    navigation.Back();
                    //测试登陆
                    driver.FindElementByLinkText("登录").Click();
                    //if (!selenium.IsTextPresent("登录百度账号"))
                    //{
                    //    Assert.Fail();
                    //}
                    IWebElement webElement = driver.FindElementById("TANGRAM__PSP_8__userName");
                    webElement.SendKeys("13764393095");
                    webElement = driver.FindElementById("TANGRAM__PSP_8__password");
                    webElement.SendKeys("edGomvS1");                    
                    driver.FindElementById("TANGRAM__PSP_8__submit").Click();
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
                    //if (selenium.IsTextPresent("密码错误"))
                    //{
                    //    Assert.Fail();
                    //}
                    Actions mouseMove = new Actions(driver);
                    webElement = driver.FindElementByClassName("user-name");
                    mouseMove.MoveToElement(webElement);
                    mouseMove.Perform();
                    webElement = driver.FindElementByClassName("quit");
                    webElement.Click();
                    driver.FindElementByLinkText("确定").Click();
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

                    //测试搜索
                    webElement = driver.FindElementById("kw");
                    webElement.SendKeys("Selenium");
                    driver.FindElementById("su").Click();
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
                    Assert.IsTrue(driver.FindElements(By.XPath("//*[contains(text(),'硒')]")).Count > 0);
                }
                catch (Exception e)
                {

                }
                finally
                {
                 
                    driver.Quit();                    
                }
           }
        }      

        ~TestBaidu()
        {


        }        
    }
}
