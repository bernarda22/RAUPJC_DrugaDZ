using System;
using Models.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Models
{
    namespace Repositories
    {
        /// <summary >
        /// Class that encapsulates all the logic for accessing TodoTtems .
        /// </ summary >
        public class TodoRepository : ITodoRepository
        {
            /// <summary >
            /// Repository does not fetch todoItems from the actual database ,
            /// it uses in memory storage for this excersise .
            /// </ summary >
            private readonly List<TodoItem> _inMemoryTodoDatabase;

            public TodoRepository(List<TodoItem> initialDbState = null)
            {
                if (initialDbState != null)
                {
                    _inMemoryTodoDatabase = initialDbState;
                }
                else
                {
                    _inMemoryTodoDatabase = new List<TodoItem>();
                }
                // Shorter way to write this in C# using ?? operator :
                // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
                // x ?? y -> if x is not null , expression returns x. Else y.
            }

            public void Add(TodoItem todoItem)
            {                
                if(todoItem != null)
                {
                    TodoItem existingItem = Get(todoItem.Id);
                    if(existingItem == null)
                    {
                        _inMemoryTodoDatabase.Add(todoItem);
                    }
                    else
                    {
                        throw new DuplicateTodoItemException("duplicate id: {id}",todoItem.Id);
                    }                  
                }
                else
                {
                    throw new ArgumentNullException();
                }              
            }

            public TodoItem Get(Guid todoId)
            {
                List<TodoItem> items = _inMemoryTodoDatabase.Where(item => item.Id.Equals(todoId)).ToList();               
                return items.FirstOrDefault();
            }

            public List<TodoItem> GetActive()
            {
                return _inMemoryTodoDatabase.Where(item => item.IsCompleted.Equals(false)).ToList();
            }

            public List<TodoItem> GetAll()
            {
                return _inMemoryTodoDatabase.OrderByDescending(item => item.DateCreated).ToList();
            }

            public List<TodoItem> GetCompleted()
            {
                return _inMemoryTodoDatabase.Where(item => item.IsCompleted.Equals(true)).ToList();
            }

            public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
            {
                return _inMemoryTodoDatabase.Where(filterFunction).ToList();
            }

            public bool MarkAsCompleted(Guid todoId)
            {
                TodoItem item = Get(todoId);
                if (item != null)
                {
                    item.MarkAsCompleted();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool Remove(Guid todoId)
            {
                TodoItem item = Get(todoId);
                if (item != null)
                {
                    _inMemoryTodoDatabase.Remove(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Update(TodoItem todoItem)
            {
                TodoItem existingItem = Get(todoItem.Id);
                if(existingItem != null)
                {
                    _inMemoryTodoDatabase.Remove(existingItem);
                }
                _inMemoryTodoDatabase.Add(todoItem); 
            }
        }
    }

}



