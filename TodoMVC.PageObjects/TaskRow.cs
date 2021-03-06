﻿using System;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TodoMVC.PageObjects
{
  

    public class TaskRow
    {
        private readonly IWebDriver driver;
        private readonly IWebElement rowElement;
        private IWebElement CompletedCheckbox => rowElement.FindElement(By.CssSelector("input[ng-model='todo.completed']"));


        public TaskRow(IWebElement rowElement,IWebDriver driver)
        {
            this.driver = driver;
            this.rowElement = rowElement;
        }

        public string TaskText => rowElement.Text;
        public bool IsCompleted => rowElement.GetAttribute("class").Contains("completed");

        public void EditTask(string newName)
        {
            //double click by Actions library
            var editable = rowElement.FindElement(By.CssSelector("[ng-dblclick='editTodo(todo)']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(editable).DoubleClick().Build().Perform();
            //select all text, delete it and type new text and press enter
            var input = rowElement.FindElement(By.CssSelector("form input.edit"));
            actions.MoveToElement(input).Build().Perform();
            actions.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete).Build().Perform();
            input.SendKeys("");
            input.SendKeys(newName+ Keys.Enter);
        }


        public void DeleteTask()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(rowElement)
                .Build()
                .Perform();
            rowElement.FindElement(By.CssSelector("button[ng-click='removeTodo(todo)']"))
                .Click();
        }

        public void MarkCompleted()
        {
            CompletedCheckbox.Click();
        }

        public void MarkActive()
        {
            CompletedCheckbox.Click();
        }
    }
}