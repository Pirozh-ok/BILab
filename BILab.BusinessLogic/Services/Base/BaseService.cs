using AutoMapper;
using BILab.DataAccess;

namespace BILab.BusinessLogic.Services.Base {
    public abstract class BaseService {
        protected readonly IMapper _mapper;
        protected readonly ApplicationDbContext _context;

        public BaseService(IMapper mapper, ApplicationDbContext context) {
            _mapper = mapper;
            _context = context;
        }
    }
}
