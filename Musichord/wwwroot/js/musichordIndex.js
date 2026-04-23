'use strict';

import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";

await main();

async function main() {
    const spotRepo = new SpotifyAJAXRepository();
    await setUpEventHandlers(spotRepo);
    //console.log(spotRepo.retrieveTopFive());
}

async function setUpEventHandlers(spotRepo) {
}