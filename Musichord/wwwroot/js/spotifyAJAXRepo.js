'use strict'; 

import { SpotifyPKCE } from "./spotifyPKCE.js";

export class SpotifyAJAXRepository {
    #AUTHORIZE = "https://accounts.spotify.com/authorize";
    #scopes = "user-read-private user-read-recently-played user-top-read user-read-email user-library-modify playlist-modify-public playlist-modify-private playlist-read-private playlist-read-collaborative";
    #clientId = "ad61ff7774e443b99cf8123f95809301";
    #redirectURI = "http://127.0.0.1:5097/Identity/Account/Register";
    #baseAddress = 'https://api.spotify.com/v1/';

    get authorize() {
        return this.#AUTHORIZE;
    }

    get baseAddress() {
        return this.#baseAddress;
    }

    get scopes() {
        return this.#scopes;
    }

    get clientId() {
        return this.#clientId;
    }

    get redirectURI() {
        return this.#redirectURI;
    }
      
    async requestAuth() {
        const pkce = new SpotifyPKCE();
        const codeChallenge = await pkce.codeChallenge();
        let url = this.authorize;
        url += "?client_id=" + this.clientId;
        url += "&response_type=code";
        url += "&redirect_uri=" + encodeURI(this.redirectURI);
        url += "&show_dialog=true";
        url += "&scope=" + this.scopes;
        url += "&code_challenge_method=S256";
        url += "&code_challenge=" + codeChallenge;
        window.location.href = url;
    }

    async callSpotAPI(method, uri, access_token) {
        return await fetch('https://api.spotify.com' + uri, {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + access_token
            }
        })
        .then(async response => {
            if (response.status == 401) {
                await this.refreshAccessToken();
            }
            return response.json();
        })
        .catch(error => {
            console.log(error);
        });
    }

    async refreshAccessToken() {
        let refresh_Token = localStorage.getItem("refresh_token");

        return await fetch('https://accounts.spotify.com/api/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: new URLSearchParams({
                'grant_type': 'refresh_token',
                'refresh_token': refresh_Token,
                'client_id': this.clientId
            })
        })
        .then(async response => {
            await this.handleAuthResponse(response)})
        .catch(error => {
            console.log(error);
        });
    }

    async retrieveTopFive() {
        const tok = localStorage.getItem('access_token');

        return await this.callSpotAPI('GET', '/v1/me/top/tracks', tok);
        
    }

    // function to make post request to 'api/token' endpoint and retrieve access token
    async retrieveAccessToken(code) {
        const verifier = localStorage.getItem('codeVerifier');
        const spotResponse = await fetch('https://accounts.spotify.com/api/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: new URLSearchParams({
                'code': code,
                'grant_type': 'authorization_code',
                'redirect_uri': 'http://127.0.0.1:5097/Identity/Account/Register',
                'client_id': this.clientId,
                'code_verifier': verifier
            })
        })
        .then(async response => {
            await this.handleAuthResponse(response)})
        .catch(error => {
            console.log(error);
        });
    }

    async handleAuthResponse(api_Response) {
        if (api_Response.ok) {
            const content = await api_Response.json();
            if (content.access_token != undefined) {
                let access_token = content.access_token;
                localStorage.setItem("access_token", access_token);
            }
            if (content.refresh_token != undefined) {
                let refresh_token = content.refresh_token;
                localStorage.setItem("refresh_token", refresh_token);
            }
        } else {
            throw new Error('Status: ' + api_Response.status);
        }
    }

    async getFive(address) {
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("Error getting top five!");
        }
        return await response.json();
    }

    async getRecent(address) {
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("Error getting recently played!");
        }
        return await response.json();
    }

    async checkToken(address) {
        const response = await fetch(address)
                .then(async resp => {
                    if (!response.ok) {
                        await this.refreshAccessToken();
                    }    
                });
    }
}

