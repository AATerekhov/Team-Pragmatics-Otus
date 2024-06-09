async function loadPlaceType(){
    let result =await fetch("http://localhost:52199/api/PlaceType");
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
           link.href = `editPlaceType.html?id=${element.id}`;
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
async function loadPlace(idPlaceType)
{
    let result =await fetch(`http://localhost:52199/api/Place/${idPlaceType}`);
    let table = document.getElementById('places');
    if(result.ok){
        let places = await result.json();
        places.forEach(element => {
           let placetypeString = document.createElement('tr');
           let idField = document.createElement('td');
           idField.innerText = element.id;
           let nameField = document.createElement('td');
           nameField.innerText = element.name;
           let descriptionField = document.createElement('td');
           descriptionField.innerText = element.description;
           let longitudeField = document.createElement('td');
           longitudeField.innerText = element.longitude;
           let latitudeField = document.createElement('td');
           latitudeField.innerText = element.latitude;
           placetypeString.appendChild(idField);
           placetypeString.appendChild(nameField);
           placetypeString.appendChild(descriptionField);
           placetypeString.appendChild(longitudeField);
           placetypeString.appendChild(latitudeField);
           table.appendChild(placetypeString);
        });
    }
    console.log(result);
}
async function createPlaceType(){
    let name = document.getElementById('name').value;
    let description = document.getElementById('description').value;
    let placeType = {name, description};
    let result = await fetch('http://localhost:52199/api/PlaceType',{
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(placeType)
    });
    if(result.ok)
        window.location.href = "index.html";
}

async function getPlaceTypeByIdAsync(id){
    let rezult = await fetch(`http://localhost:52199/api/PlaceType/${id}`)
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
    let result = await fetch(`http://localhost:52199/api/PlaceType/${id}`,{
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(placeType)
    });
    if(result.ok)
        window.location.href = "index.html";
}

async function initMap() {
    await ymaps3.ready;

    const {YMap, YMapDefaultSchemeLayer, YMapDefaultFeaturesLayer} = ymaps3;

    const markersGeoJsonSource = 
        {
          coordinates: [30.314997, 59.938784 ],
          title: 'Санкт-Петербург',
          subtitle: 'Город на берегу Невы. <br> Культурная сталица России.',
          color: '#00CC00'
        }

    const map = new YMap(
        document.getElementById('map'),
        {
            location: {
                center: [30.314997, 59.938784 ],
                zoom: 10
            },
        behaviors: ['drag', 'scrollZoom', 'pinchZoom', 'dblClick']
        }
    );

    // Добавьте слой с дорогами и зданиями
    map.addChild(new YMapDefaultSchemeLayer());

    // Добавьте слой для маркеров
    map.addChild(new YMapDefaultFeaturesLayer({zIndex: 1800}));

    // Import the package to add a default marker
    const {YMapDefaultMarker} = await ymaps3.import('@yandex/ymaps3-markers@0.0.1');

    // Создайте DOM-элемент для содержимого маркера.
    // Важно это сделать до инициализации маркера!
    // Элемент можно создавать пустым. Добавить HTML-разметку внутрь можно после инициализации маркера.
    const content = document.createElement('section');

    // Инициализируйте маркер
    // const marker = new YMapMarker(
    //   {
    //     coordinates: [30.314997, 59.938784 ],
    //     draggable: true
    //   },
    //   content
    // );
    // Добавьте маркер на карту
    // map.addChild(marker); 
      // Create default markers and add them to the map
      const marker = new YMapDefaultMarker(markersGeoJsonSource);
      map.addChild(marker);
  
    // Добавьте произвольную HTML-разметку внутрь содержимого маркера
    // content.innerHTML = '<h3>Новое место.</h3>';      
}