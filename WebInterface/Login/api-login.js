async function CheckUser() {
    
    let name = document.getElementById('loginValue').value;
    let password = document.getElementById('passwordValue').value;
    let user = {name,password};
    let result = await fetch(`http://localhost:5200/api/user/Authorization`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'},
        body: JSON.stringify(user)
    });
    if(result.ok){
        let newUser = await result.json();            
        window.location.href = `../Travel/index.html?userid=${newUser.id}&user=${newUser.name}`;
        //переход на строницу своих путешествий.
        //на публичные путешествия доступ должен быть всегда.
    }
    else{
        await ToEnter();
    }
}
async function RegisterUser() {
    window.location.href = `registerUser.html`;
}

async function CreateUser(){
    let name = document.getElementById('loginValue').value;
    let password = document.getElementById('passwordValue').value;
    let email = document.getElementById('emailValue').value;
    let dateRegistration = new Date();
    let user = {name,password,dateRegistration,email};
    let result = await fetch(`http://localhost:5200/api/user`,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'TM-API-Key': 'F504ED6B-68AA-456C-B839-C1559ACED2EF'},
        body: JSON.stringify(user)
    });
    if(result.ok){
        await ToEnter();
    }
    else{
        await RegisterUser();
    }
}

async function ToEnter(){
    window.location.href = `login.html`;
}