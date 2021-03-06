﻿using Catalog.Domain.Entities;
using Catalog.Fixtures;
using Catalog.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Infrastructure.Tests
{
    public class ArtistRepositoryTests : IClassFixture<CatalogContextFactory>
    {
        private readonly CatalogContextFactory _factory;

        public ArtistRepositoryTests(CatalogContextFactory factory)
        {
            _factory = factory;
        }

        [Theory]
        [LoadData("artist")]
        public async Task Should_return_record_by_id(Artist artist)
        {
            var sut = new ArtistRepository(_factory.ContextInstance);
            var result = await sut.GetAsync(artist.ArtistId);

            result.ArtistId.ShouldBe(artist.ArtistId);
            result.ArtistName.ShouldBe(artist.ArtistName);
        }

        [Theory]
        [LoadData("artist")]
        public async Task Should_add_new_item(Artist artist)
        {
            artist.ArtistId = Guid.NewGuid();
            var sut = new ArtistRepository(_factory.ContextInstance);

            sut.Add(artist);
            await sut.UnitOfWork.SaveEntitiesAsync();

            _factory.ContextInstance.Artists
                .FirstOrDefault(x => x.ArtistId == artist.ArtistId)
                .ShouldNotBeNull();
        }

        [Theory]
        [LoadData("artist")]
        public async Task Should_get_data(Artist artist)
        {
            var sut = new ArtistRepository(_factory.ContextInstance);

            var response = await sut.GetAsync();

            response.FirstOrDefault(a => a.ArtistId == artist.ArtistId).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_returns_null_with_id_not_present()
        {
            var sut = new ArtistRepository(_factory.ContextInstance);
            var result = await sut.GetAsync(Guid.NewGuid());

            result.ShouldBeNull();
        }
    }
}
