'use strict';

import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";

await main();

async function main() {
    const code = retrieveCode();
    const spotRepo = new SpotifyAJAXRepository(); 
    const registerBtn = document.getElementById('registerSubmit');
    const addBtn = document.getElementById('spotifyAdd');
    
    await setUpEventHandlers(spotRepo);
    console.log(code);
    if (code != null) {
        await spotRepo.retrieveAccessToken(code)
            .then(async function() {
                const accessToken = localStorage.getItem('access_token');
                const spotResponse = await spotRepo.callSpotAPI('GET', '/v1/me', accessToken);             
                document.getElementById('spotUser').value = spotResponse.display_name;
                document.getElementById('spotEmail').value = spotResponse.email;
                const profilePicInp = document.querySelector('.profile-picture');
                const profilePicUrl = spotResponse.images && spotResponse.images.length > 0 ? spotResponse.images[0].url : '';
                profilePicInp.value = profilePicUrl;
                const tokenInp = document.querySelector('.spotify-token');
                tokenInp.value = accessToken;
                addBtn.disabled = true;
                registerBtn.disabled = false;
            })
            .catch (error => {
                console.log(error);
            })
    }
}

function retrieveCode() {
    // grab url for 'code' for use with authentication and parse the value of the code parameter from the url
    let code = null;

    const queryString = window.location.search;
    if (queryString.length > 0) {
        const urlParams = new URLSearchParams(queryString);
        code = urlParams.get('code');
    }
    return code;
}

async function setUpEventHandlers(spotRepo) {
    document.addEventListener('click', async (e) => {
        const addBtn = e.target.closest('#spotifyAdd');
        if (addBtn) {
            e.preventDefault();
            await spotRepo.requestAuth();             
        }
    });
}

