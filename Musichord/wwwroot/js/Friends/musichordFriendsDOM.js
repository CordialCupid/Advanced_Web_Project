'use strict';

export class FriendsDOM {

    static showNoActivityMessage() {
        const emptyState = document.querySelector('#emptyStateAct');
        emptyState.setAttribute('style', 'display: block;');
    }

    static hideNoActivityMessage() {
        const emptyState = document.querySelector('#emptyStateAct');
        emptyState.setAttribute('style', 'display: none;');
    }


    static showUserActivity(records) {
        const table = document.querySelector('.activity-body');
        table.innerHTML = "";
        if (records.length === 0) {
            FriendsDOM.showNoActivityMessage();
        } else {
            FriendsDOM.hideNoActivityMessage();
            records.forEach(rec => {
                FriendsDOM.insertActivity(rec);
            });
        }
    }

    static insertActivity(record) {
        const tableRow = document.createElement('tr');
        const body = document.querySelector('.activity-body');
        body.appendChild(tableRow);
        tableRow.innerHTML = `
            <td class="align-middle">${record.userHandle}</td>
            <td class="align-middle">${record.trackName}</td>
            <td class="align-middle"><img src="${record.profilePicture}" width="20px" height="20px"></td>
        `;
    }

}
