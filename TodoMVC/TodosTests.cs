﻿using System.Linq;
using NUnit.Framework;
using TodoMVC.PageObjects;

namespace TodoMVC.Tests
{
    [Parallelizable(ParallelScope.Children)]
    [TestFixture]
    public class TodosTests
    {
        [Test]
        public void AddTask()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();

                todosPage.AddTask("New task");

                var taskRow = todosPage.TodosList.Last();
                Assert.AreEqual("New task", taskRow.TaskText);
            }
        }

        [Test]
        public void EditTask()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("New Task");

                var newTask = todosPage.TodosList.First(a=>a.TaskText== "New Task");
                newTask.EditTask("New Name");

                Assert.AreEqual("New task", newTask.TaskText);
            }
        }

        [Test]
        public void DeleteTask()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("New Task");

                var taskRow = todosPage.TodosList.First(a=>a.TaskText== "New Task");
                taskRow.DeleteTask();

                Assert.AreEqual(0, todosPage.TodosList.Count);
            }
        }

        [Test]
        public void MarkTaskCompleted()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("New Task");

                var task = todosPage.FindTask("New Task");
                task.MarkCompleted();

                Assert.IsTrue(task.IsCompleted);
            }
        }

        [Test]
        public void MarkTaskActive()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("New Task");
                var task = todosPage.FindTask("New Task");
                task.MarkCompleted();

                task.MarkActive();

                Assert.IsFalse(task.IsCompleted);
            }
        }

        [Test]
        public void ClearCompletedTasks()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("New Task");
                todosPage.AddTask("New Task2");
                todosPage.FindTask("New Task").MarkCompleted();
                todosPage.FindTask("New Task2").MarkCompleted();

                todosPage.ClearAllCompletedTasks();

                Assert.AreEqual(0,todosPage.TodosList.Count);
            }
        }

        [Test]
        public void FilterAllTasksWillShowCompletedAndActiveTasks()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("Active");
                todosPage.AddTask("Completed");
                todosPage.FindTask("Completed").MarkCompleted();

                todosPage.SelectViewAll();
                var allTasks = todosPage.TodosList;

                Assert.AreEqual(2, todosPage.TodosList.Count);
                Assert.IsTrue(allTasks.Any(a => a.TaskText == "Completed"));
                Assert.IsTrue(allTasks.Any(a => a.TaskText == "Active"));
            }
        }

        [Test]
        public void FilterActiveTasksWillShowOnlyActiveTasks()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("Active");
                todosPage.AddTask("Completed");
                todosPage.FindTask("Completed").MarkCompleted();

                todosPage.SelectActiveTasksView();
                var allTasks = todosPage.TodosList;

                Assert.AreEqual(1, todosPage.TodosList.Count);
                Assert.IsTrue(allTasks.Any(a => a.TaskText == "Active"));
            }
        }


        [Test]
        public void FilterCompletedTasksWillShowOnlyCompletedTasks()
        {
            using (var todosDriver = new TodosDriver())
            {
                var todosPage = todosDriver.GotoTodosPage();
                todosPage.AddTask("Active");
                todosPage.AddTask("Completed");
                todosPage.FindTask("Completed").MarkCompleted();

                todosPage.SelectCompletedTasksView();
                var allTasks = todosPage.TodosList;

                Assert.AreEqual(1, todosPage.TodosList.Count);
                Assert.IsTrue(allTasks.Any(a => a.TaskText == "Completed"));
            }
        }

    }
}
