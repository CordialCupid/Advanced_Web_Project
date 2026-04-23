'use strict'; 

export class SpotifyAJAXRepository {
    #AUTHORIZE = "https://accounts.spotify.com/authorize";
    #scopes = "user-read-private user-top-read user-read-email user-library-modify playlist-modify-public playlist-modify-private playlist-read-private playlist-read-collaborative";
    #clientId = "ad61ff7774e443b99cf8123f95809301";
    #clientSecret = "a433dd0d69874f64b96d5a5cc193c496";
    #redirectURI = "http://127.0.0.1:5097/Identity/Account/Register";
    #baseAddress = 'https://api.spotify.com/v1/';

    get authorize() {
        return this.#AUTHORIZE;
    }

    get baseAddress() {
        return this.#baseAddress;
    }

    get secret() {
        return this.#clientSecret;
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
      
    requestAuth() {
        let url = this.authorize;
        url += "?client_id=" + this.clientId;
        url += "&response_type=code";
        url += "&redirect_uri=" + encodeURI(this.redirectURI);
        url += "&show_dialog=true";
        url += "&scope=" + this.scopes;
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
        const authBasic = btoa(this.clientId + ':' + this.#clientSecret);

        return await fetch('https://accounts.spotify.com/api/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': 'Basic ' + authBasic,
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
        const authBasic = btoa(this.clientId + ':' + this.clientSecret);
        const spotResponse = await fetch('https://accounts.spotify.com/api/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': 'Basic ' + authBasic,
            },
            body: new URLSearchParams({
                'code': code,
                'grant_type': 'authorization_code',
                'redirect_uri': 'http://127.0.0.1:5097/Identity/Account/Register'
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
                const tokenInp = document.querySelector('.spotify-token');
                tokenInp.value = access_token;
            }
            if (content.refresh_token != undefined) {
                let refresh_token = content.refresh_token;
                localStorage.setItem("refresh_token", refresh_token);
            }
        } else {
            throw new Error('Status: ' + api_Response.status);
        }
    }
}

