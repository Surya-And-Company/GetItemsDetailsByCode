using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using ItemsArchiveService.Data;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ItemsArchiveService.Repository
{
    public class ItemRepository : IItemRepository
    {

        private readonly IDbContext _context;
        private  readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper   _mapper;
        public ItemRepository(IDbContext context,IFileRepository fileRepository, IMapper mapper, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task AddItem(ItemDTO item)
        {
            var _item = _mapper.Map<Item>(item);
            if (item.Images != null)
            {
                var time = DateTime.Now.ToString();
                var path = Path.Combine(_configuration.GetValue<string>($"ResizeImage:ItemImage:FilePath"), time);
                await _fileRepository.UploadFileAsync(item.Images, path);

                foreach (var image in item.Images)
                {
                    _item.ImagePath.Add(Path.Combine(time, image.FileName));
                }
                await _context.Items.InsertOneAsync(_item);
            }
        }

        public async Task ApproveItems(IEnumerable<ApproveItemDTO> items)
        {
            foreach (var item in items)
            {
                var filter = Builders<Item>.Filter.Eq(x => x.Code, item.Code);
                var update = Builders<Item>.Update
                                      .Set(d => d.IsApproved, item.IsApproved);
                await _context.Items.UpdateOneAsync(filter, update);
            }
        }
        public async Task<Item> GetItem(string code)
        {
            return await _context.Items.Find(x => x.Code == code).FirstOrDefaultAsync();
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

            var filteredLogs = _context.Items.Find(filter);

            var totalRecord = await filteredLogs.CountDocumentsAsync();
            var result = await filteredLogs
            .SortByDescending(x => x.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

            return (result, totalRecord);
        }

    }
}