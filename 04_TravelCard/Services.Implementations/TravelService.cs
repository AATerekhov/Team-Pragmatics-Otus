using AutoMapper;
using Services.Abstractions;
using Services.Repositories.Abstractions;
using Services.Contracts.Travel;
using Domain.Entities;
using Infrastructure.Repositories.Implementations;

namespace Services.Implementations
{
    /// <summary>
    /// Сервис работы с путешествиями.
    /// </summary>
    public class TravelService : ITravelService
    {
        private readonly IMapper _mapper;
        private readonly ITravelRepository _TravelRepository;

        public TravelService(IMapper mapper, ITravelRepository TravelRepository)
        {
            _mapper = mapper;
            _TravelRepository = TravelRepository;
        }

        /// <summary>
        /// Создать путешествие.
        /// </summary>
        /// <param name="CreatingTravelDto"> ДТО путешествия. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<int> CreateAsync(CreatingTravelDto creatingtravelDto)
        {
            var TravelEntity = _mapper.Map<CreatingTravelDto, Travel>(creatingtravelDto);
            TravelEntity.Deleted = false;
            var createdTravel = await _TravelRepository.AddAsync(TravelEntity);
            await _TravelRepository.SaveChangesAsync();
            return createdTravel.Id;

            //Брокер
            //await _busControl.Publish(new MessageDto
            //{
            //    Content = $"Travel {createdTravel.Id} with desc {createdTravel.TravelDesc} is added"
            //});

            return createdTravel.Id;
        }

        /// <summary>
        /// Удалить путешествие.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(int id)
        {
            var travel = await _TravelRepository.GetAsync(id, CancellationToken.None);
            travel.Deleted = true;
            _TravelRepository.Update(travel);
            await _TravelRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить путешествие.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО путешествия. </returns>
        public async Task<TravelDto> GetByIdAsync(int id)
        {
            //return _mapper.Map<TravelDto>(await _TravelRepository.GetAsync(id, CancellationToken.None));
            var travel = await _TravelRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Travel, TravelDto>(travel);
        }

        //public async Task<TravelDto?> GetTravelAsync(int id) => _mapper.Map<TravelDto>(await _TravelRepository.GetTravelByIdAsync(id));

        /// <summary>
        /// Изменить путешествие.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingTravelDto"> ДТО путешествия. </param>
        public async Task UpdateAsync(int id, UpdatingTravelDto updatingTravelDto)
        {
            var travel = await _TravelRepository.GetAsync(id, CancellationToken.None);
            if (travel == null)
            {
                throw new Exception($"Путешествие с id = {id} не найдено");
            }

            travel.Description = updatingTravelDto.Description;
            _TravelRepository.Update(travel);
            await _TravelRepository.SaveChangesAsync();
        }
    }
}
