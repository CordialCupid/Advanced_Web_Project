'use strict';
import { SpotifyAJAXRepository } from "./spotifyAJAXRepo.js";
import { friendAJAXRepository } from "./friendAJAXRepository.js";

await main();

async function main() {
    const spotRepo = new SpotifyAJAXRepository();
    const friendRepo = new friendAJAXRepository('http://127.0.0.1:5097/api/friend');
    await getRecentlyPlayed(spotRepo);
    await getTopFive(spotRepo);
}

async function setUpEventHandlers(friendRepo) {
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
            window.location.reload();      
        }
    });

    document.addEventListener('click', async (e) => {
        const friendBtn = e.target.closest('#addFriendBtn');
        if (friendBtn) {
            const name = friendBtn.getAttribute('data-user-name');
            console.log(name);
            await friendRepo.createFriendship(name);
        }
    });

    document.addEventListener('click', async (e) => {
        const refreshBtn = e.target.closest('#manageIdentity');
        if (refreshBtn) {
            e.preventDefault();
            window.location.href = '/Identity/Account/Manage';
        }
    });
}
