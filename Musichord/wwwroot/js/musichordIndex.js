'use strict';
import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";
import { friendAJAXRepository } from "./friendAJAXRepository.js";

await main();

async function main() {
    const spotRepo = new SpotifyAJAXRepository();
    const friendRepo = new friendAJAXRepository('http://127.0.0.1:5097/api/friend');
    await getRecentlyPlayed(spotRepo);
    await getTopFive(spotRepo);
    await setUpEventHandlers(friendRepo);
}

async function getRecentlyPlayed(spotRepo) {
    const token = localStorage.getItem("access_token")
    const address = 'http://127.0.0.1:5097/api/spotify/recently-played/' + token;
    const response = await fetch(address);
    if (!response.ok)
    {
        await spotRepo.refreshAccessToken()
            .then(await fetch(address));
        
    }
}

async function getTopFive(spotRepo) {
    const token = localStorage.getItem("access_token")
    const address = 'http://127.0.0.1:5097/api/spotify/topfive/' + token;
    const response = await fetch(address);
    if (!response.ok)
    {
        await spotRepo.refreshAccessToken()
            .then(await fetch(address));
    }
}


