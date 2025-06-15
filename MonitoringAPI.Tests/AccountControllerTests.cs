using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

[TestClass]
public class AccountControllerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        var contextAccessor = new Mock<IHttpContextAccessor>();
        var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
            _mockUserManager.Object,
            contextAccessor.Object,
            userPrincipalFactory.Object,
            null, null, null, null);

        _controller = new AccountController(
            _mockUserManager.Object,
            _mockSignInManager.Object,
            null);
    }

    [TestMethod]
    public async Task Register_WithValidData_ReturnsSuccess()
    {
        var dto = new RegisterDto { Email = "test@test.com", Password = "ValidPass123!" };
        _mockUserManager.Setup(x => x.FindByEmailAsync(dto.Email))
                       .ReturnsAsync((ApplicationUser)null);
        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                       .ReturnsAsync(IdentityResult.Success);

        var result = await _controller.Register(dto);

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
}