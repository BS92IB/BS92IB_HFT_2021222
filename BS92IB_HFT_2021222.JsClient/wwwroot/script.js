let weapons = [];
const apiBase = 'http://localhost:21990';
let weaponId = -1;

getWeaponsData();
initModals();

async function getWeaponsData() {
    let response = await fetch(`${apiBase}/Weapon`)
    let json = await response.json();
    console.log(json);
    weapons = json;
    displayWeapons();
}

function displayWeapons() {
    let resultArea = document.getElementById('weapon-resultarea');
    resultArea.innerHTML = "";
    weapons.forEach(w => {
        const rowTemplate = `<tr>
        <td>${w.id}</td>
        <td>${w.designation}</td>
        <td>${w.weaponType}</td>
        <td><button type="button" class="btn btn-sm btn-info" data-bs-toggle="modal" data-bs-target="#weapon-update-modal" data-weapon-id="${w.id}"><i class="fas fa-edit"></i> Update</button>
        <button type="button" class="btn btn-sm btn-danger" onclick="deleteWeapon(${w.id})"><i class="fas fa-trash"></i> Delete</button></td>
        </tr>`;
        resultArea.innerHTML += rowTemplate;
    });
}

function createWeapon() {
    let designation = document.getElementById('weapon-create-designation').value;
    let weaponType = document.getElementById('weapon-create-type').value;
    fetch(`${apiBase}/Weapon`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            designation,
            weaponType
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getWeaponsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function deleteWeapon(id) {
    fetch(`${apiBase}/Weapon/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    })
        .then(data => {
            console.log('Success: ', data)
            getWeaponsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function updateWeapon() {

    let designation = document.getElementById('weapon-update-designation').value;
    let weaponType = document.getElementById('weapon-update-type').value;           

    fetch(`${apiBase}/Weapon`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            id: weaponId,
            designation,
            weaponType
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getWeaponsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function initModals() {
    const weaponUpdateModal = document.getElementById('weapon-update-modal');
    if (weaponUpdateModal) {
        weaponUpdateModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget;
            weaponId = button.getAttribute('data-weapon-id');
            document.getElementById('weapon-update-designation').value = weapons.find(w => w.id == weaponId).designation;
            document.getElementById('weapon-update-type').value = weapons.find(w => w.id == weaponId).weaponType;            
        });
    }
}
