﻿using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Item;
using Catalog.Domain.Responses;

namespace Catalog.Domain.Mappers
{
    public class ItemMapper : IItemMapper
    {
        private readonly IArtistMapper _artistMapper;
        private readonly IGenreMapper _genreMapper;

        public ItemMapper(IArtistMapper artistMapper, IGenreMapper genreMapper)
        {
            _artistMapper = artistMapper;
            _genreMapper = genreMapper;
        }

        public Item Map(AddItemRequest request)
        {
            if (request == default) return default;

            var item = new Item
            {
                Name = request.Name,
                Description = request.Description,
                LabelName = request.LabelName,
                PictureUri = request.PictureUri,
                ReleaseDate = request.ReleaseDate,
                Format = request.Format,
                AvailableStock = request.AvailableStock,
                GenreId = request.GenreId,
                ArtistId = request.ArtistId,
            };

            if (request.Price != default)
            {
                item.Price = new Price
                {
                    Currency = request.Price.Currency,
                    Amount = request.Price.Amount
                };
            }

            return item;
        }

        public Item Map(EditItemRequest request)
        {
            if (request == default) return default;

            var item = new Item
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                LabelName = request.LabelName,
                PictureUri = request.PictureUri,
                ReleaseDate = request.ReleaseDate,
                Format = request.Format,
                AvailableStock = request.AvailableStock,
                GenreId = request.GenreId,
                ArtistId = request.ArtistId,               
            };

            if (request.Price != default)
            {
                item.Price = new Price
                {
                    Currency = request.Price.Currency,
                    Amount = request.Price.Amount
                };
            }

            return item;
        }

        public ItemResponse Map(Item item)
        {
            if (item == default) return default;

            var result = new ItemResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                LabelName = item.LabelName,
                PictureUri = item.PictureUri,
                ReleaseDate = item.ReleaseDate,
                Format = item.Format,
                AvailableStock = item.AvailableStock,
                GenreId = item.GenreId,
                Genre = _genreMapper.Map(item.Genre),
                ArtistId = item.ArtistId,
                Artist = _artistMapper.Map(item.Artist)
            };

            if (item.Price != default)
            {
                result.Price = new PriceResponse
                {
                    Currency = item.Price.Currency,
                    Amount = item.Price.Amount
                };
            }

            return result;
        }
    }
}
