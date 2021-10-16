using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using ItemsArchiveService.Data;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ItemsArchiveService.Repository
{
    public class ItemRepository : IItemRepository
    {

        private readonly IDbContext _context;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        public ItemRepository(IDbContext context, IFileRepository fileRepository, IMapper mapper, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        public async Task AddItem(ItemDTO item, string userId)
        {
            var _item = _mapper.Map<Item>(item);
            _item.ItemImages = new List<ItemImage>();
            foreach (var path in item.ImagePath)
            {
                _item.ItemImages.Add(new ItemImage() { Path = path, IsDeleted = false });
            }
            _item.UserId = userId;
            await _context.Items.InsertOneAsync(_item);
        }

        public async Task ApproveAndDisapproveItem(string id)
        {

            var item = await _context.Items.Find(x => x.Id == id).FirstAsync();

            var filter = Builders<Item>.Filter.Eq(x => x.Id, id);

            var update = Builders<Item>.Update
                                       .Set(d => d.IsApproved, !item.IsApproved);

            await _context.Items.UpdateOneAsync(filter, update);

            // foreach (var item in items)
            // {
            //     var filter = Builders<Item>.Filter.Eq(x => x.Code, item.Code);
            //     var update = Builders<Item>.Update
            //                           .Set(d => d.IsApproved, item.IsApproved);
            //     await _context.Items.UpdateOneAsync(filter, update);
            // }
        }
        public async Task<List<GetItemByCodeResponseDTO>> GetItemsByCode(string code)
        {
            var projectOptions = new BsonDocument { { "code" , 1 },
                                                     { "brand", 1 },
                                                     { "name", 1 },
                                                     { "s_price", 1 },
                                                     { "desc", 1 },
                                                     { "img",  new BsonDocument("$filter" ,new BsonDocument{
                                                         {"input", "$img"},
                                                         {"as" ,  "item"},
                                                         {"cond", new BsonDocument("$eq", new BsonArray{"$$item.isDeleted",false})}
                                                     })}};


            var pipeline = new List<BsonDocument>{
                    new BsonDocument { { "$match", new BsonDocument("$and", new BsonArray { new BsonDocument ("code", code ),
                                                                            new BsonDocument("isapproved", false )
                                                                        }) }},
                    new BsonDocument("$project", projectOptions)

                };

            var items = new List<GetItemByCodeResponseDTO>();

            using (var cursor = await _context.Items.AggregateAsync<BsonDocument>(pipeline))
            {

                while (await cursor.MoveNextAsync())
                {

                    foreach (var doc in cursor.Current)
                    {
                        items.Add(BsonSerializer.Deserialize<GetItemByCodeResponseDTO>(doc));
                    }
                }
            }
            return items;
        }

        public async Task<(IEnumerable<Item>, long)> GetItems(DateTime? date, bool? status, int pageSize, int page)
        {

            var builder = Builders<Item>.Filter;
            FilterDefinition<Item> filter = builder.Empty;

            if (date.HasValue)
            {
                var endDate = date.Value.AddDays(1);
                filter = builder.Gt(x => x.CreatedDate, date.Value) & builder.Lt(x => x.CreatedDate, endDate);
            }

            if (status.HasValue)
            {
                filter &= builder.Eq(x => x.IsApproved, status.Value);
            }

            var filtereditems = _context.Items.Find(filter);

            var totalRecord = await filtereditems.CountDocumentsAsync();
            var result = await filtereditems
            .SortByDescending(x => x.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

            return (result, totalRecord);
        }

        public async Task DeleteAndUndeleteImage(string id, string path)
        {

            var isDeleted = _context.Items.Find(x => x.Id == id).First()
            .ItemImages.First(x => x.Path == path).IsDeleted;

            var filterBuilder = Builders<Item>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, id) &
                filterBuilder.ElemMatch(i => i.ItemImages, el => el.Path == path);

            var update = Builders<Item>.Update
                                      .Set(d => d.ItemImages[-1].IsDeleted, !isDeleted);
            await _context.Items.UpdateOneAsync(filter, update);
        }
    }
}