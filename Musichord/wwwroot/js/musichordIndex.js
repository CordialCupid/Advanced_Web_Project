'use strict';

import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";

await main();

async function main() {
    const spotRepo = new SpotifyAJAXRepository();
    await setUpEventHandlers(spotRepo);
}

async function setUpEventHandlers(spotRepo) {
    document.addEventListener('click', async (e) => {
        const registerBtn = e.target.closest('#register');
        if (registerBtn) {
            e.preventDefault();
            spotRepo.requestAuth();
        }
    });
}