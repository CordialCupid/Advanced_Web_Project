'use strict';
import { friendAJAXRepository } from "./friendAJAXRepository.js";

await main();

async function main() {
    const friendRepo = new friendAJAXRepository('http://127.0.0.1:5097/api/friend');
    await setUpEventHandlers(friendRepo);
}

async function setUpEventHandlers(friendRepo) {

    document.addEventListener('click', async (e) => {
        const friendBtn = e.target.closest('.addFriendBtn');
        if (friendBtn) {
            const name = friendBtn.getAttribute('data-user-name');
            console.log(name);
            await friendRepo.createFriendship(name);
        }
    });    
    
    document.addEventListener('click', async (e) => {
        const viewFriendBtn = e.target.closest('.viewFriendBtn');
        if (viewFriendBtn) {
            const name = viewFriendBtn.getAttribute('data-user-name');
            console.log(name);
            window.location.href = '/Profile/' + name;
        }
        });

    document.addEventListener('click', async (e) => {
        const acceptFriendReqBtn = e.target.closest('.acceptFriendReqBtn');

        if (acceptFriendReqBtn) {
            const name = acceptFriendReqBtn.getAttribute('data-user-name');
            console.log(name);
            try {
                await friendRepo.updateFriendshipStatus(name);
                console.log('Update successful, reloading...');
                window.location.reload();
            } catch (err) {
                console.error('Error updating friendship status:', err);
            }
        }
    });

    // declineFriendReqBtn
    document.addEventListener('click', async (e) => {
        const declineFriendReqBtn = e.target.closest('.declineFriendReqBtn');

        if (declineFriendReqBtn) {
            const name = declineFriendReqBtn.getAttribute('data-user-name');
            console.log(name);
            try {
                await friendRepo.deleteFriendship(name);
                console.log('Delete successful, reloading...');
                window.location.reload();
            } catch (err) {
                console.error('Error deleting friendship:', err);
                window.location.reload();
            }
        }
    });
}



