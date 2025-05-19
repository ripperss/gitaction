using TodoApi.Models;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests;

public class TodoServiceTests
{
    private readonly ITodoService _todoService;

    public TodoServiceTests()
    {
        _todoService = new TodoService();
    }

    [Fact]
    public void Create_ShouldCreateNewTodo()
    {
        // Arrange
        var todo = new Todo
        {
            Title = "Test Todo",
            Description = "Test Description"
        };

        // Act
        var createdTodo = _todoService.Create(todo);

        // Assert
        Assert.NotNull(createdTodo);
        Assert.Equal(1, createdTodo.Id);
        Assert.Equal(todo.Title, createdTodo.Title);
        Assert.Equal(todo.Description, createdTodo.Description);
        Assert.False(createdTodo.IsCompleted);
    }

    [Fact]
    public void GetById_ShouldReturnTodo_WhenExists()
    {
        // Arrange
        var todo = new Todo { Title = "Test Todo" };
        var createdTodo = _todoService.Create(todo);

        // Act
        var retrievedTodo = _todoService.GetById(createdTodo.Id);

        // Assert
        Assert.NotNull(retrievedTodo);
        Assert.Equal(createdTodo.Id, retrievedTodo.Id);
        Assert.Equal(createdTodo.Title, retrievedTodo.Title);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var todo = _todoService.GetById(999);

        // Assert
        Assert.Null(todo);
    }

    [Fact]
    public void Update_ShouldUpdateTodo_WhenExists()
    {
        // Arrange
        var todo = new Todo { Title = "Original Title" };
        var createdTodo = _todoService.Create(todo);
        var updatedTodo = new Todo
        {
            Title = "Updated Title",
            Description = "Updated Description",
            IsCompleted = true
        };

        // Act
        var result = _todoService.Update(createdTodo.Id, updatedTodo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedTodo.Title, result.Title);
        Assert.Equal(updatedTodo.Description, result.Description);
        Assert.Equal(updatedTodo.IsCompleted, result.IsCompleted);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenTodoExists()
    {
        // Arrange
        var todo = new Todo { Title = "Test Todo" };
        var createdTodo = _todoService.Create(todo);

        // Act
        var result = _todoService.Delete(createdTodo.Id);

        // Assert
        Assert.True(result);
        Assert.Null(_todoService.GetById(createdTodo.Id));
    }
} 