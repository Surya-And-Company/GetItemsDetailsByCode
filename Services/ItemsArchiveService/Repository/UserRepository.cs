using System.IO;
using System;
using System.Threading.Tasks;
using AutoMapper;
using ItemsArchiveService.Data;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;
using ItemsArchiveService.Utility;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Microsoft.Extensions.FileProviders;

namespace ItemsArchiveService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _hasher;
        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;
        private readonly string _wwwPath;

        public UserRepository(IDbContext context, IMapper mapper, IPasswordHasher hasher, IConfiguration configuration, IFileRepository fileRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _wwwPath = new PhysicalFileProvider(Directory.GetCurrentDirectory()).Root;
        }
        public async Task AddUserAsync(CreateUserDTO user)
        {
            var _user = _mapper.Map<User>(user);
            _user.Password = _hasher.Hash(_user.Password);
            await _context.Users.InsertOneAsync(_user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _context
                              .Users
                              .FindOneAndUpdateAsync(x => x.Id == id,
                               Builders<User>.Update
                              .Set(x => x.IsDeleted, true)
                              );
        }

        public async Task UnDeleteUserAsync(string id)
        {
            await _context
                              .Users
                              .FindOneAndUpdateAsync(x => x.Id == id,
                               Builders<User>.Update
                              .Set(x => x.IsDeleted, false)
                              );
        }


        public async Task<User> GetUserByIdAsync(string id, bool? isDeleted)
        {

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, id);
            if (isDeleted.HasValue)
            {
                filter &= builder.Eq(x => x.IsDeleted, isDeleted);
            }

            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByMobileNoAsync(string mobileNo)
        {
            return await _context.Users.Find(x => x.MobileNo == mobileNo).FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(UpdateUserDTO user, User previousDetails)
        {
            var path = Path.Combine(_wwwPath, _configuration.GetValue<string>($"ResizeImage:ProfileImage:FilePath"), DateTime.Now.ToString(), user.Name);
            var fileName = _configuration.GetValue<string>($"ResizeImage:ProfileImage:FileName");
            if (user.ProfileImage != null)
            {
                await _fileRepository.ResizeUploadFileAsync(
                    user.ProfileImage,
                    path,
                    _configuration.GetValue<int>($"ResizeImage:ProfileImage:Width"),
                    _configuration.GetValue<int>($"ResizeImage:ProfileImage:Height"),
                    _configuration.GetValue<string>($"ResizeImage:ProfileImage:FileExtension"),
                   fileName
                );
            }

            await _context
                               .Users
                               .FindOneAndUpdateAsync(x => x.Id == previousDetails.Id,
                                Builders<User>.Update
                               .Set(x => x.MobileNo, user.MobileNo)
                               .Set(x => x.Name, user.Name)
                               .Set(x => x.ProfileImage, user.ProfileImage != null ? Path.Combine(path, fileName) : null)
                               );
        }

        public async Task UpdateUserAsync(CreateUserDTO user, string id)
        {
            await _context
                              .Users
                              .FindOneAndUpdateAsync(x => x.Id == id,
                               Builders<User>.Update
                              .Set(x => x.MobileNo, user.MobileNo)
                              .Set(x => x.Name, user.Name)
                              .Set(x => x.Role, user.Role)
                              .Set(x => x.Password, _hasher.Hash(user.Password))
                              );
        }

    }
}