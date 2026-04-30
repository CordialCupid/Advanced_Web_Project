export class friendAJAXRepository
{
    #baseAddress;

    constructor(address) {
        this.#baseAddress = address;
    }

    async createFriendship(name) {
        const newAddr = this.#baseAddress + `/addfriend/${name}`;
        const response = await fetch(newAddr, {
            method: 'post'
        });
        if (!response.ok) {
            throw new Error("Error adding friend!");
        }
        return await response.json();
    }

    async readAll() {
        const newAddr = this.#baseAddress + `/nonfriends`;
        const response = await fetch(newAddr);
        if (!response.ok) {
            throw new Error("Error adding friend!");
        }
        return response.json();
    }
    
    async updateFriendshipStatus(name) {
        const newAddr = this.#baseAddress + `/updatestatus/${name}`;
        const response = await fetch(newAddr, {
            method: 'put'
        });
        if (!response.ok) {
            throw new Error("Error updating friendship status!");
        }
        return await response.json();
    }    
    
    // deletes a friend request OR friendship
    async deleteFriendship(name) {
        const newAddr = this.#baseAddress + `/removefriend/${name}`;
        const response = await fetch(newAddr, {
            method: 'delete'
        });
        if (!response.ok) {
            throw new Error("Error deleting friendship!");
        }
        return await response.json();
    }

    async getNonActivity() {
        const newAddr = this.#baseAddress + `/nonfriends/records`;
        const response = await fetch(newAddr);
        if (!response.ok) {
            throw new Error("Error adding friend!");
        }
        return response.json();
    }
}