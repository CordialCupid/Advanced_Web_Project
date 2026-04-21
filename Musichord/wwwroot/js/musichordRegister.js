'use strict';

await main();

async function main() {
    await retrieveAccessToken();
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

// function to make post request to 'api/token' endpoint and retrieve access token
async function retrieveAccessToken() {
    const clientId = "ad61ff7774e443b99cf8123f95809301";
    const clientSecret = "a433dd0d69874f64b96d5a5cc193c496";
    const authBasic = btoa(clientId + ':' + clientSecret);
    const spotResponse = await fetch('https://accounts.spotify.com/api/token', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Authorization': 'Basic ' + authBasic,
        },
        body: new URLSearchParams({
            'code': retrieveCode(),
            'grant_type': 'authorization_code',
            'redirect_uri': 'http://127.0.0.1:5097/Identity/Account/Register'
        })
    })
    .then(async response => {
        await handleAuthResponse(response)})
    .catch(error => {
        console.log(error);
    });
}

async function handleAuthResponse(api_Response) {
    if (api_Response.ok) {
        const content = await api_Response.json();
        console.log(content.access_token);
        if (content.access_token != undefined) {
            let access_token = content.access_token;
            localStorage.setItem("access_token", access_token);
            const tokenInp = document.querySelector('.spotify-token');
            tokenInp.value = access_token;
        }
        if (content.refresh_token != undefined) {
            let refresh_token = content.refresh_token;
            localStorage.setItem("refresh_token", refresh_token);
        }
    } else {
        throw new Error('Status: ' + api_Response.status);
    }
}