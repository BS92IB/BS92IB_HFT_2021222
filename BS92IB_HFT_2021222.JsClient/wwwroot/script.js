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
getShipsData();
getFleetsData();
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



async function getShipsData() {
    let response = await fetch(`${apiBase}/Ship`)
    let json = await response.json();
    console.log(json);
    ships = json;
    displayShips();
}

function displayShips() {
    let resultArea = document.getElementById('ship-resultarea');
    resultArea.innerHTML = "";
    ships.forEach(s => {
        const rowTemplate = `<tr>
        <td>${s.id}</td>
        <td>${s.fleetId}</td>
        <td>${s.name}</td>
        <td>${s.class}</td>
        <td>${s.hullType}</td>
        <td>${s.displacement}</td>
        <td>${s.length}</td>
        <td>${s.beam}</td>
        <td>${s.draft}</td>
        <td>${s.maxSpeedKnots}</td>
        <td><button type="button" class="btn btn-sm btn-info" data-bs-toggle="modal" data-bs-target="#ship-update-modal" data-ship-id="${s.id}"><i class="fas fa-edit"></i> Update</button>
        <button type="button" class="btn btn-sm btn-danger" onclick="deleteShip(${s.id})"><i class="fas fa-trash"></i> Delete</button></td>
        </tr>`;
        resultArea.innerHTML += rowTemplate;
    });
}

function createShip() {
    let fleetId = document.getElementById('ship-create-fleet-id').value;
    let name = document.getElementById('ship-create-name').value;
    let class_ = document.getElementById('ship-create-class').value;
    let hullType = document.getElementById('ship-create-hull-type').value;
    let displacement = document.getElementById('ship-create-displacement').value;
    let length = document.getElementById('ship-create-length').value;
    let beam = document.getElementById('ship-create-beam').value;
    let draft = document.getElementById('ship-create-draft').value;
    let maxSpeedKnots = document.getElementById('ship-create-speed').value;
    fetch(`${apiBase}/Ship`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            fleetId,
            name,
            class: class_,
            hullType,
            displacement,
            length,
            beam,
            draft,
            maxSpeedKnots
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getShipsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function deleteShip(id) {
    fetch(`${apiBase}/Ship/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    })
        .then(data => {
            console.log('Success: ', data)
            getShipsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function updateShip() {
    let fleetId = document.getElementById('ship-update-fleet-id').value;
    let name = document.getElementById('ship-update-name').value;
    let class_ = document.getElementById('ship-update-class').value;
    let hullType = document.getElementById('ship-update-hull-type').value;
    let displacement = document.getElementById('ship-update-displacement').value;
    let length = document.getElementById('ship-update-length').value;
    let beam = document.getElementById('ship-update-beam').value;
    let draft = document.getElementById('ship-update-draft').value;
    let maxSpeedKnots = document.getElementById('ship-update-speed').value;
    fetch(`${apiBase}/Ship`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            id: shipId,
            fleetId,
            name,
            class: class_,
            hullType,
            displacement,
            length,
            beam,
            draft,
            maxSpeedKnots
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getShipsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}


async function getFleetsData() {
    let response = await fetch(`${apiBase}/Fleet`)
    let json = await response.json();
    console.log(json);
    fleets = json;
    displayFleets();
}

function displayFleets() {
    let resultArea = document.getElementById('fleet-resultarea');
    resultArea.innerHTML = "";
    fleets.forEach(f => {
        const rowTemplate = `<tr>
        <td>${f.id}</td>
        <td>${f.name}</td>
        <td><button type="button" class="btn btn-sm btn-info" data-bs-toggle="modal" data-bs-target="#fleet-update-modal" data-fleet-id="${f.id}"><i class="fas fa-edit"></i> Update</button>
        <button type="button" class="btn btn-sm btn-danger" onclick="deleteFleet(${f.id})"><i class="fas fa-trash"></i> Delete</button></td>
        </tr>`;
        resultArea.innerHTML += rowTemplate;
    });
}

function createFleet() {
    let name = document.getElementById('fleet-create-name').value;
    fetch(`${apiBase}/Fleet`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            name            
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getFleetsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function deleteFleet(id) {
    fetch(`${apiBase}/Fleet/${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    })
        .then(data => {
            console.log('Success: ', data)
            getFleetsData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function updateFleet() {

    let name = document.getElementById('fleet-update-name').value;

    fetch(`${apiBase}/Fleet`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            id: fleetId,
            name            
        })
    })
        .then(data => {
            console.log('Success: ', data)
            getFleetsData();
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

    const shipUpdateModal = document.getElementById('ship-update-modal');
    if (shipUpdateModal) {
        shipUpdateModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget;
            shipId = button.getAttribute('data-ship-id');
            document.getElementById('ship-update-fleet-id').value = ships.find(s => s.id == shipId).fleetId;
            document.getElementById('ship-update-name').value = ships.find(s => s.id == shipId).name;
            document.getElementById('ship-update-class').value = ships.find(s => s.id == shipId).class;
            document.getElementById('ship-update-hull-type').value = ships.find(s => s.id == shipId).hullType;
            document.getElementById('ship-update-displacement').value = ships.find(s => s.id == shipId).displacement;
            document.getElementById('ship-update-length').value = ships.find(s => s.id == shipId).length;
            document.getElementById('ship-update-beam').value = ships.find(s => s.id == shipId).beam;
            document.getElementById('ship-update-draft').value = ships.find(s => s.id == shipId).draft;
            document.getElementById('ship-update-speed').value = ships.find(s => s.id == shipId).maxSpeedKnots;
        });
    }

    const fleetUpdateModal = document.getElementById('fleet-update-modal');
    if (fleetUpdateModal) {
        fleetUpdateModal.addEventListener('show.bs.modal', event => {
            const button = event.relatedTarget;
            fleetId = button.getAttribute('data-fleet-id');
            document.getElementById('fleet-update-name').value = fleets.find(f => f.id == fleetId).name;
        });
    }
}
