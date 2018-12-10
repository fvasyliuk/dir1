using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary4
{
    [TestFixture]
    public class Class1
    {



        IWebDriver driver;
        [OneTimeSetUp]
        public void Initialize()
        {
            var option = new ChromeOptions();
            option.AddArguments
                (
                    "-headless"
                );
            driver = new ChromeDriver(option);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl("http://www.leafground.com/home.html");
            new Actions(driver).KeyDown(Keys.Control).Click(driver.FindElement(By.LinkText("HyperLink"))).Perform();
            new Actions(driver).KeyUp(Keys.Control).Perform();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            new Actions(driver).MoveToElement(driver.FindElement(By.LinkText("Go to Home Page"))).Perform();
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var screenshotPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "screenshot.png");
            screenshot.SaveAsFile(screenshotPath);
            TestContext.AddTestAttachment(screenshotPath);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Navigate().GoToUrl("https://jqueryui.com/demos/");
            driver.FindElement(By.LinkText("Droppable")).Click();
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector(".demo-frame")));
            var smallBox = driver.FindElement(By.Id("draggable"));
            var bigBox = driver.FindElement(By.Id("droppable"));
            new Actions(driver).DragAndDrop(smallBox, bigBox).Perform();
            bigBox = driver.FindElement(By.Id("droppable"));
            Assert.That(bigBox.Text, Does.Contain("Dropped"));





        }

        [OneTimeTearDown]
        public void Clear()
        {
            driver.Quit();
        }
    }
}
