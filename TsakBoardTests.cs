using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TaskBoardWebApp
{
    public class TaskboardWebTests
    {
        const string AppBaseUrl = "https://taskboard.blagosimov.repl.co/";
        IWebDriver driver;

        [OneTimeSetUp]

        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void List_The_Tasks()
        {

           
            string contactsUrl = AppBaseUrl + "/boards";

            driver.Navigate().GoToUrl(contactsUrl);

            var headerDone = driver.FindElement(By.XPath("/ html / body / main / div / div[3] / h1"));
            Assert.AreEqual("Done", headerDone.Text);

            var titleBoard = driver.FindElement(By.CssSelector("#task1 > tbody > tr.title > td"));
            Assert.AreEqual("Project skeleton", titleBoard.Text);
        }

        
        [Test]
        public void Find_Task_ByKeyword()
        {

            
            string contactsUrl = AppBaseUrl + "/tasks/search";

            driver.Navigate().GoToUrl(contactsUrl);


            var textboxKeyword = driver.FindElement(By.Id("keyword"));
            textboxKeyword.SendKeys("home");

            var buttonSearchKeyword = driver.FindElement(By.CssSelector("#search"));
            buttonSearchKeyword.Click();

            var taskTitle = driver.FindElement(By.CssSelector("#task2 > tbody > tr.title > td"));
            Assert.AreEqual("Home page", taskTitle.Text);

        }

        [Test]
        public void Find_Task_By_MissingKeyword()
        {

            // Arrange
            string contactsUrl = AppBaseUrl + "/tasks/search";

            driver.Navigate().GoToUrl(contactsUrl);


            var textboxKeyword = driver.FindElement(By.Id("keyword"));
            textboxKeyword.SendKeys("missing");

            var buttonSearchKeyword = driver.FindElement(By.CssSelector("#search"));
            buttonSearchKeyword.Click();

            var taskTitle = driver.FindElement(By.Id("searchResult"));
            Assert.AreEqual("No tasks found.", taskTitle.Text);

        }

        [Test]
        public void CreateContact_By_InvalidData()
        {

            // Arrange
            string contactsUrl = AppBaseUrl + "/tasks/create";

            driver.Navigate().GoToUrl(contactsUrl);


            var textboxTitle = driver.FindElement(By.Id("title"));
            textboxTitle.SendKeys("");

            var textboxDescription = driver.FindElement(By.Id("description"));
            textboxDescription.SendKeys("djsfbvgfkjb");

            var buttonCreateTask = driver.FindElement(By.Id("create"));
            buttonCreateTask.Click();

            var divErr = driver.FindElement(By.CssSelector("body > main > div"));
            Assert.AreEqual("Error: Title cannot be empty!", divErr.Text);

        }

        [Test]
        public void CreateContact_By_ValidData()
        {

            // Arrange
            string contactsUrl = AppBaseUrl + "/tasks/create";

            driver.Navigate().GoToUrl(contactsUrl);
            string title = "Add Newtests";
            string description = "UI tests";
            var textboxTitle = driver.FindElement(By.Id("title"));
            textboxTitle.SendKeys(title);

            var textboxDescription = driver.FindElement(By.Id("description"));
            textboxDescription.SendKeys(description);

            var buttonCreateTask = driver.FindElement(By.Id("create"));
            buttonCreateTask.Click();

            var pageHeading = driver.FindElement(By.XPath("/html/body/main/div/div[1]/h1"));
            Assert.AreEqual("Open", pageHeading.Text);

           var taskGrids = driver.FindElements(By.CssSelector("div.task"))[0];

           var taskBoards = taskGrids.FindElements(By.CssSelector("table.task-entry"));

           var lasttask = taskBoards[taskBoards.Count - 1];

           var lastTitle = lasttask.FindElement(By.CssSelector("tbody > tr.title > td"));

           var lastDescription = lasttask.FindElement(By.CssSelector("tbody > tr.description > td > div"));

            Assert.AreEqual(title, lastTitle.Text);
            Assert.AreEqual(description, lastDescription.Text);


        }
        [OneTimeTearDown]

        public void ShutDown()
        {
            driver.Quit();

        }
    }
   }
