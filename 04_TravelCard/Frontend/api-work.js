async function loadTravels(){
    //alert('Hello');
    let result = await fetch("http://localhost:5100/Travel");
	let Traveltable = document.getElementById('Travels');
    console.log(Traveltable);
    if(result.ok){
        let travelss = await result.json();
        travelss.forEach(travel => {
            alert(JSON.stringify(travel));
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