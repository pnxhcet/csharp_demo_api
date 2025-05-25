using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csharp_demo_api.Application.Services;
using csharp_demo_api.Domain.Entities;
using csharp_demo_api.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace csharp_demo_api.Tests.Application.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockRepository;
        private readonly TaskService _sut;

        public TaskServiceTests()
        {
            _mockRepository = new Mock<ITaskRepository>();
            _sut = new TaskService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            // Arrange
            var expectedTasks = new List<TaskEntity>
            {
                new TaskEntity { Id = Guid.NewGuid(), Title = "Task 1" },
                new TaskEntity { Id = Guid.NewGuid(), Title = "Task 2" }
            };
            _mockRepository.Setup(r => r.GetAllAsync())
                           .ReturnsAsync(expectedTasks);

            // Act
            var result = await _sut.GetAllTasksAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedTasks);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetTaskByIdAsync_WithExistingId_ShouldReturnTask()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var expectedTask = new TaskEntity { Id = taskId, Title = "Test Task" };
            _mockRepository.Setup(r => r.GetByIdAsync(taskId))
                           .ReturnsAsync(expectedTask);

            // Act
            var result = await _sut.GetTaskByIdAsync(taskId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedTask);
            _mockRepository.Verify(r => r.GetByIdAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task GetTaskByIdAsync_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetByIdAsync(taskId))
                           .ReturnsAsync((TaskEntity?)null);

            // Act
            var result = await _sut.GetTaskByIdAsync(taskId);

            // Assert
            result.Should().BeNull();
            _mockRepository.Verify(r => r.GetByIdAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task AddTaskAsync_ShouldGenerateIdAndTimestamp_AndCallRepository()
        {
            // Arrange
            var task = new TaskEntity { Title = "New Task" };
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<TaskEntity>()))
                           .Returns(Task.CompletedTask);
            
            // Store the original ID to verify it changes
            var originalId = task.Id;
            
            // Act
            await _sut.AddTaskAsync(task);

            // Assert
            task.Id.Should().NotBe(Guid.Empty);
            task.Id.Should().NotBe(originalId);
            task.CreatedAt.Should().NotBe(default);
            // Less strict time assertion to avoid timing issues
            task.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
            _mockRepository.Verify(r => r.AddAsync(It.Is<TaskEntity>(t => t.Id != Guid.Empty)), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldCallRepository()
        {
            // Arrange
            var task = new TaskEntity 
            { 
                Id = Guid.NewGuid(), 
                Title = "Updated Task",
                Description = "Updated Description"
            };
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<TaskEntity>()))
                           .Returns(Task.CompletedTask);

            // Act
            await _sut.UpdateTaskAsync(task);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.Is<TaskEntity>(t => 
                t.Id == task.Id && 
                t.Title == task.Title && 
                t.Description == task.Description)), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldCallRepository()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _mockRepository.Setup(r => r.DeleteAsync(taskId))
                           .Returns(Task.CompletedTask);

            // Act
            await _sut.DeleteTaskAsync(taskId);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(taskId), Times.Once);
        }
    }
}