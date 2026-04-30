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
        return await response.json();
    }
}