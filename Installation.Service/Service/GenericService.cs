using AutoMapper;
using Installation.Domain;
using Installation.Domain.IRepository;
using Installation.Domain.UOW;
using Installation.Service.IService;
using Installation.Service.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.Service
{
    public class GenericService<TDto, T>: IGenericService<TDto, T> where TDto : class where T: class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto?>> GetAllAsync()
        {
            var result = await _unitOfWork.GetRepository<T>().GetAllAsync();
            var resultDto = _mapper.Map<IEnumerable<TDto>>(result);

            return resultDto;
        }

        public async Task<TDto?> GetByIdAsync(int? id)
        {
            var result = await _unitOfWork.GetRepository<T>().GetByIdAsync(id);
            var resultDto = _mapper.Map<TDto>(result);
            return resultDto;
        }

        public Task AddAsync(TDto entity)
        {
            var resultEntity = _mapper.Map<T>(entity);
            return _unitOfWork.GetRepository<T>().AddAsync(resultEntity);
        }

        public Task UpdateAsync(TDto entity)
        {
            var resultEntity = _mapper.Map<T>(entity);
            return _unitOfWork.GetRepository<T>().UpdateAsync(resultEntity);
        }

        public Task DeleteAsync(TDto entity)
        {
            var resultEntity = _mapper.Map<T>(entity);
            return _unitOfWork.GetRepository<T>().DeleteAsync(resultEntity);
        }

    }
}
