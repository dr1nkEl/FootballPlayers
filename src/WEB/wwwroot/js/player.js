/**
 * Make request function.
 * @async
 * @function
 * @param url {string} - URL.
 * @param data {any} - Data.
 * @returns {any} - Some data from result.
 * */
async function makeRequest(url, data, method) {
    var formdata = new FormData();

    for (var key in data) {
        formdata.append(key, data[key]);
    }

    return await $.ajax({
        type: method,
        url: url,
        data: formdata,
        contentType: false,
        processData: false,
        error: function (err) {
            alert('Failed to proceed request!');
        },
    });
}

/**
 * Updates player list.
 * @function
 * @async
 * @param {string} id - DOM parent ID.
 */
async function updatePlayerList(id) {
    const players = await makeRequest('/api/player/list', {}, 'GET');
    $.each(players, function () {
        const player = $(this)[0];
        addPlayer(id, player);
    });
    setCountries();
    setTeams();
    setGenders();
}

/**
 * Function to add player to DOM.
 * @param {string} id - DOM parent ID.
 * @param {any} player - Player obj.
 */
function addPlayer(id, player) {
    $(`#${id}`).append(`
            <div class="d-flex-row">
                <label for="nameInput${player.id}">Name</label>
                <input id="nameInput${player.id}" value="${player.name}" disabled/>

                <label for="surnameInput${player.id}">Surname</label>
                <input id="surnameInput${player.id}" value="${player.surname}" disabled/>

                <label for="birthday${player.id}">Birthday</label>
                <input type="date" id="birthday${player.id}" value="${player.birthDay.split('T')[0]}" disabled/>

                <label>Gender</label>
                <select id="gender${player.id}" class="gender" style="width:10%;" data-value="${player.gender}" disabled>
                </select>

                <label for="team${player.id}">Team</label>
                <select id="team${player.id}" class="team" style="width:10%;" data-value="${player.teamId}" disabled>
                    
                </select>

                <label for="country${player.id}">Country</label>
                <select id="country${player.id}" class="country" style="width:10%;" data-value="${player.country}" disabled>

                </select>
                <button id="editBtn${player.id}" type="button" class="btn btn-primary" onclick="editPlayer(${player.id})" data-state="disabled">Edit</button>
            </div>`)
}

/**
 * Function to edit player.
 * @async
 * @function
 * @param {number} id - Player ID.
 */
async function editPlayer(id) {
    const button = $(`#editBtn${id}`);
    const country = $(`#country${id}`);
    const name = $(`#nameInput${id}`);
    const surname = $(`#surnameInput${id}`);
    const birthday = $(`#birthday${id}`);
    const team = $(`#team${id}`);
    const teamSelected = $(`#team${id} option:selected`);
    const gender = $(`#gender${id}`);

    if (button.data('state') == 'enabled') {
        let player = {
            name: name.val(),
            surname: surname.val(),
            country: country.val(),
            birthDay: birthday.val(),
            teamName: teamSelected.text(),
            id: id,
            gender: gender.val(),
        }

        country.attr('disabled', 'disabled');
        name.attr('disabled', 'disabled');
        surname.attr('disabled', 'disabled');
        birthday.attr('disabled', 'disabled');
        team.attr('disabled', 'disabled');
        gender.attr('disabled', 'disabled');
        button.data('state', 'disabled');
        await makeRequest('/api/player/patch', player, 'PATCH');
        hubConnection.invoke('Update', id);
    } else {
        button.data('state', 'enabled');
        country.removeAttr('disabled');
        name.removeAttr('disabled');
        surname.removeAttr('disabled');
        birthday.removeAttr('disabled');
        team.removeAttr('disabled');
        gender.removeAttr('disabled');
    }
}

/**
 * Function to set countries.
 * @async
 * @function
 * */
async function setCountries() {
    const countriesData = await makeRequest('/api/player/countries', {}, 'GET');
    const countries = $('.country');
    $.each(countries, function () {
        const countryDom = $(this);
        countryDom.empty();
        $.each(countriesData, function () {
            const country = $(this)[0];
            countryDom.append(`<option value=${country.value}>${country.text}</option>`)
        });
        countryDom.val(countryDom.data('value'));
    });
}

/**
 * Function to set genders.
 * @async
 * @function
 * */
async function setGenders() {
    const gendersData = await makeRequest('/api/player/genders', {}, 'GET');
    const genders = $('.gender');
    $.each(genders, function () {
        const genderDom = $(this);
        genderDom.empty();
        $.each(gendersData, function () {
            const gender = $(this)[0];
            genderDom.append(`<option value=${gender.value}>${gender.text}</option>`)
        });
        genderDom.val(genderDom.data('value'));
    });
}

/**
 * Function to set teams.
 * @async
 * @function
 * */
async function setTeams() {
    const teamsData = await makeRequest('/api/team/list', {}, 'GET');
    const teams = $('.team');

    $.each(teams, function () {
        const teamDom = $(this);
        teamDom.empty();
        $.each(teamsData, function () {
            const team = $(this)[0];
            teamDom.append(`<option value="${team.id}">${team.name}</option>`);
        })
        teamDom.val(teamDom.data('value'));
        teamDom.select2({
            tags: true,
        });
    });
}

/**
 * Function to update player.
 * @async
 * @function
 * @param {any} player - Player obj.
 */
async function updatePlayer(player) {
    const id = player.id;
    $(`#country${id}`).val(player.country);
    $(`#country${id}`).data('value', player.country);
    $(`#gender${id}`).data('value', player.gender);
    $(`#gender${id}`).val(player.gender);
    $(`#nameInput${id}`).val(player.name);
    $(`#surnameInput${id}`).val(player.surname);
    $(`#birthday${id}`).val(player.birthDay.split('T')[0]);
    $(`#team${id}`).data('value', player.teamId);
    await setTeams();
}