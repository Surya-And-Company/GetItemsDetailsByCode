using System;
using System.Threading.Tasks;
using AutoMapper;
using ItemsArchiveService.Data;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;
using MongoDB.Driver;

namespace ItemsArchiveService.Repository
{
    public class ThirdPartyRepository : IThirdPartyRepository
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public ThirdPartyRepository(IDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
       
        public async Task AddAsync(ThirdPartyDTO thirdparty)
        {
            var data = _mapper.Map<ThirdPartyAllowed>(thirdparty);
            await _context.ThirdPartiesAllowed.InsertOneAsync(data);
        }

        public async Task<bool> IsValidAsync(string token)
        {
            var result = await _context.ThirdPartiesAllowed.Find(x => x.Token == token).FirstOrDefaultAsync();
            if (result != null)
                return true;

            return false;
        }

    }
}