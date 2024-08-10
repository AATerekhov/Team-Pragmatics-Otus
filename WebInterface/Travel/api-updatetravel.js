
async function getTravelByIdAsync(id){
    let result =await fetch(`http://localhost:5200/api/travel/${id}`,{
        method: 'GET',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    if(result.ok)
        return await result.json();
}
//Добавление Yкарты c EventPoint и предложение точек по типам.
async function initUpdateMap() {       

    let travelId = new URLSearchParams(window.location.search).get('id');
    let scale = new URLSearchParams(window.location.search).get('Scale');
    let placeTypeId = new URLSearchParams(window.location.search).get('typeId');
    let travel = await getTravelByIdAsync(travelId);
    // Промис `ymaps3.ready` будет зарезолвлен, когда загрузятся все компоненты основного модуля API
    await ymaps3.ready;
    const {YMap, YMapDefaultSchemeLayer, YMapDefaultFeaturesLayer,YMapFeature,YMapControls,YMapControlButton,YMapListener} = ymaps3;    
    
    let TrDescField = document.getElementById('description');
    TrDescField.value = travel.description;

    let lngSP = document.getElementById('longitudeSP');
    let startLng = Number(travel.startPoint.split(',')[0]);
    lngSP.value = startLng; 
    let latSP = document.getElementById('latitudeSP');
    let startLat = Number(travel.startPoint.split(',')[1]);
    latSP.value = startLat;
    
    let start = {
        longitude: startLng,
        latitude: startLat};

        function onDragMoveHandlerSP(coordinates) { 
            lngSP.value = coordinates[0].toFixed(6); 
            startLng = coordinates[0].toFixed(6);   
            latSP.value = coordinates[1].toFixed(6);
            startLat = coordinates[1].toFixed(6);
            line.update({ geometry: {
                type: 'LineString',
                coordinates: getMapPoints()
              }});
        }
    
    let lngFP = document.getElementById('longitudeFP');
    let finishLng = Number(travel.finishPoint.split(',')[0]);
    lngFP.value = finishLng;
    let latFP = document.getElementById('latitudeFP');
    let finishLat = Number(travel.finishPoint.split(',')[1]);
    latFP.value = finishLat;

    let finish = {
        longitude: finishLng,
        latitude: finishLat};

        function onDragMoveHandlerEP(coordinates) { 
            //strpnt.value = coordinates;
            lngFP.value = coordinates[0].toFixed(6);    
            finishLng = coordinates[0].toFixed(6);  
            latFP.value = coordinates[1].toFixed(6);
            finishLat = coordinates[1].toFixed(6);
            line.update({ geometry: {
                type: 'LineString',
                coordinates: getMapPoints()
              }});
        }    
    
    // Иницилиазируем карту
    const map = new YMap(
        // Передаём ссылку на HTMLElement контейнера
        document.getElementById('travelmap'),
        // Передаём параметры инициализации карты
        {
            location: {
                // Координаты центра карты
                center: [startLng/2 + finishLng/2, startLat/2 + finishLat/2],
                // Уровень масштабирования
                zoom: Number(scale)
            },
            behaviors: ['drag', 'scrollZoom', 'pinchZoom', 'dblClick'],
            showScaleInCopyrights: true
        }
    );
    // Добавляем слой для отображения схематической карты
    map.addChild(new YMapDefaultSchemeLayer()); 

    // Добавьте слой для маркеров
    map.addChild(new YMapDefaultFeaturesLayer({zIndex: 1800}));

    const clickCallback = (object,event) => {
        let lngP = document.getElementById('longitudeP');
        lngP.value = event.coordinates[0].toFixed(6);
        let latP = document.getElementById('latitudeP');
        latP.value = event.coordinates[1].toFixed(6);
    }
    const listener = new YMapListener({
        layer: 'any',
        onClick: clickCallback
    });
    map.addChild(listener);
    
    let points = await GetPoints();
    let mapPoints = getMapPoints();
    function getMapPoints()
    {
        let maps = [[startLng, startLat]];
        points.forEach(element => {
            maps.push([Number(element.pointMap.split(',')[0]),Number(element.pointMap.split(',')[1])]);
        });
        maps.push([finishLng, finishLat]);
        return maps;
    }

    // Import the package to add a default marker
    const {YMapDefaultMarker} = await ymaps3.import('@yandex/ymaps3-markers@0.0.1');
    // Create default markers and add them to the map
    const draggableStartPoint  = CreateDragMarker( [startLng, startLat],`Начало путешествия`,onDragMoveHandlerSP);  
    const draggableFinishPoint = CreateDragMarker( [finishLng, finishLat],`Конечная точка`,onDragMoveHandlerEP);
    const line = CreateLine(mapPoints);
    map.addChild(draggableStartPoint); 
    map.addChild(draggableFinishPoint);    
    map.addChild(line);  
    
    if(placeTypeId !== 0){
        let places = await GetTrasingPlaces(ConvertMapPointsInTracing(mapPoints),placeTypeId);
        places.forEach(element => {   
          let insertPoint = [element.longitude,element.latitude];
          const draggableMarker  = new YMapDefaultMarker({
           coordinates: insertPoint,
           color: '#00CC00',
           popup: {content: `${element.name}<br> 
               ${element.description.split(" |",8)[0]}<br> 
               ${element.description.split(" |",8)[1]}<br> 
               ${element.description.split(" |",8)[2]}<br> 
               ${element.description.split(" |",8)[3]}<br> 
               ${element.description.split(" |",8)[4]}<br> 
               ${element.description.split(" |",8)[5]}<br> 
               ${element.description.split(" |",8)[6]}<br> 
               ${element.description.split(" |",8)[7]}<br> 
               ${element.longitude}, ${element.latitude}<br>             
               `, position: 'left'}
            });
            map.addChild(draggableMarker ); 
        });
    }    

    const bottomControls = new YMapControls({position: 'bottom'});
    map.addChild(bottomControls);
    let typePlaces =await GetPlaceTypes();
    const buttoms = [];
    typePlaces.forEach(element => {
        const button = new YMapControlButton({
            text: element.name,
            color: '#fff',
            background: '#007afce6',
            onClick: () => window.location.href = `updateTravel.html?typeId=${element.id}&Scale=8&id=${travelId}`
          });
          buttoms.push(button);
    });
    buttoms.forEach(element => 
        bottomControls.addChild(element));    

    function CreateLine(points)
     {
        return new YMapFeature({
            geometry: {
              type: 'LineString',
              coordinates: points
            },
            style: {stroke: [{color: '#0047FF', width: 4}]}
          });
     }
     function CreateDragMarker( point, title ,onDragMoveHandler)
     {
       return new YMapDefaultMarker({
           coordinates: point,
           title: title,
           subtitle: 'Перетащите метку',
           color: '#0047FF',
           draggable: true,
           onDragMove: onDragMoveHandler
         });
     }  
}

function ConvertMapPointsInTracing(mapPoints) {
    let roadPoints = [];
    mapPoints.forEach(element =>{
        roadPoints.push({
            longitude: element[0],
            latitude: element[1]});
    });
    return roadPoints;
}
async function GetPlaceTypes() {
    let resultType =await fetch("http://localhost:5200/api/placetypes",{
        method: 'GET',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    if(resultType.ok)
        return await resultType.json();
}
//id - тип мест трассировки
async function GetTrasingPlaces(roadPoints,id) {    
    let offset = 0.045454;
    let road = {roadPoints,offset};
    let result = await fetch(`http://localhost:5200/api/place/tracing/${id}`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'},
        body: JSON.stringify(road)
    });
    if(result.ok)
        return await result.json();
}
async function  addPoint() {
    let travelId = new URLSearchParams(window.location.search).get('id');
    const lngP = Number(document.getElementById('longitudeP').value);
    const latP = Number(document.getElementById('latitudeP').value);
    let waitingTimeCountMinutes = Number(document.getElementById('timeP').value);
    let pointDesc = document.getElementById('descP').value;
    let pointMap = `${lngP},${latP}`;
    let trevelPoint = {pointMap,pointDesc,travelId,waitingTimeCountMinutes};
    let result = await fetch(`http://localhost:5200/api/point`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'},
        body: JSON.stringify(trevelPoint)
    });
    if(result.ok)
        window.location.href = `updateTravel.html?typeId=0&Scale=8&id=${travelId}`
}
async function GetPoints() {
    let travelId = new URLSearchParams(window.location.search).get('id');
    let result = await fetch(`http://localhost:5200/api/points/${travelId}`,{
        method: 'PUT',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    if(result.ok)
        return await result.json();
}