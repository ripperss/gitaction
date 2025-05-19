using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    IEnumerable<Todo> GetAll();
    Todo? GetById(int id);
    Todo Create(Todo todo);
    Todo? Update(int id, Todo todo);
    bool Delete(int id);
}

public class TodoService : ITodoService
{
    private readonly List<Todo> _todos = new();
    private int _nextId = 1;

    public IEnumerable<Todo> GetAll()
    {
        return _todos;
    }

    public Todo? GetById(int id)
    {
        return _todos.FirstOrDefault(t => t.Id == id);
    }

    public Todo Create(Todo todo)
    {
        todo.Id = _nextId++;
        todo.CreatedAt = DateTime.UtcNow;
        _todos.Add(todo);
        return todo;
    }

    public Todo? Update(int id, Todo todo)
    {
        var existingTodo = GetById(id);
        if (existingTodo == null)
            return null;

        existingTodo.Title = todo.Title;
        existingTodo.Description = todo.Description;
        existingTodo.IsCompleted = todo.IsCompleted;

        return existingTodo;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo == null)
            return false;

        _todos.Remove(todo);
        return true;
    }
} 