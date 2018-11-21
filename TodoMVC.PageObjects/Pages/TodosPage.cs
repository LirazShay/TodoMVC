using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TodoMVC.PageObjects.Pages
{
    public class TodosPage
    {
        private readonly IWebDriver driver;

        private IWebElement NewTaskField => driver.FindElement(By.CssSelector(".new-todo"));

        public TodosPage(IWebDriver driver)
        {
            this.driver = driver;
            WaitForPageLoad();
        }

        public void AddTask(string newTaskName)
        {
            NewTaskField.Click();
            NewTaskField.SendKeys(newTaskName + Keys.Enter);
        }

        public IList<TaskRow> TodosList
        {
            get
            {
                var rows = driver.FindElements(By.CssSelector(".todo-list li[ng-repeat*='todo in todos']"));
                return rows.Select(a => new TaskRow(a,driver)).ToList();
            }
        }

        public TaskRow FindTask(string task)
        {
            return TodosList.First(a => a.TaskText == task);
        }

        public void ClearAllCompletedTasks()
        {
            driver.FindElement(By.CssSelector(".clear-completed")).Click();
        }

        public void SelectViewAll()
        {
            driver.FindElement(By.XPath("//ul[contains(@class,'filters')]//a[text()='All']")).Click();
        }

        public void SelectActiveTasksView()
        {
            driver.FindElement(By.CssSelector("a[href=\"#/active\"]")).Click();
        }

        public void SelectCompletedTasksView()
        {
            driver.FindElement(By.CssSelector("a[href=\"#/completed\"]")).Click();
        }

        private void WaitForPageLoad()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(a => NewTaskField.Displayed);
        }

    }
}