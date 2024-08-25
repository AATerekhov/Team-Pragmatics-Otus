async function loadPlaceType(){
    let result =await fetch("http://localhost:5200/api/placetypes",{
        method: 'GET',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    let table = document.getElementById('placeTypes');
    if(result.ok){
        let placetypes = await result.json();
        placetypes.forEach(element => {
           let placetypeString = document.createElement('tr');
           let idField = document.createElement('td');
           idField.innerText = element.id;
           let nameField = document.createElement('td');
           nameField.innerText = element.name;
           let descriptionField = document.createElement('td');
           descriptionField.innerText = element.description;
           let link = document.createElement('a');
           link.innerText = "Редактировать";
           link.href = `editPlaceType.html?LngLat=30.31499,59.938784&Scale=10&id=${element.id}`;
           let editField = document.createElement('td');
           editField.appendChild(link);
           placetypeString.appendChild(idField);
           placetypeString.appendChild(nameField);
           placetypeString.appendChild(descriptionField);
           placetypeString.appendChild(editField);
           table.appendChild(placetypeString);
        });
    }
    console.log(result);
}
async function loadPlace()
{
    await ymaps3.ready;
    const {YMap, YMapDefaultSchemeLayer, YMapDefaultFeaturesLayer} = ymaps3;    
    const {YMapDefaultMarker} = await ymaps3.import('@yandex/ymaps3-markers@0.0.1'); 
    
    let centerPoint = new URLSearchParams(window.location.search).get('LngLat').split(","); 
    let scale = new URLSearchParams(window.location.search).get('Scale');
    const map = new YMap(
        document.getElementById('map'),
        {
            location: {
                center: centerPoint,
                zoom: scale
            },
            behaviors: ['drag', 'scrollZoom', 'pinchZoom', 'dblClick'],
            showScaleInCopyrights: true
        }
    );
    map.addChild(new YMapDefaultSchemeLayer());
    map.addChild(new YMapDefaultFeaturesLayer({zIndex: 1800}));      

    let idPlaceType = new URLSearchParams(window.location.search).get('id');
    let result =await fetch(`http://localhost:5200/api/places/${idPlaceType}`,{
        method: 'GET',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    //let table = document.getElementById('places');
    if(result.ok){
        let places = await result.json();
        places.forEach(element => {   
           let insertPoint = [element.longitude,element.latitude];
           const draggableMarker  = new YMapDefaultMarker({
            coordinates: insertPoint,
            //title: `${element.name}`,
            //subtitle: 'Place',
            color: '#00CC00',
            popup: {content: `${element.name}<br> 
                ${getComment(element.description)}
                <a href=updatePlace.html?LngLat=${element.longitude},${element.latitude}&Scale=17&id=${element.id}&type=${element.placeTypeId}>Редактировать</a>
                `, position: 'left'}
          });
          map.addChild(draggableMarker ); 
        });
    }
    
    function getComment(descriptions) {
        let list = descriptions.split(" |");
        let result = "";
        list.forEach(element => {
            result =  `${result}${element}<br>`;
        });
        return result;
    }

    console.log(result);
}
async function createPlaceType(){
    let name = document.getElementById('name').value;
    let description = document.getElementById('description').value;
    let placeType = {name, description};
    let result = await fetch('http://localhost:5200/api/placetype',{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'},
        body: JSON.stringify(placeType)
    });
    if(result.ok)
        window.location.href = "index.html";
}
async function getPlaceTypeByIdAsync(id){
    let rezult = await fetch(`http://localhost:5200/api/placetype/${id}`,{
        method: 'GET',
        headers: {'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'}
    });
    if(rezult.ok)
        return await rezult.json();
}
async function updatePlaceTypeLoad(){
    let id = new URLSearchParams(window.location.search).get('id');
    let selectPlaceType = await getPlaceTypeByIdAsync(id);
    let nameField = document.getElementById('name');
    let descriptionField = document.getElementById('description');
    nameField.value = selectPlaceType.name;
    descriptionField.value = selectPlaceType.description;
}
async function updatePlaceType(){
    let id = new URLSearchParams(window.location.search).get('id');
    let description = document.getElementById('description').value;
    let placeType = {description};
    let result = await fetch(`http://localhost:5200/api/placetype/${id}`,{
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'
        },
        body: JSON.stringify(placeType)
    });
    if(result.ok)
        window.location.href = "index.html";
}
//Yandex Map function
async function initMap() {
    await ymaps3.ready;
    const {YMap, YMapDefaultSchemeLayer, YMapDefaultFeaturesLayer} = ymaps3;   
    let centerPoint = new URLSearchParams(window.location.search).get('LngLat').split(","); 
    let scale = new URLSearchParams(window.location.search).get('Scale');

    let lng = document.getElementById('longitude');
    lng.value = centerPoint[0];
    let lat = document.getElementById('latitude');
    lat.value = centerPoint[1];

        function onDragMoveHandler(coordinates) { 
            const longitude = `Longitude: ${coordinates[0].toFixed(6)}`; 
            const latitude = `Latitude: ${coordinates[1].toFixed(6)}`;                     
            lng.value = coordinates[0].toFixed(6);            
            lat.value = coordinates[1].toFixed(6);
            draggableMarker.update({coordinates, title: `${longitude} <br> ${latitude} `});
        }

    const map = new YMap(
        document.getElementById('map'),
        {
            location: {
                center: centerPoint,
                zoom: scale
            },
            behaviors: ['drag', 'scrollZoom', 'pinchZoom', 'dblClick'],
            showScaleInCopyrights: true
        }
    );

    // Добавьте слой с дорогами и зданиями
    map.addChild(new YMapDefaultSchemeLayer());

    // Добавьте слой для маркеров
    map.addChild(new YMapDefaultFeaturesLayer({zIndex: 1800}));

    // Import the package to add a default marker
    const {YMapDefaultMarker} = await ymaps3.import('@yandex/ymaps3-markers@0.0.1');
  
      // Create default markers and add them to the map
      const draggableMarker  = new YMapDefaultMarker({
        coordinates: centerPoint,
        title: `Longitude: ${centerPoint[0]} <br>
                Latitude: ${centerPoint[1]}`,
        subtitle: 'Укажите новое место. <br> Перетащите метку.',
        color: '#00CC00',
        draggable: true,
        onDragMove: onDragMoveHandler
      });
      map.addChild(draggableMarker );  
      
      
}

async function searchPointPlace()
{   
    let coordinates = document.getElementById('LngLat').value;    
    let idPlaceType = new URLSearchParams(window.location.search).get('id');
    window.location.href = `createPlace.html?LngLat=${coordinates}&Scale=17&id=${idPlaceType}`;
}

async function createPlaceForma()
{
    let idPlaceType = new URLSearchParams(window.location.search).get('id');
    window.location.href = `createPlace.html?LngLat=30.31499,59.938784&Scale=10&id=${idPlaceType}`;
}
async function createPlace()
{
    let placeTypeId = Number(new URLSearchParams(window.location.search).get('id'));
    let name = document.getElementById('name').value;
    let description = document.getElementById('description').value;
    let longitude = Number(document.getElementById('longitude').value);
    let latitude = Number(document.getElementById('latitude').value);
    let place = {placeTypeId, name, description,longitude, latitude};
    let result = await fetch('http://localhost:5200/api/place',{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'},
        body: JSON.stringify(place)
    });
    if(result.ok)
        window.location.href = `editPlaceType.html?LngLat=30.31499,59.938784&Scale=10&id=${placeTypeId}`;        
}
class CustomMarkerWithPopup extends YMapComplexEntity {
      _marker;
     _popup;
    // Обработчик для прикрепления элемента управления к карте
    _onAttach() {
        this._createMarker();
    }
    // Обработчик для отсоединения элемента управления от карты
    _onDetach() {
        this._marker = null;
    }
    // Обработчик для обновления свойств маркера
    _onUpdate(props) {
        if (props.zIndex !== undefined) {
            this._marker?.update({zIndex: props.zIndex});
        }
        if (props.coordinates !== undefined) {
            this._marker?.update({coordinates: props.coordinates});
        }
    }
    // Способ создания маркерного элемента
    _createMarker() {
        const element = document.createElement('div');
        element.className = 'marker';
        element.onclick = () => {
            this._openPopup();
        };

        this._marker = new YMapMarker({coordinates: this._props.coordinates}, element);
        this.addChild(this._marker);
    }

    // Способ создания элемента всплывающего окна
    _openPopup() {
        if (this._popup) {
            return;
        }

        const element = document.createElement('div');
        element.className = 'popup';

        const textElement = document.createElement('div');
        textElement.className = 'popup__text';
        textElement.textContent = this._props.popupContent;

        const closeBtn = document.createElement('button');
        closeBtn.className = 'popup__close';
        closeBtn.textContent = 'Close Popup';
        closeBtn.onclick = () => this._closePopup();

        element.append(textElement, closeBtn);

        const zIndex = (this._props.zIndex ?? YMapMarker.defaultProps.zIndex) + 1_000;
        this._popup = new YMapMarker({
            coordinates: this._props.coordinates,
            zIndex,
            // Это позволяет вам прокручивать всплывающее окно
            blockBehaviors: this._props.blockBehaviors
        }, element);
        this.addChild(this._popup);
    }

    _closePopup() {
        if (!this._popup) {
            return;
        }

        this.removeChild(this._popup);
        this._popup = null;
    }
}
async function returnPlaceTypes(){
    window.location.href = "index.html";
}