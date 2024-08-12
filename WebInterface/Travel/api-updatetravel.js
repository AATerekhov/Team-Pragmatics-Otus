
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
        let desc = document.getElementById('descP');
        let time = document.getElementById('timeP');
        const decsPoint = sorted(event.coordinates);
        if (decsPoint != ""){
            desc.value = decsPoint;
            time.value = 10;
        } else {
            desc.value = "";
            time.value = "";
        }
    }
    function sorted(coordinates)
    {
        let result = "";
        places.forEach(element => {
            const distance =Math.sqrt((element.longitude - coordinates[0])**2 + (element.latitude - coordinates[1])**2);
            if (distance < 0.001) 
                result = element.description;
        });
        return result;
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
            if(element.waitingTimeCountMinutes == 0)
            maps.push([Number(element.pointMap.split(',')[0]),Number(element.pointMap.split(',')[1])]);
        });
        maps.push([finishLng, finishLat]);
        return maps;
    }

    class HintWindow extends ymaps3.YMapComplexEntity {
        _element;
        _detachDom;
        _unwatchSearchContext;
       // Method for create a DOM control element
       _createElement() {
           const windowElement = document.createElement('div');
           windowElement.className = 'hintWindow';
           return windowElement;
       }

       // Callback method triggered on hint context change, responsible for updating the text in the hint window
       _searchContextListener() {
           this._element.innerHTML = this._consumeContext(YMapHintContext)?.hint;
       }

       // Method for attaching the control to the map
        _onAttach() {
           this._element = this._createElement();
           this._unwatchSearchContext = this._watchContext(YMapHintContext, this._searchContextListener.bind(this));
           this._detachDom = ymaps3.useDomContext(this, this._element, this._element);
       }

       // Method for detaching control from the map
        _onDetach() {
           this._unwatchSearchContext();
           this._detachDom();
       }
   }
    const {YMapHint, YMapHintContext} = await ymaps3.import('@yandex/ymaps3-hint@0.0.1');
    // Import the package to add a default marker
    const {YMapDefaultMarker} = await ymaps3.import('@yandex/ymaps3-markers@0.0.1');
    // Create default markers and add them to the map
    const draggableStartPoint  = CreateDragMarker( [startLng, startLat],`Начало путешествия`,onDragMoveHandlerSP);  
    const draggableFinishPoint = CreateDragMarker( [finishLng, finishLat],`Конечная точка`,onDragMoveHandlerEP);
    const line = CreateLine(mapPoints);
    map.addChild(draggableStartPoint); 
    map.addChild(draggableFinishPoint);    
    map.addChild(line);
    let places=[];
    if(placeTypeId != 0){
        places = await GetTrasingPlaces(ConvertMapPointsInTracing(mapPoints),placeTypeId);
        places.forEach(element => {   
          let insertPoint = [element.longitude,element.latitude];
          const draggableMarker  = new YMapDefaultMarker({
           coordinates: insertPoint,
           properties: {
            hint: `${getComment(element.description)}`
          },
           color: '#00CC00',
           //popup: {content: `${getComment(element.description)}`, position: 'left'}
        });
            map.addChild(draggableMarker ); 
        });
    } else {
        points.forEach(element =>{
            if(element.waitingTimeCountMinutes != 0){
            let insertPoint = [Number(element.pointMap.split(',')[0]),Number(element.pointMap.split(',')[1])];
            const draggableMarker  = new YMapDefaultMarker({
            coordinates: insertPoint,
            properties: {
            hint: `${getComment(element.pointDesc)}`
            },
            color: '#FF0000'           
            });
            map.addChild(draggableMarker ); 
            }
        });
    }  
    const hint = new YMapHint({hint: (object) => object?.properties?.hint});
    map.addChild(hint); 
    hint.addChild(new HintWindow({}));
    
    function getComment(descriptions) {
        let list = descriptions.split(" |");
        let result = "";
        list.forEach(element => {
            if(element != "")
            result =  `${result}${element}<br>`;
        });
        return result;
    }

    const bottomControls = new YMapControls({position: 'bottom'});
    map.addChild(bottomControls);
    let typePlaces =await GetPlaceTypes();
    const buttoms = [];
    const buttonBase = new YMapControlButton({
        text: 'Travel',
        color: '#fff',
        background: '#007afce6',
        onClick: () => window.location.href = `updateTravel.html?typeId=0&Scale=8&id=${travelId}`
      });
      buttoms.push(buttonBase);
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

    //Добавление точек в таблицу.
    let Traveltable = document.getElementById('TravelsPoint');
    points.forEach(element =>{
        if(element.waitingTimeCountMinutes != 0){               
        let TravelRow  = document.createElement('tr');
        let Time = document.createElement('td');
        Time.innerText = element.waitingTimeCountMinutes;
        let Decs = document.createElement('td');
        Decs.innerText = element.pointDesc;
        let Coord = document.createElement('td');
        Coord.innerText = element.pointMap;
        TravelRow.appendChild(Time);
        TravelRow.appendChild(Decs);
        TravelRow.appendChild(Coord);
        Traveltable.appendChild(TravelRow);
        }
    });


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
async function deleteLastPoint(event) {
    let travelId = new URLSearchParams(window.location.search).get('id');    
    let point = await GetPopPoints(event);
    let result = await fetch(`http://localhost:5200/api/point/${point.id}`,{
        method: 'DELETE',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    if(result.ok)
         window.location.href = `updateTravel.html?typeId=0&Scale=8&id=${travelId}`;
}
async function GetPopPoints(event)
{
    let result = "";
    let points = await GetPoints();
    let pointsEvent = [];
    let pointsTrece = [];
    points.forEach(element =>{
        if (element.waitingTimeCountMinutes == 0){
            pointsTrece.push(element);
        } else {
            pointsEvent.push(element);
        }
    });
    
    if( event == 1){
        result = pointsEvent.pop();
    } else {
        result = pointsTrece.pop();
    }
    return result;
}


function goHome() {
    window.location.href = `index.html`;
}

async function travelUpdating() {
    let travelId = new URLSearchParams(window.location.search).get('id');
    let description = document.getElementById('description').value;
    let startPoint = `${document.getElementById('longitudeSP').value},${document.getElementById('latitudeSP').value}`;
    let finishPoint = `${document.getElementById('longitudeFP').value},${document.getElementById('latitudeFP').value}`;
    let travel = {description,startPoint,finishPoint};
    let result = await fetch(`http://localhost:5200/api/travel/${travelId}`,{
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'
        },
        body: JSON.stringify(travel)
    });
    if(result.ok)
        window.location.href = `updateTravel.html?typeId=0&Scale=8&id=${travelId}`;
}

async function travelDelete() {
    let travelId = new URLSearchParams(window.location.search).get('id');
    let result = await fetch(`http://localhost:5200/api/travel/${travelId}`,{
        method: 'DELETE',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    if(result.ok)
        window.location.href = `index.html`;
}
async function ShowYandex()
{    
    let travelId = new URLSearchParams(window.location.search).get('id');
    let scale = new URLSearchParams(window.location.search).get('Scale');
    let travel = await getTravelByIdAsync(travelId);

    let startLng = Number(travel.startPoint.split(',')[0]);    
    let startLat = Number(travel.startPoint.split(',')[1]);
    let finishLng = Number(travel.finishPoint.split(',')[0]);
    let finishLat = Number(travel.finishPoint.split(',')[1]);
    
    let points = await GetPoints();
    let pointsEvent = [];
    points.forEach(element =>{
        if (element.waitingTimeCountMinutes != 0){
            pointsEvent.push(element);
        } 
    });

    let centerlat = startLat/2 + finishLat/2;
    let centerlgn = startLng/2 + finishLng/2;
    let result = `https://yandex.ru/maps/2/saint-petersburg/?ll=${getCoordinat(centerlgn,centerlat)}&mode=routes&rtext=${getCoordinat(startLat,startLng)}`;
    let gaps = "";
    let finish = `~${getCoordinat(finishLat,finishLng)}`;
    let rtm = '&rtm=atm';
    let rtt = `&rtt=auto`;
    let ruri = `&ruri=~`;

    pointsEvent.forEach(element=>{
        let coordinates = element.pointMap.split(',');
        ruri += `~`;
        gaps += `~${getCoordinat(coordinates[1],coordinates[0])}`;
    });
    let z = `&z=${scale}`;
    result = result + gaps + finish + rtm + rtt + ruri + z;
    window.location.href = result;
    function getCoordinat(lat,lng){
        return `${lat}%2C${lng}`;
    }
}