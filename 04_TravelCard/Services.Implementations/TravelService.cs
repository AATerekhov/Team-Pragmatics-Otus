using AutoMapper;
using Services.Abstractions;
using Services.Repositories.Abstractions;
using Services.Contracts.Travel;
using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using MassTransit;
using Newtonsoft.Json;
using CommonNameSpace;

namespace Services.Implementations
{
    /// <summary>
    /// Сервис работы с путешествиями.
    /// </summary>
    public class TravelService : ITravelService
    {
        private readonly string _queueName = "event_travel_queue";
        private readonly IMapper _mapper;
        private readonly ITravelRepository _TravelRepository;
        private readonly IBusControl _busControl;

        public TravelService(IMapper mapper, ITravelRepository TravelRepository)
        {
            _mapper = mapper;
            _TravelRepository = TravelRepository;
            //_busControl = busControl;
        }

        /// <summary>
        /// Создать путешествие.
        /// </summary>
        /// <param name="CreatingTravelDto"> ДТО путешествия. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<TravelDto> CreateAsync(CreatingTravelDto creatingtravelDto)
        {
            var TravelEntity = _mapper.Map<CreatingTravelDto, Travel>(creatingtravelDto);
            var createdTravel = await _TravelRepository.AddAsync(TravelEntity);
            await _TravelRepository.SaveChangesAsync();
            return _mapper.Map<TravelDto>(createdTravel);

            //Брокер
            //return createdTravel.Id;
        }

        /// <summary>
        /// Удалить путешествие.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(int id)
        {
            //var travel = await _TravelRepository.GetAsync(id, CancellationToken.None);
            //travel.Deleted = true;
            //_TravelRepository.Update(travel);
            //await _TravelRepository.SaveChangesAsync();
            await _TravelRepository.DeleteAsync(id);
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
            travel.StartPoint = updatingTravelDto.StartPoint;
            travel.FinishPoint = updatingTravelDto.FinishPoint;
            travel.StartDate = updatingTravelDto.StartDate;
            _TravelRepository.Update(travel);
            await _TravelRepository.SaveChangesAsync();

            //Брокер
            //var sendEndPoint = await _busControl.GetSendEndpoint(new Uri($"queue:{_queueName}"));
            //if (sendEndPoint == null)
            //{
            //    throw new Exception($"Не удалось найти очередь {_queueName}");
            //}
            //await sendEndPoint.Send(new MessageDto
            //{
            //    Content = $"Travel is Updated. {JsonConvert.SerializeObject(travel)}"
            //}, CancellationToken.None);
        }

        /// <summary>
        /// Получить все путешествия.
        /// </summary>
        /// <returns> IEnumerable путешествий. </returns>
        public async Task<IEnumerable<TravelDto>> GetTravelsAsync() => (await _TravelRepository.GetAllAsync()).Select(_mapper.Map<TravelDto>);
    }
}
