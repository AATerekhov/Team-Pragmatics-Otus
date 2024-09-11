async function loadTravels(){    
    let userName = new URLSearchParams(window.location.search).get('user');
    let userId = new URLSearchParams(window.location.search).get('userid');
    let responceUrl = '';

    
    if(userId !== null)
        {
            responceUrl = `http://localhost:5200/api/travels/${userId}`;
            var logo = document.getElementById('LogoPage');
            logo.innerHTML  = `Путешествия (${userName})`;    
            
            //Генерация меню на вход
            let nemus = document.getElementsByTagName('p');
            let nemu = nemus[0];
            let linkCreate = document.createElement('a');
            linkCreate.innerText = "Создать путешествие";
            linkCreate.href = `createTravel.html?userid=${userId}&user=${userName}`;
            nemu.appendChild(linkCreate);
            let linkSettings = document.createElement('a');
            linkSettings.innerText = "Настройка"; 
            linkSettings.href = `../index.html`;
            nemu.appendChild(linkSettings);
            let linkEnter = document.createElement('a');
            linkEnter.innerText = "Выход"; 
            linkEnter.href = `../Login/login.html`;
            linkEnter.margin = 5;
            nemu.appendChild(linkEnter);
        }
        else
        {
            responceUrl = "http://localhost:5200/api/travels";
            //Генерация меню на вход
            let nemus = document.getElementsByTagName('p');
            let nemu = nemus[0];
            let linkEnter = document.createElement('a');
            linkEnter.innerText = "Вход"; 
            linkEnter.href = `../Login/login.html`;
            nemu.appendChild(linkEnter);
        }

    let result =await fetch(responceUrl,{
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
            let TrDateField = document.createElement('td');
            TrDateField.innerText = new Date(travel.startDate).toLocaleDateString('ru', {
			                                                                              year: 'numeric',
			                                                                              month: 'long',
			                                                                              day: 'numeric'
			                                                                            });            
            let TrOwnerField = document.createElement('td');
            TrOwnerField.innerText = travel.userName;
            let TrStartField = document.createElement('td');
            TrStartField.innerText = travel.startPoint;
            let TrFinishField = document.createElement('td');
            TrFinishField.innerText = travel.finishPoint;
            let link = document.createElement('a');
            if(userId !== null){
                link.innerText = "Редактировать";
                link.href = `updateTravel.html?typeId=0&Scale=8&id=${travel.id}&userid=${userId}&user=${userName}`;
            }
            else{
                link.innerText = "Посмотреть";
                link.href = `viewTravel.html?typeId=0&Scale=8&id=${travel.id}`;
            }
            
            
            let editField = document.createElement('td');
            editField.appendChild(link);
            TravelRow.appendChild(travelId);
            TravelRow.appendChild(TrDescField);
            TravelRow.appendChild(TrDateField);
            TravelRow.appendChild(TrOwnerField);
            TravelRow.appendChild(TrStartField);
            TravelRow.appendChild(TrFinishField);
            TravelRow.appendChild(editField);
            Traveltable.appendChild(TravelRow);
        });
    }
}
async function travelCreating(){
    let description = document.getElementById('description').value;
    let lngSP = document.getElementById('longitudeSP').value;
    let latSP = document.getElementById('latitudeSP').value;
    let startPoint = lngSP + ',' + latSP;
    let lngFP = document.getElementById('longitudeFP').value;
    let latFP = document.getElementById('latitudeFP').value;
    let finishPoint = lngFP + ',' + latFP;
    let stringDate = document.getElementById('dateStart').value;
    var startDate = new Date(stringDate);
    let isPrivate = document.getElementById('isPrivate').checked;
    let userID = new URLSearchParams(window.location.search).get('userid');
    let userName = new URLSearchParams(window.location.search).get('user');
    let travel = {description, startPoint, finishPoint, startDate, isPrivate, userID};
    
    

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
        window.location.href = `index.html?userid=${userID}&user=${userName}`;
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



