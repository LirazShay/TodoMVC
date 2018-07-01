using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace TodoMVC.PageObjects
{
    public class TodosPage
    {
        private readonly IWebDriver _driver;

        public TodosPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddTask(string newTaskName)
        {
            var addNewField = _driver.FindElement(By.ClassName("new-todo"));
            addNewField.Click();
            addNewField.SendKeys(newTaskName + Keys.Enter);
        }

        public IList<TaskRow> TodosList
        {
            get
            {
                var rows = _driver.FindElements(By.CssSelector("#todo-list li[ng-repeat*='todo in todos']"));
                return rows.Select(a => new TaskRow(a)).ToList();
            }
        }

        public TaskRow FindTask(string task)
        {
            return TodosList.First(a => a.TaskText == task);
        }

        public void ClearAllCompletedTasks()
        {
            _driver.FindElement(By.CssSelector("#clear-completed")).Click();
        }

        public void SelectViewAll()
        {
            _driver.FindElement(By.XPath("//ul[@id='filters']//a[text()='All']")).Click();
        }

        public void SelectActiveTasksView()
        {
            _driver.FindElement(By.XPath("//ul[@id='filters']//a[text()='Active']")).Click();
        }

        public void SelectCompletedTasksView()
        {
            _driver.FindElement(By.XPath("//ul[@id='filters']//a[text()='Completed']")).Click();
        }
    }
}