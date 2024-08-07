async function loadTravels(){
    let result =await fetch("http://localhost:5200/api/travels",{
        method: 'GET',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });

	let Traveltable = document.getElementById('Travels');
    console.log(Traveltable);
    if(result.ok){
        let travelss = await result.json();
        travelss.forEach(travel => {
            let TravelRow  = document.createElement('tr');
            let travelId = document.createElement('td');
            travelId.innerText = travel.id;
            let TrDescField = document.createElement('td');
            TrDescField.innerText = travel.description;
            let TrStartField = document.createElement('td');
            TrStartField.innerText = travel.startPoint;
            let TrFinishField = document.createElement('td');
            TrFinishField.innerText = travel.finishPoint;
            let link = document.createElement('a');
            link.innerText = "Редактировать";
            link.href = `updateTravel.html?typeId=0&Scale=8&id=${travel.id}`;
            let editField = document.createElement('td');
            editField.appendChild(link);
            TravelRow.appendChild(travelId);
            TravelRow.appendChild(TrDescField);
            TravelRow.appendChild(TrStartField);
            TravelRow.appendChild(TrFinishField);
            TravelRow.appendChild(editField);
            Traveltable.appendChild(TravelRow);
        });
    }
}
async function travelCreating(){
    let id = 0;
    let description = document.getElementById('description').value;
    let lngSP = document.getElementById('longitudeSP').value;
    let latSP = document.getElementById('latitudeSP').value;
    let startPoint = lngSP + ',' + latSP;
    let lngFP = document.getElementById('longitudeFP').value;
    let latFP = document.getElementById('latitudeFP').value;
    let finishPoint = lngFP + ',' + latFP;
    let travel = {id,description, startPoint, finishPoint};
    //alert(JSON.stringify(travel))
    let result = await fetch('http://localhost:5200/api/travel', {
     method: 'POST',
     headers: {
         'Content-Type': 'application/json',
         'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'
     },
     body: JSON.stringify(travel)
    });
    if(result.ok)
     window.location.href = "index.html";
 }

 async function travelUpdating() {
    
 }


 //Yandex Map function
async function initMap() {    
    await ymaps3.ready;
    const {YMap, YMapDefaultSchemeLayer, YMapDefaultFeaturesLayer} = ymaps3;    
    
    let dateStart = document.getElementById('dateStart');
    var today = new Date();
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    today = yyyy + '-' + mm + '-' + dd ;
    dateStart.value = today;
    let lngSP = document.getElementById('longitudeSP');
    lngSP.value = 30.316927;
    let latSP = document.getElementById('latitudeSP');
    latSP.value = 59.938716;

        function onDragMoveHandlerSP(coordinates) { 
            //strpnt.value = coordinates;
            lngSP.value = coordinates[0].toFixed(6);            
            latSP.value = coordinates[1].toFixed(6);
            draggableStartPoint.update({coordinates});
        }

    let lngFP = document.getElementById('longitudeFP');
    lngFP.value = 30.316927;
    let latFP = document.getElementById('latitudeFP');
    latFP.value = 59.938716;

        function onDragMoveHandlerEP(coordinates) { 
            //strpnt.value = coordinates;
            lngFP.value = coordinates[0].toFixed(6);            
            latFP.value = coordinates[1].toFixed(6);
            draggableFinishPoint.update({coordinates});
        }    

    // Иницилиазируем карту
    const map = new YMap(
        // Передаём ссылку на HTMLElement контейнера
        document.getElementById('travelmap'),
        // Передаём параметры инициализации карты
        {
            location: {
                // Координаты центра карты
                //center: [37.588144, 55.733842],
                center: [30.314607, 59.939876],
                // Уровень масштабирования
                zoom: 10
            },
            behaviors: ['drag', 'scrollZoom', 'pinchZoom', 'dblClick'],
            showScaleInCopyrights: true
        }
    );
    // Добавляем слой для отображения схематической карты
    map.addChild(new YMapDefaultSchemeLayer()); 

    // Добавьте слой для маркеров
    map.addChild(new YMapDefaultFeaturesLayer({zIndex: 1800}));

    // Import the package to add a default marker
    const {YMapDefaultMarker} = await ymaps3.import('@yandex/ymaps3-markers@0.0.1');
    // Create default markers and add them to the map
    const draggableStartPoint  = new YMapDefaultMarker({
        coordinates: [30.314607, 59.939876],
        title: `Начало путешествия`,
        subtitle: 'Перетащите метку',
        color: '#0047FF',
        draggable: true,
        onDragMove: onDragMoveHandlerSP
      });
      map.addChild(draggableStartPoint); 
    
    const draggableFinishPoint  = new YMapDefaultMarker({
        coordinates: [30.394966, 59.948866],
        title: `Конечная точка`,
        subtitle: 'Перетащите метку',
        color: '#0047FF',
        draggable: true,
        onDragMove: onDragMoveHandlerEP
      });
      map.addChild(draggableFinishPoint); 
}
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
    const {YMap, YMapDefaultSchemeLayer, YMapDefaultFeaturesLayer} = ymaps3;
    
    let TrDescField = document.getElementById('description');
    TrDescField.value = travel.description;

    let lngSP = document.getElementById('longitudeSP');
    let startLng = Number(travel.startPoint.split(',')[0]);
    lngSP.value = startLng; 
    let latSP = document.getElementById('latitudeSP');
    let startLat = Number(travel.startPoint.split(',')[1]);
    latSP.value = startLat
    
    let start = {
        longitude: startLng,
        latitude: startLat};

        function onDragMoveHandlerSP(coordinates) { 
            //strpnt.value = coordinates;
            lngSP.value = coordinates[0].toFixed(6);            
            latSP.value = coordinates[1].toFixed(6);
            draggableStartPoint.update({coordinates});
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
            latFP.value = coordinates[1].toFixed(6);
            draggableFinishPoint.update({coordinates});
        }    
    
    // Иницилиазируем карту
    const map = new YMap(
        // Передаём ссылку на HTMLElement контейнера
        document.getElementById('travelmap'),
        // Передаём параметры инициализации карты
        {
            location: {
                // Координаты центра карты
                //center: [37.588144, 55.733842],
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

    // Import the package to add a default marker
    const {YMapDefaultMarker} = await ymaps3.import('@yandex/ymaps3-markers@0.0.1');
    // Create default markers and add them to the map
    const draggableStartPoint  = new YMapDefaultMarker({
        coordinates: [startLng, startLat],
        title: `Начало путешествия`,
        subtitle: 'Перетащите метку',
        color: '#0047FF',
        draggable: true,
        onDragMove: onDragMoveHandlerSP
      });
      map.addChild(draggableStartPoint); 
  
    const draggableFinishPoint  = new YMapDefaultMarker({
        coordinates: [finishLng, finishLat],
        title: `Конечная точка`,
        subtitle: 'Перетащите метку',
        color: '#0047FF',
        draggable: true,
        onDragMove: onDragMoveHandlerEP
      });
      map.addChild(draggableFinishPoint);      
      
      let places = await GetTrasingPlaces(start,finish,placeTypeId);
      places.forEach(element => {   
        let insertPoint = [element.longitude,element.latitude];
        const draggableMarker  = new YMapDefaultMarker({
         coordinates: insertPoint,
         //title: `${element.name}`,
         //subtitle: 'Place',
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

async function AddButton() {    
    let id = new URLSearchParams(window.location.search).get('id');
     //Заполняем таблицу placeTypes кнопками.
     let resultType =await fetch("http://localhost:5200/api/placetypes",{
        method: 'GET',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    if(resultType.ok){
        let placetypes = await resultType.json();
        placetypes.forEach(element => {
           
           createButton(document.body,element.name,element.id,id);           
        });
    } 
}

function createButton(context, name , typeId, id) {
    var button = document.createElement("input");
    button.type = "button";
    button.value = name;
    button.onclick = function openTravelUpdate() {     
        window.location.href = `updateTravel.html?typeId=${typeId}&Scale=8&id=${id}`;  
        return false;      
     }
    button.disabled = false;
    context.appendChild(button);
}
//id - тип мест трассировки
async function GetTrasingPlaces(start,finish,id) {    
    let offset = 0.045454;
    let road = {start,finish,offset};
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
