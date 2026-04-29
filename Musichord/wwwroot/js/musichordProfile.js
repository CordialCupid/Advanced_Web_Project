'use strict';
import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";
import { friendAJAXRepository } from "./friendAJAXRepository.js";

await main();

async function main() {
    const spotRepo = new SpotifyAJAXRepository();
    const friendRepo = new friendAJAXRepository('http://127.0.0.1:5097/api/friend');

    await setUpEventHandlers(friendRepo);
}

async function setUpEventHandlers(friendRepo, spotRepo) {
    document.addEventListener('click', async (e) => {
        const refreshBtn = e.target.closest('#refreshFive');
        const token = localStorage.getItem("access_token")
        const address = 'http://127.0.0.1:5097/api/spotify/topfive/' + token;
        if (refreshBtn) {
            e.preventDefault();
            const response = await fetch(address);  
            if (!response.ok) {
                await spotRepo.refreshAccessToken()
                .then(await fetch(address));
            }          
        }
    });
}