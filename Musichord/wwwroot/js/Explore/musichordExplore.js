'use strict';

import { friendAJAXRepository } from "../friendAJAXRepository.js";
import { ExploreDOM } from "./musichordExploreDOM.js";

await main();

async function main() {
    const friendRepo = new friendAJAXRepository('http://127.0.0.1:5097/api/friend');
    ExploreDOM.showNoUsersMessage();
    ExploreDOM.showNoActivityMessage();

    await setUpEventHandlers(friendRepo);

    let users = await friendRepo.readAll();
    console.log(users);
    let records = await friendRepo.getNonActivity();
    ExploreDOM.showUserCards(users);
    ExploreDOM.showUserActivity(records);
}



async function setUpEventHandlers(friendRepo) {

    document.addEventListener('click', async (e) => {
        const friendBtn = e.target.closest('#addFriendBtn');
        if (friendBtn) {
            const name = friendBtn.getAttribute('data-user-handle');
            console.log(name);
            await friendRepo.createFriendship(name);
        }
    });
}