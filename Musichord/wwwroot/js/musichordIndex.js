'use strict';
import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";

await main();

async function main() {
    const spotRepo = new SpotifyAJAXRepository();
    await spotRepo.refreshAccessToken();
    await setUpEventHandlers();
}

async function setUpEventHandlers() {
    document.addEventListener('click', async (e) => {
        const refreshBtn = e.target.closest('#refreshFive');
        const token = localStorage.getItem("access_token")
        const address = 'http://127.0.0.1:5097/api/spotify/topfive/' + token;
        if (refreshBtn) {
            e.preventDefault();
            const response = await fetch(address);  
            if (!response.ok) {
                throw new Error("There was an HTTP error getting the top five data.");
            }          
        }
    });
}