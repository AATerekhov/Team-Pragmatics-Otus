async function loadPlaceType(){
    let result =await fetch("http://localhost:49759/api/PlaceType");
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

           placetypeString.appendChild(idField);
           placetypeString.appendChild(nameField);
           placetypeString.appendChild(descriptionField);
           table.appendChild(placetypeString);
        });
    }
    console.log(result);
}
async function loadPlace(idPlaceType)
{
    let result =await fetch(`http://localhost:49759/api/Place/${idPlaceType}`);
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
    let result = await fetch('http://localhost:49759/api/PlaceType',{
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(placeType)
    });
    if(result.ok)
        window.location.href = "index.html";
}