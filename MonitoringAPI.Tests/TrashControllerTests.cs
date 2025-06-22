using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class TrashControllerTests
{
    private readonly Mock<ITrashRepository> _mockRepo;
    private readonly TrashController _controller;

    public TrashControllerTests()
    {
        _mockRepo = new Mock<ITrashRepository>();
        _controller = new TrashController(_mockRepo.Object);
    }


    [TestMethod]
    public async Task GetFiltered_ByDate_ReturnsFilteredResults()
    {
        var testDate = new DateTime(2023, 1, 1);
        var testData = new List<Trash> { new Trash { DateCollected = testDate } };

        _mockRepo.Setup(repo => repo.GetFilteredAsync(testDate, null, null, null, null))
                .ReturnsAsync(testData);

        var result = await _controller.GetFiltered(date: testDate) as OkObjectResult;
        var returnedData = result.Value as List<Trash>;

        Assert.IsNotNull(result);
        Assert.AreEqual(1, returnedData.Count);
        Assert.AreEqual(testDate, returnedData[0].DateCollected);
    }

    [TestMethod]
    public async Task GetFiltered_ByType_ReturnsMatchingTypes()
    {
        var testType = "Plastic";
        var testData = new List<Trash>
       {
           new Trash { TypeAfval = testType }
       };

        _mockRepo.Setup(repo => repo.GetFilteredAsync(null, null, null, testType, null))
                .ReturnsAsync(testData);

        var result = await _controller.GetFiltered(type: testType);

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var data = okResult.Value as List<Trash>;
        Assert.IsNotNull(data);
        Assert.AreEqual(1, data.Count);
        Assert.AreEqual(testType, data[0].TypeAfval);
    }

    [TestMethod]
    public async Task GetById_ExistingTrash_ReturnsTrash()
    {
        var testTrash = new Trash { Id = 1 };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(testTrash);

        var result = await _controller.GetById(1);

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(testTrash.Id, (okResult.Value as Trash).Id);
    }
}