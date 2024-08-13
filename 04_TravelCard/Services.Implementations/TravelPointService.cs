using AutoMapper;
using Services.Abstractions;
using Services.Repositories.Abstractions;
using Services.Contracts.TravelPoint;
using Domain.Entities;

namespace Services.Implementations
{
    /// <summary>
    /// Сервис работы с точками остановки.
    /// </summary>
    public class TravelPointService : ITravelPointService
    {
        private readonly IMapper _mapper;
        private readonly ITravelPointRepository _TravelPointRepository;

        public TravelPointService(IMapper mapper, ITravelPointRepository TravelPointRepository)
        {
            _mapper = mapper;
            _TravelPointRepository = TravelPointRepository;
        }

        /// <summary>
        /// Создать точку остановки.
        /// </summary>
        /// <param name="CreatingTravelPointDto"> ДТО точки остановки. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<int> CreateAsync(CreatingTravelPointDto creatingtravelPointDto)
        {
            var TravelPointEntity = _mapper.Map<CreatingTravelPointDto, TravelPoint>(creatingtravelPointDto);
            TravelPointEntity.Deleted = false;
            var createdTravelPoint = await _TravelPointRepository.AddAsync(TravelPointEntity);
            await _TravelPointRepository.SaveChangesAsync();
            return createdTravelPoint.Id;

            //Брокер
            //await _busControl.Publish(new MessageDto
            //{
            //    Content = $"TravelPoint {createdTravelPoint.Id} with desc {createdTravelPoint.PointDesc} is added"
            //});

            return createdTravelPoint.Id;
        }

        /// <summary>
        /// Удалить точку остановки.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(int id)
        {
            var travelpoint = await _TravelPointRepository.GetAsync(id, CancellationToken.None);
            if (travelpoint != null)
            {
                 _TravelPointRepository.Delete(travelpoint.Id);
            }
            //travelpoint.Deleted = true;
            //_TravelPointRepository.Update(travelpoint);
            await _TravelPointRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить точку остановки.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО точки остановки. </returns>
        public async Task<TravelPointDto> GetByIdAsync(int id)
        {
            //return _mapper.Map<TravelPointDto>(await _TravelPointRepository.GetAsync(id, CancellationToken.None));
            var travelpoint = await _TravelPointRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<TravelPoint, TravelPointDto>(travelpoint);
        }

        /// <summary>
        /// Получить точки остановки путешествия.
        /// </summary>
        /// <param name="travelId"> Идентификатор путешествия. </param>
        /// <returns> массив ДТО точек остановки. </returns>
        public async Task<List<TravelPointDto>> GetAllByTravelIdAsync(int travelId)
        {
            var travelpoints = await _TravelPointRepository.GetAllByTravelIdAsync(travelId, CancellationToken.None);
            List<TravelPointDto> mappedList = travelpoints.Select(tp => _mapper.Map<TravelPoint, TravelPointDto>(tp)).ToList();
            return mappedList;
        }

        //public async Task<TravelPointDto?> GetTravelPointAsync(int id) => _mapper.Map<TravelPointDto>(await _TravelPointRepository.GetTravelPointByIdAsync(id));

        /// <summary>
        /// Изменить точку остановки.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="UpdatingTravelPointDto"> ДТО точки остановки. </param>
        public async Task UpdateAsync(int id, UpdatingTravelPointDto updatingTravelPointDto)
        {
            var travelpoint = await _TravelPointRepository.GetAsync(id, CancellationToken.None);
            if (travelpoint == null)
            {
                throw new Exception($"Точка остановки с id = {id} не найдена");
            }

            travelpoint.PointMap = updatingTravelPointDto.PointMap;
            travelpoint.PointDesc = updatingTravelPointDto.PointDesc;
            travelpoint.WaitingTimeCountMinutes = updatingTravelPointDto.WaitingTimeCountMinutes;
            _TravelPointRepository.Update(travelpoint);
            await _TravelPointRepository.SaveChangesAsync();
        }
    }
}
