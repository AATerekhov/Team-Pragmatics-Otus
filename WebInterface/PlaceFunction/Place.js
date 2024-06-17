async function updatePlace(){
    let id = new URLSearchParams(window.location.search).get('id');
    let type = new URLSearchParams(window.location.search).get('type');

    let name = document.getElementById('name').value;
    let description = document.getElementById('description').value;
    let longitude = Number(document.getElementById('longitude').value);
    let latitude = Number(document.getElementById('latitude').value);
    let place = {name,description,longitude,latitude};
    let result = await fetch(`http://localhost:52199/api/Place/${id}`,{
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(place)
    });
    if(result.ok)
        window.location.href = `editPlaceType.html?LngLat=30.31499,59.938784&Scale=10&id=${type}`;
}

async function updatePointPlace()
{   
    let coordinates = document.getElementById('LngLat').value;    
    let id = new URLSearchParams(window.location.search).get('id');
    let type = new URLSearchParams(window.location.search).get('type');
    window.location.href = `updatePlace.html?LngLat=${coordinates}&Scale=17&id=${id}&type=${type}`;
}

async function updatePlaceLoad(){
    let id = new URLSearchParams(window.location.search).get('id');
    let selectPlace = await getPlaceByGuidAsync(id);
    let nameField = document.getElementById('name');
    let descriptionField = document.getElementById('description');
    nameField.value = selectPlace.name;
    descriptionField.value = selectPlace.description;
}

async function getPlaceByGuidAsync(id){
    let rezult = await fetch(`http://localhost:52199/api/Place/${id}`)
    if(rezult.ok)
        return await rezult.json();
}