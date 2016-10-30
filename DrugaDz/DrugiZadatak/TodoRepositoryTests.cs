using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Interfaces;
using Models.Repositories;
using Models;
using System.Linq;
using System.Collections.Generic;

namespace DrugiZadatak
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void GetsItemForGivenId()
        {
            Guid id = Guid.NewGuid();
            TodoItem item = new TodoItem("newItem");
            item.Id = id;
            ITodoRepository repository = new TodoRepository();
            repository.Add(item);
            Assert.AreEqual(item,repository.Get(id));
        }

        [TestMethod]
        public void GettingNullToDatabase()
        {
            Guid id = Guid.NewGuid();
            ITodoRepository repository = new TodoRepository();
            Assert.AreEqual(null, repository.Get(id));
        }

        [TestMethod]
        public void GettingInCompleteTodos()
        {
            ITodoRepository repo = new TodoRepository();
            TodoItem item1 = new TodoItem("prvi");
            TodoItem item2 = new TodoItem("drugi");
            TodoItem item3 = new TodoItem("treci");
            item2.MarkAsCompleted();
            repo.Add(item1);
            repo.Add(item2);
            repo.Add(item3);
            Assert.AreEqual(2,repo.GetActive().Count);
        }

        [TestMethod]
        public void GettingCompleteTodos()
        {
            ITodoRepository repo = new TodoRepository();
            TodoItem item1 = new TodoItem("prvi");
            TodoItem item2 = new TodoItem("drugi");
            TodoItem item3 = new TodoItem("treci");
            item2.MarkAsCompleted();
            repo.Add(item1);
            repo.Add(item2);
            repo.Add(item3);
            Assert.AreEqual(1, repo.GetCompleted().Count);
        }

        [TestMethod]
        public void GettingDescendingTodos()
        {
            ITodoRepository repo = new TodoRepository();
            TodoItem item1 = new TodoItem("prvi");
            TodoItem item2 = new TodoItem("drugi");
            TodoItem item3 = new TodoItem("treci");
            repo.Add(item2);
            repo.Add(item3);
            repo.Add(item1);
            List<TodoItem> test = new List<TodoItem>();
            test.Add(item3);
            test.Add(item2);
            test.Add(item1);
            Assert.AreEqual(test.FirstOrDefault().DateCreated, repo.GetAll().FirstOrDefault().DateCreated);
            Assert.AreEqual(test.ElementAt(1).DateCreated, repo.GetAll().ElementAt(1).DateCreated);
            Assert.AreEqual(test.ElementAt(2).DateCreated, repo.GetAll().ElementAt(2).DateCreated);
        }

        [TestMethod]
        public void GettingFilteredTodos()
        {
            ITodoRepository repo = new TodoRepository();
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            DateTime time1 = new DateTime(2016, 9, 15);
            DateTime time2 = DateTime.Now;
            TodoItem item1 = new TodoItem("cool");
            TodoItem item2 = new TodoItem("super");
            item1.Id = id1;
            item2.Id = id2;
            item1.DateCreated = time1;
            item2.DateCreated = time2;
            repo.Add(item1);
            repo.Add(item2);
            Assert.AreEqual(1, repo.GetFiltered(item => item.DateCreated.Equals(time2)).Count);
            Assert.AreEqual(1, repo.GetFiltered(item => item.Id.Equals(id2)).Count);
        }

        [TestMethod]
        public void RemovingItem()
        {
            TodoItem item1 = new TodoItem("prvi");
            TodoItem item2 = new TodoItem("drugi");
            TodoItem item3 = new TodoItem("treci");
            ITodoRepository repo = new TodoRepository();
            repo.Add(item1);
            repo.Add(item2);
            repo.Add(item3);
            repo.Remove(item2.Id);
            Assert.AreEqual(2, repo.GetAll().Count);
        }

        [TestMethod]
        public void UpdatingTodo()
        {
            TodoItem item1 = new TodoItem("prvi");
            TodoItem item2 = new TodoItem("drugi");
            TodoItem item3 = new TodoItem("treci");
            Guid id1 = Guid.NewGuid();
            item1.Id = id1;
            item2.Id = id1;
            ITodoRepository repo = new TodoRepository();
            repo.Add(item1);
            //repo.Add(item2);
            repo.Add(item3);
            repo.Update(item2);
            Assert.AreEqual(item2.DateCreated, repo.Get(id1).DateCreated);
            Assert.AreEqual(item2.IsCompleted, repo.Get(id1).IsCompleted);
        }
    }
}

