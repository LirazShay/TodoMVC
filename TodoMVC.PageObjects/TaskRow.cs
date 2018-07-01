using System;
using OpenQA.Selenium;

namespace TodoMVC.PageObjects
{
    public class TaskRow
    {
        private readonly IWebElement _taskRowElement;

        public TaskRow(IWebElement taskRowElement)
        {
            _taskRowElement = taskRowElement;
        }

        public string TaskText => _taskRowElement.Text;
        public bool IsCompleted => _taskRowElement.GetAttribute("class").Contains("completed");

        public void EditTask(string newName)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask()
        {
            throw new NotImplementedException();
        }

        public void MarkCompleted()
        {
            throw new NotImplementedException();
        }

        public void MarkActive()
        {
            throw new NotImplementedException();
        }
    }
}