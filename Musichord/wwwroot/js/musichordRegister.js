'use strict';

import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";

await main();

async function main() {
    const spotRepo = new SpotifyAJAXRepository();
    await spotRepo.retrieveAccessToken(retrieveCode());
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

