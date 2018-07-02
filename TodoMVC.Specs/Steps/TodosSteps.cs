using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TodoMVC.PageObjects;
using TodoMVC.PageObjects.Entities;
using TodoMVC.PageObjects.Pages;

namespace TodoMVC.Specs.Steps
{
    [Binding]
    public sealed class TodosSteps : TechTalk.SpecFlow.Steps
    {
        public TodosSteps(TodosDriver driver)
        {
            todosDriver = driver;
        }

        [StepArgumentTransformation]
        public TaskRow TransformTaskRow(string taskName)
        {
            return todosPage.FindTask(taskName);
        }

        private TodosDriver todosDriver;
        private TodosPage todosPage;

        [Given(@"I am in the todos page")]
        public void GivenIAmInTheTodosPage() => todosPage = todosDriver.GotoTodosPage();

        [Given(@"there is a task ""(.*)"" in the list")]
        [When(@"I add new task ""(.*)""")]
        public void WhenIAddNewTask(string taskName) => todosPage.AddTask(taskName);

        [Given(@"there is a completed task ""(.*)"" in the list")]
        public void GivenThereIsACompletedTaskInTheList(string taskName)
        {
            todosPage.AddTask(taskName);
            todosPage.FindTask(taskName).MarkCompleted();
        }

        [When(@"I click on Clear completed tasks")]
        public void WhenIClickOnClearCompletedTasks() => todosPage.ClearAllCompletedTasks();
        
        [When(@"I make the task ""(.*)"" completed")]
        public void WhenIMakeTheTaskCompleted(TaskRow taskRow) => taskRow.MarkCompleted();

        [When(@"I make the task ""(.*)"" active")]
        public void WhenIMakeTheTaskActive(TaskRow taskRow) => taskRow.MarkActive();


        [Then(@"the task ""(.*)"" will appear as completed")]
        public void ThenTheTaskWillAppearAsCompleted(TaskRow taskRow) => Assert.IsTrue(taskRow.IsCompleted);


        [Then(@"the task ""(.*)"" will be added to the list")]
        public void ThenTheTaskWillBeAddedToTheList(string taskName)
        {
            var task = todosPage.TodosList.FirstOrDefault(a => a.TaskText == taskName);
            Assert.That(task, Is.Not.Null);
            Assert.That(task.TaskText, Is.EqualTo(taskName));
        }

        [When(@"I delete the task ""(.*)""")]
        public void WhenIDeleteTheTask(string taskName) => todosPage.FindTask(taskName).DeleteTask();

        [Then(@"the task list will be empty")]
        public void ThenTheTaskListWillBeEmpty() => CollectionAssert.IsEmpty(todosPage.TodosList);

        [When(@"I edit the task ""(.*)"" to be ""(.*)""")]
        public void WhenIEditTheTaskToBe(string oldName, string newName)  => todosPage.FindTask(oldName).EditTask(newName);


        [Then(@"the task list will be")]
        public void ThenTheTaskListWillBe(Table table)
        {
            var actual =
                todosPage.TodosList.Select(a => new Task()
                {
                    TaskName = a.TaskText,
                    IsCompleted = a.IsCompleted
                });
            table.CompareToSet(actual);
        }

        [When(@"I switch to view Active")]
        public void WhenISwitchToViewActive() => todosPage.SelectActiveTasksView();

        [When(@"I switch to All view")]
        public void WhenISwitchToAllView() => todosPage.SelectViewAll();

        [When(@"I switch to Active view")]
        public void WhenISwitchToActiveView() => todosPage.SelectActiveTasksView();

        [When(@"I switch to Completed view")]
        public void WhenISwitchToCompletedView() => todosPage.SelectCompletedTasksView();

    }
}