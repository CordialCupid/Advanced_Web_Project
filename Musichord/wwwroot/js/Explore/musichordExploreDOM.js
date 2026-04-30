'use strict';

export class ExploreDOM {
    static showNoUsersMessage() {
        const emptyState = document.querySelector('#emptyState');
        emptyState.setAttribute('style', 'display: block;');
    }

    static hideNoUsersMessage() {
        const emptyState = document.querySelector('#emptyState');
        emptyState.setAttribute('style', 'display: none;');
    }

    static showUserCards(users) {
        const userContainer = document.querySelector('#userContainer');
        userContainer.innerHTML = "";
        if (users.length === 0) {
            ExploreDOM.showNoUsersMessage();
        } else {
            ExploreDOM.hideNoUsersMessage();
            users.forEach(user => {
                ExploreDOM.createUserCard(user);
                console.log(user);
            });
        }
    }

    static createUserCard(user) {
        const outerDiv = document.createElement('div');
        outerDiv.className = 'col';
        const shadowDiv = document.createElement('div');
        shadowDiv.className = 'card h-100 shadow-sm';
        outerDiv.appendChild(shadowDiv);
        shadowDiv.innerHTML = `
            <div class="card-header">
                <h5 class="card-title" style="color: #1DB954">${user.handle}</h5>
            </div>
            <div class="card-body">
                <p class="card-text">
                    <strong style="color: #1DB954">Spotify Email: </strong>${user.spotEmail}<br>
                    <strong style="color: #1DB954">Spotify Username: </strong> ${user.spotUser}
                </p>
            </div>
            <div class="card-footer bg-transparent">
                <button class="btn btn-sm add-btn" style="outline-style: inset; outline-color: #1DB954;" data-user-id="${user.id}">
                    <i class="bi bi-plus"></i> Add Friend
                </button>
                <button class="btn btn-sm view-btn" data-user-id="${user.id}">
                    <i class="bi bi-search"></i> View Profile
                </button>
            </div>
        `;
        const userContainer = document.querySelector('#userContainer');
        userContainer.appendChild(outerDiv);
    }

}
