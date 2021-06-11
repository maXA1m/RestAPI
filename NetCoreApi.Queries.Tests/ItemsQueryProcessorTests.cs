using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NetCoreApi.Data.Access.DAL;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Items;
using NetCoreApi.Queries.Common;
using NetCoreApi.Security;
using Xunit;

namespace NetCoreApi.Queries.Tests
{
    public class ItemsQueryProcessorTests
    {
        private Mock<IUnitOfWork> _uow;
        private List<Item> _items;
        private IQueryProcessor<Item, CreateItemModel, UpdateItemModel> _query;
        private Random _random;
        private User _currentUser;
        private Mock<ISecurityContext> _securityContext;

        public ItemsQueryProcessorTests()
        {
            _random = new Random();
            _uow = new Mock<IUnitOfWork>();

            _items = new List<Item>();
            _uow.Setup(x => x.Query<Item>()).Returns(() => _items.AsQueryable());

            _currentUser = new User { Id = _random.Next() };
            _securityContext = new Mock<ISecurityContext>(MockBehavior.Strict);
            _securityContext.Setup(x => x.User).Returns(_currentUser);
            _securityContext.Setup(x => x.IsAdministrator).Returns(false);

            _query = new ItemsQueryProcessor(_uow.Object);
        }

        [Fact]
        public void GetShouldReturnAll()
        {
            _items.Add(new Item());

            var result = _query.Get().ToList();
            result.Count.Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnById()
        {
            var item = new Item { Id = _random.Next() };
            _items.Add(item);

            var result = _query.Get(item.Id);
            result.Should().Be(item);
        }

        [Fact]
        public void GetShouldThrowExceptionIfItemIsNotFoundById()
        {
            var item = new Item { Id = _random.Next() };
            _items.Add(item);

            Action get = () =>
            {
                _query.Get(_random.Next());
            };

            get.Should().Throw<Exception>();
        }

        [Fact]
        public async Task CreateShouldSaveNew()
        {
            var model = new CreateItemModel
            {
                Name = _random.Next().ToString(),
                AvailableQuantity = _random.Next(),
                CreatedAt = DateTime.Now
            };

            var result = await _query.Create(model);

            result.Name.Should().Be(model.Name);
            result.AvailableQuantity.Should().Be(model.AvailableQuantity);

            _uow.Verify(x => x.Add(result));
            _uow.Verify(x => x.CommitAsync());
        }

        [Fact]
        public void UpdateShoudlThrowExceptionIfItemIsNotFound()
        {
            Action create = () =>
            {
                var result = _query.Update(_random.Next(), new UpdateItemModel()).Result;
            };

            create.Should().Throw<Exception>();
        }

        [Fact]
        public void DeleteShoudlThrowExceptionIfItemIsNotFound()
        {
            Action execute = () =>
            {
                _query.Delete(_random.Next()).Wait();
            };

            execute.Should().Throw<Exception>();
        }
    }
}
