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
    let records = await friendRepo.getNonActivity();
    ExploreDOM.showUserCards(users);
    ExploreDOM.showUserActivity(records);
}



async function setUpEventHandlers(friendRepo) {

    document.addEventListener('click', async (e) => {
        const friendBtn = e.target.closest('.add-btn');
        if (friendBtn) {
            const name = friendBtn.getAttribute('data-user-handle');
            console.log(name);
            await friendRepo.createFriendship(name);
            let users = await friendRepo.readAll();
            let records = await friendRepo.getNonActivity();
            ExploreDOM.showUserCards(users);
            ExploreDOM.showUserActivity(records);
        }
    });

    document.addEventListener('click', async (e) => {
        const viewBtn = e.target.closest('.view-btn');
        if (viewBtn) {
            const handle = viewBtn.getAttribute('data-user-handle');
            window.location.href = 'http://127.0.0.1:5097/Profile/' + handle;
        }
    });

}