let weapons = [];
let armaments = [];
let ships = [];
let fleets = [];
const apiBase = 'http://localhost:21990';
let weaponId = -1;
let armamentId = -1;
let shipId = -1;
let fleetId = -1;

getWeaponsData();
getArmamentsData();
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



async function getArmamentsData() {
    let response = await fetch(`${apiBase}/Armament`)
    let json = await response.json();
    console.log(json);
    armaments = json;
    displayArmaments();
}

function displayArmaments() {
    let resultArea = document.getElementById('armament-resultarea');
    resultArea.innerHTML = "";
    armaments.forEach(a => {
        const rowTemplate = `<tr>
        <td>${a.id}</td>
        <td>${a.shipId}</td>
        <td>${a.name}</td>
        <td>${a.weaponId}</td>
        <td>${a.quantity}</td>
        <td><button type="button" class="btn btn-sm btn-info" data-bs-toggle="modal" data-bs-target="#armament-update-modal" data-armament-id="${a.id}"><i class="fas fa-edit"></i> Update</button>
        <button type="button" class="btn btn-sm btn-danger" onclick="deleteArmament(${a.id})"><i class="fas fa-trash"></i> Delete</button></td>
        </tr>`;
        resultArea.innerHTML += rowTemplate;
    });
}

function createArmament() {
    let shipId = document.getElementById('armament-create-ship-id').value;
    let name = document.getElementById('armament-create-name').value;
    let weaponId = document.getElementById('armament-create-weapon-id').value;
    let quantity = document.getElementById('armament-create-quantity').value;
    fetch(`${apiBase}/Armament`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            shipId,
            name,
            weaponId,
            quantity
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getArmamentsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function deleteArmament(id) {
    fetch(`${apiBase}/Armament/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    })
        .then(data => {
            console.log('Success: ', data)
            getArmamentsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function updateArmament() {
    let shipId = document.getElementById('armament-update-ship-id').value;
    let name = document.getElementById('armament-update-name').value;
    let weaponId = document.getElementById('armament-update-weapon-id').value;
    let quantity = document.getElementById('armament-update-quantity').value;
    fetch(`${apiBase}/Armament`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            id: armamentId,
            shipId,
            name,
            weaponId,
            quantity
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getArmamentsData();
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

    const armamentUpdateModal = document.getElementById('armament-update-modal');
    if (armamentUpdateModal) {
        armamentUpdateModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget;
            armamentId = button.getAttribute('data-armament-id');
            document.getElementById('armament-update-ship-id').value = armaments.find(a => a.id == armamentId).shipId;
            document.getElementById('armament-update-name').value = armaments.find(a => a.id == armamentId).name;
            document.getElementById('armament-update-weapon-id').value = armaments.find(a => a.id == armamentId).weaponId;
            document.getElementById('armament-update-quantity').value = armaments.find(a => a.id == armamentId).quantity;
        });
    }
}
