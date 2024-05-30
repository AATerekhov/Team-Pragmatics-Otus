using AutoMapper;
using Services.Abstractions;
using Services.Repositories.Abstractions;
using Services.Contracts.Travel;
using Domain.Entities;

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
            var createdTravel = await _TravelRepository.AddAsync(TravelEntity);
            await _TravelRepository.SaveChangesAsync();
            return createdTravel.Id;

            //Брокер
            //await _busControl.Publish(new MessageDto
            //{
            //    Content = $"Travel {createdTravel.Id} with subject {createdTravel.Subject} is added"
            //});

            return createdTravel.Id;
        }

        /// <summary>
        /// Удалить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(int id)
        {
            var travel = await _TravelRepository.GetAsync(id, CancellationToken.None);
            travel.Deleted = true;
            await _TravelRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО юзера. </returns>
        public async Task<TravelDto> GetByIdAsync(int id)
        {
            //return _mapper.Map<TravelDto>(await _TravelRepository.GetAsync(id, CancellationToken.None));
            var travel = await _TravelRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Travel, TravelDto>(travel);
        }

        //public async Task<TravelDto?> GetTravelAsync(int id) => _mapper.Map<TravelDto>(await _TravelRepository.GetTravelByIdAsync(id));

        /// <summary>
        /// Изменить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingTravelDto"> ДТО юзера. </param>
        public async Task UpdateAsync(int id, UpdatingTravelDto updatingTravelDto)
        {
            var travel = await _TravelRepository.GetAsync(id, CancellationToken.None);
            if (travel == null)
            {
                throw new Exception($"Юзер с id = {id} не найден");
            }

            travel.TravelDesc = updatingTravelDto.TravelDesc;
            _TravelRepository.Update(travel);
            await _TravelRepository.SaveChangesAsync();
        }
    }
}
