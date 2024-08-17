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
    if(result.ok)
        return result.json();
}