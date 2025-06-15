using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

[TestClass]
public class CameraControllerTests
{
    private readonly Mock<ICameraRepository> _mockRepo;
    private readonly CameraController _controller;

    public CameraControllerTests()
    {
        _mockRepo = new Mock<ICameraRepository>();
        _controller = new CameraController(_mockRepo.Object);
    }

    [TestMethod]
    public async Task CreateCamera_ValidData_ReturnsCreated()
    {
        var camera = new Camera { Id = 1, Naam = "Test Camera" };
        _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Camera>())).ReturnsAsync(camera);

        var result = await _controller.Create(camera);

        Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        var createdAtActionResult = result as CreatedAtActionResult;
        Assert.IsNotNull(createdAtActionResult);
        Assert.AreEqual(201, createdAtActionResult.StatusCode);
        Assert.AreEqual(camera.Id, (createdAtActionResult.Value as Camera).Id);
    }

    [TestMethod]
    public async Task GetCamera_ExistingId_ReturnsCamera()
    {
        var camera = new Camera { Id = 1, Naam = "Test Camera" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(camera);

        var result = await _controller.GetById(1) as OkObjectResult;
        var resultCamera = result.Value as Camera;

        Assert.IsNotNull(result);
        Assert.AreEqual(camera.Id, resultCamera.Id);
    }

    [TestMethod]
    public async Task DeleteCamera_ExistingId_ReturnsNoContent()
    {
        _mockRepo.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task GetCamera_NonExistingId_ReturnsNotFound()
    {
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Camera)null);

        var result = await _controller.GetById(1);

        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

}