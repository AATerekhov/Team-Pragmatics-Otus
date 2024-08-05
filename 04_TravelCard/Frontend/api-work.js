async function loadTravels(){
    //alert('Hello');
    let result = await fetch("http://localhost:5100/Travel");
	let Traveltable = document.getElementById('Travels');
    console.log(Traveltable);
    if(result.ok){
        let travelss = await result.json();
        travelss.forEach(travel => {
            //alert(JSON.stringify(travel));
            let TravelRow  = document.createElement('tr');
            let TrDescField = document.createElement('td');
            TrDescField.innerText = travel.description;
            let TrStartField = document.createElement('td');
            TrStartField.innerText = travel.startPoint;
            let TrFinishField = document.createElement('td');
            TrFinishField.innerText = travel.finishPoint;
            TravelRow.appendChild(TrDescField);
            TravelRow.appendChild(TrStartField);
            TravelRow.appendChild(TrFinishField);
            Traveltable.appendChild(TravelRow);
        });
    }
}
async function travelCreating(){

    let id = document.getElementById('id').value;;
    let description = document.getElementById('description').value;
    let lngSP = document.getElementById('longitudeSP').value;
    let latSP = document.getElementById('latitudeSP').value;
    let startPoint = lngSP + ',' + latSP;
    let lngFP = document.getElementById('longitudeFP').value;
    let latFP = document.getElementById('latitudeFP').value;
    let finishPoint = lngFP + ',' + latFP;
    let travel = {id, description, startPoint, finishPoint};
    //alert(JSON.stringify(travel))
    let result = await fetch('http://localhost:5100/Travel', {
     method: 'POST',
     headers: {
         'Content-Type': 'application/json'
     },
     body: JSON.stringify(travel)
    });
    if(result.ok)
     window.location.href = "index.html";
 }
 //Yandex Map function
async function initMap() {
    // Промис `ymaps3.ready` будет зарезолвлен, когда загрузятся все компоненты основного модуля API
    await ymaps3.ready;
    const {YMap, YMapDefaultSchemeLayer, YMapDefaultFeaturesLayer} = ymaps3;
    //let centerPoint = new URLSearchParams(window.location.search).get('LngLat').split(","); 
    
    //let strpnt = document.getElementById('startPoint');
    //strpnt.value = 30.316927,59.938716;
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
        color: '#00CC00',
        draggable: true,
        onDragMove: onDragMoveHandlerSP
      });
      map.addChild(draggableStartPoint); 
    
    const draggableFinishPoint  = new YMapDefaultMarker({
        coordinates: [30.394966, 59.948866],
        title: `Конечная точка`,
        subtitle: 'Перетащите метку',
        color: '#00CC00',
        draggable: true,
        onDragMove: onDragMoveHandlerEP
      });
      map.addChild(draggableFinishPoint); 
}