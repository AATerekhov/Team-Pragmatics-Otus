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
    let startPoint = document.getElementById('startPoint').value;
    let finishPoint = document.getElementById('finishPoint').value;
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
    const {YMap, YMapDefaultSchemeLayer} = ymaps3;
    // Иницилиазируем карту
    const map = new YMap(
        // Передаём ссылку на HTMLElement контейнера
        document.getElementById('travelmap'),
        // Передаём параметры инициализации карты
        {
            location: {
                // Координаты центра карты
                center: [37.588144, 55.733842],
                // Уровень масштабирования
                zoom: 10
            }
        }
    );
    // Добавляем слой для отображения схематической карты
    map.addChild(new YMapDefaultSchemeLayer()); 
}