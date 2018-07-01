using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TodoMVC.PageObjects
{
    public class TodosDriver : IDisposable
    {
        public TodosDriver()
        {
            WebDriver = new ChromeDriver();
        }

        public IWebDriver WebDriver { get; set; }

        public void Dispose()
        {
            WebDriver.Quit();
        }

        public TodosPage GotoTodosPage()
        {
            WebDriver.Navigate().GoToUrl("http://todomvc.com/examples/angularjs/#/");
            return new TodosPage(WebDriver);
        }
    }
}